using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace gymus_server.Shared.Security;

public class JwtProperties(IConfiguration configuration)
{
    public SecurityKey Key => new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value!)
    );

    public string Issuer => configuration.GetSection("Jwt:Issuer").Value!;
    public string Audience => configuration.GetSection("Jwt:Audience").Value!;

    public DateTime AccessTokenExpiration =>
        DateTime.Now.AddMinutes(
            Convert.ToDouble(configuration.GetSection("Jwt:AccessTokenExpiration").Value)
        );

    public DateTime RefreshTokenExpiration =>
        DateTime.Now.AddDays(
            Convert.ToDouble(configuration.GetSection("Jwt:RefreshTokenExpiration").Value)
        );
}