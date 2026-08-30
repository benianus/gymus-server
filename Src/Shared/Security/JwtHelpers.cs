using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using gymus_server.GymusApp.Auth;
using gymus_server.GymusApp.Auth.Models;
using gymus_server.Shared.Enums;
using gymus_server.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace gymus_server.Shared.Security;

public class JwtHelpers(
    UserRepository userRepository,
    JwtProperties jwtProperties
)
{
    private SigningCredentials SigningCredentials => new(
        jwtProperties.Key,
        SecurityAlgorithms.HmacSha256
    );

    public string GenerateAccessToken(Claim[] claims) =>
        GenerateJwtToken(claims, jwtProperties.AccessTokenExpiration);

    public string GenerateRefreshToken(Claim[] claims) =>
        GenerateJwtToken(claims, jwtProperties.RefreshTokenExpiration);

    private string GenerateJwtToken(Claim[] claims, DateTime expires) =>
        new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                jwtProperties.Issuer,
                jwtProperties.Audience,
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
            claims => claims.FirstOrDefault(claim => claim.Type == nameof(Claims.Username))?.Value
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
        new(nameof(Claims.UserId), user.Id.ToString()),
        new("username", user.Username),
        new("role", user.Role)
    ];
}