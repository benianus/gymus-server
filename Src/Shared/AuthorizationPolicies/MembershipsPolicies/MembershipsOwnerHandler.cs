using System.Security.Claims;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace gymus_server.Shared.AuthorizationPolicies.Memberships;

public class MembershipsOwnerHandler(MembershipRepository membershipRepository)
    : AuthorizationHandler<MembershipsOwnerRequirement, int> {
    protected override async Task<Task> HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MembershipsOwnerRequirement requirement,
        int memberId
    ) {
        if (context.User.IsInRole("Owner")) {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        if (await membershipRepository.IsUserOwnMembership(memberId, userId))
            context.Succeed(requirement);

        throw new ForbiddenAccessException("you are not allowed to access this resource");
    }
}