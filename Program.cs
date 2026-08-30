using System.Text;
using System.Threading.RateLimiting;
using gymus_server.Shared.AuthorizationPolicies.Memberships;
using gymus_server.Shared.AuthorizationPolicies.StorePolicies;
using gymus_server.Shared.DependencyInjection;
using gymus_server.Shared.Enums;
using gymus_server.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices();
builder.Services.AddValidatorsServices();
builder.Services.AddAuthorizationPolicies();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

/***
 * security layers
 */
builder.Services.AddHttpsRedirection(options => {
        options.HttpsPort = 8080;
        options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    }
);
builder.Services.AddCors(corsOptions => {
        corsOptions.AddPolicy(
            nameof(CorsPolicies.GymusApiPolicy),
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
                            builder.Configuration.GetSection("Jwt:SecretKey").Value!
                        )
                    )
                };
            }
        );
builder.Services.AddAuthorization(options => {
        options.AddPolicy(
            nameof(AuthorizationPolicies.MembershipsOwner),
            policy => {
                policy.Requirements.Add(new MembershipsOwnerRequirement());
            }
        );
        options.AddPolicy(
            nameof(AuthorizationPolicies.StoreOwner),
            policy => { policy.Requirements.Add(new StoreOwnershipRequirement()); }
        );
    }
);

builder.Services.AddRateLimiter(options => {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddPolicy(
            nameof(RateLimiterPolicies.AuthRateLimiter),
            context => {
                var id = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(
                    id,
                    _ => new FixedWindowRateLimiterOptions {
                        AutoReplenishment = false,
                        PermitLimit = 5,
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(1)
                    }
                );
            }
        );

        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context => {
                var id = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter(
                    id,
                    _ => new FixedWindowRateLimiterOptions {
                        AutoReplenishment = false,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(1)
                    }
                );
            }
        );
    }
);

builder.Services.AddHttpLogging(options => options.LoggingFields = HttpLoggingFields.All);

builder.Services.AddHttpContextAccessor();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors(nameof(CorsPolicies.GymusApiPolicy));

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseExceptionHandler();

app.UseHttpLogging();

app.MapControllers();

app.Run();