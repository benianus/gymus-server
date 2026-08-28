using System.Security.Claims;
using gymus_server.GymusApp.Store;
using gymus_server.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace gymus_server.Shared.AuthorizationPolicies.StorePolicies;

public class StoreOwnershipHandler(
    StoreRepository storeRepository
) : AuthorizationHandler<StoreOwnershipRequirement, int>
{
    protected override async Task<Task> HandleRequirementAsync(
        AuthorizationHandlerContext context,
        StoreOwnershipRequirement requirement,
        int productId
    ) {
        if (context.User.IsInRole("Owner")) {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        if (await storeRepository.IsUserOwnProduct(productId, userId)) context.Succeed(requirement);
        throw new ForbiddenAccessException("you are not allowed to access this resource");
    }
}