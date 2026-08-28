using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Diagnostics;

namespace gymus_server.Shared.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler {
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    ) {
        var response = httpContext.Response;

        response.ContentType = "application/json";

        var statusCode = exception switch {
            BadCredentialsException => StatusCodes.Status401Unauthorized,
            ForbiddenAccessException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            InvalidIdException => StatusCodes.Status400BadRequest,
            LogoutException => StatusCodes.Status200OK,
            TooManyRequestsException => StatusCodes.Status429TooManyRequests,
            _ => StatusCodes.Status500InternalServerError
        };

        response.StatusCode = statusCode;
        response.ContentType = "application/json";

        var result = new ApiResponse<List<string>>([exception.Message]);

        await response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}