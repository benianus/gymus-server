namespace gymus_server.Shared.Enums;

public enum CorsPolicies {
    GymusApiPolicy
}

public enum RateLimiterPolicies {
    AuthRateLimiter, GlobalRateLimiter
}

public enum AuthorizationPolicies {
    MembershipsOwner, StoreOwner
}