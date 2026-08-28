using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gymus_server.GymusApp.Auth;
using gymus_server.GymusApp.Auth.Models;
using gymus_server.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace gymus_server.Shared.Security;

public class JwtHelpers(IConfiguration configuration, UserRepository userRepository) {
    private SigningCredentials SigningCredentials => new(Key, SecurityAlgorithms.HmacSha256);

    private SecurityKey Key => new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value!)
    );

    private string Issuer => configuration.GetSection("Jwt:Issuer").Value!;
    private string Audience => configuration.GetSection("Jwt:Audience").Value!;

    private DateTime AccessTokenExpiration =>
        DateTime.Now.AddMinutes(
            Convert.ToDouble(configuration.GetSection("Jwt:AccessTokenExpiration").Value)
        );

    private DateTime RefreshTokenExpiration =>
        DateTime.Now.AddDays(
            Convert.ToDouble(configuration.GetSection("Jwt:RefreshTokenExpiration").Value)
        );

    public string GenerateAccessToken(Claim[] claims) =>
        GenerateJwtToken(claims, AccessTokenExpiration);

    public string GenerateRefreshToken(Claim[] claims) =>
        GenerateJwtToken(claims, RefreshTokenExpiration);

    private string GenerateJwtToken(Claim[] claims, DateTime expires) =>
        new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: expires,
                signingCredentials: SigningCredentials
            )
        );

    public List<Claim> ExtractClaims(string token) => [
        .. new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.Claims
    ];

    public T ExtractClaim<T>(string token, Func<List<Claim>, T> extractClaim) {
        var claims = ExtractClaims(token);
        return extractClaim(claims);
    }

    public string? ExtractUsername(string token) =>
        ExtractClaim(
            token,
            claims => claims.FirstOrDefault(claim => claim.Type == "username")?.Value
        );

    public long ExtractExpiration(string token) =>
        Convert.ToInt64(
            ExtractClaim(
                token,
                claims => claims.FirstOrDefault(claim => claim.Type == "exp")?.Value
            )
        );

    public async Task<bool> IsTokenValid(string token) {
        var username = ExtractUsername(token)
                    ?? throw new UsernameNotFoundException("resource not found");
        var expiration = DateTimeOffset.FromUnixTimeSeconds(ExtractExpiration(token))
                                       .LocalDateTime;
        var user = await userRepository.FindByUsername(username);
        return user != null && expiration > DateTime.Now;
    }

    public Claim[] GenerateClaims(User user) => [
        new("userId", user.Id.ToString()), new("username", user.Username), new("role", user.Role)
    ];
}