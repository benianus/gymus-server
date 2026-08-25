using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace gymus_server.Shared.Security;

public class JwtHelpers(IConfiguration configuration) {
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
}