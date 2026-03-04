using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymSaaS.Core.DTOs.Auth;
using GymSaaS.Core.Interfaces;
using GymSaaS.Domain.Entities;
using GymSaaS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GymSaaS.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IDbHelperService _db;
    private readonly IConfiguration _config;

    public AuthService(IDbHelperService db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var emailExists = await _db.ExistsAsync<Tenant>(t => t.Email == request.Email);
        if (emailExists)
            throw new InvalidOperationException("A gym with this email already exists.");

        var tenant = Tenant.Create(request.GymName, request.OwnerName, request.Email, request.Phone, request.City);
        await _db.InsertAsync(tenant);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var owner = User.Create(tenant.Id, request.OwnerName, request.Email, request.Phone, passwordHash, UserRole.Owner);
        await _db.InsertAsync(owner);

        return BuildAuthResponse(owner, tenant);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _db.Query<User>()
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var tenant = await _db.GetByIdAsync<Tenant>(user.TenantId)
            ?? throw new UnauthorizedAccessException("Gym account not found.");

        user.RecordLogin();
        await _db.UpdateAsync(user);

        return BuildAuthResponse(user, tenant);
    }

    private AuthResponse BuildAuthResponse(User user, Tenant tenant)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("tenantId", tenant.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("gymName", tenant.GymName)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            TenantId = tenant.Id,
            GymName = tenant.GymName,
            ExpiresAt = expiry
        };
    }
}
