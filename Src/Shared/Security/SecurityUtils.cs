using System.Security.Claims;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.Shared.Exceptions;

namespace gymus_server.Shared.Security;

public class SecurityUtils(MembershipRepository membershipRepository) {
    public async Task<bool> IsUserOwnMembership(int memberId, ClaimsPrincipal user) {
        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return await membershipRepository.IsUserOwnMembership(memberId, userId)
            ? true
            : throw new ForbiddenAccessException("You're not allowed to access this data");
    }
}