using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookManagement.DataAccess.Data;
using BookManagement.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.DataAccess.Repositories;

public class UserService : IUserService
{
    private readonly AppIdentityDbContext _identityDbContext;
    private readonly IConfiguration _config;

    public UserService(AppIdentityDbContext identityDbContext, IConfiguration config)
    {
        _identityDbContext = identityDbContext;
        _config = config;
    }
    
    public async Task<string> RegisterAsync(string email, string password)
    {
        var existingUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (existingUser != null)
            throw new Exception("Email already exists.");
        
        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password)
        };
        
        _identityDbContext.Users.Add(user);
        await _identityDbContext.SaveChangesAsync();

        return "User registered successfully.";
    }
    
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        // Find the user by email
        var user = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
            throw new Exception("Invalid credentials.");

        
        return GenerateToken(user);
    }
    
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Jwt:ExpiryInMinutes")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}