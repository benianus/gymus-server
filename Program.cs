using System.Text;
using gymus_server.Shared.DependencyInjection;
using gymus_server.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices();
builder.Services.AddValidatorsServices();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(corsOptions => {
        corsOptions.AddPolicy(
            "GymusApiPolicy",
            policyBuilder => {
                policyBuilder.WithOrigins("http://localhost:5138", "https://localhost:7118")
                             .AllowAnyHeader()
                             .AllowAnyHeader();
            }
        );
    }
);
builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(jwtBearerOptions => {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("Jwt:SecurityKey").Value!
                        )
                    )
                };
            }
        );
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors("GymusApiPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseExceptionHandler();

app.MapControllers();

app.Run();