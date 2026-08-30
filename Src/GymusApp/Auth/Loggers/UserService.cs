namespace gymus_server.GymusApp.Auth;

partial class UserService {
    [LoggerMessage(
        LogLevel.Information,
        "logging succeed, user '{username}' success to login from ip: {ip}"
    )]
    partial void LogLoggingSucceedUserUsernameSuccessToLogInFromIpIp(string username, string ip);

    [LoggerMessage(
        LogLevel.Error,
        "login Failed, username not found, user: {username} failed to login from ip: {ip}"
    )]
    partial void LogLoginFailedUsernameNotFoundUsernameUsernameIpIp(string username, string ip);

    [LoggerMessage(
        LogLevel.Error,
        "invalid credentials, user '{username}' try to sing in from ip: {ip}"
    )]
    partial void LogInvalidCredentialsUserUsernameTryToSingInFromIpIp(string username, string ip);

    [LoggerMessage(LogLevel.Error, "user '{username}' failed to save refresh token from ip: {ip}")]
    partial void LogUserUsernameFailedToSaveRefreshTokenFromIpIp(string username, string ip);

    [LoggerMessage(
        LogLevel.Error,
        "refresh token not found, user: {username} try to find refresh token from ip: {ip}"
    )]
    partial void LogRefreshTokenNotFoundUserUsernameTryToFindRefreshTokenFromIpIp(
        string username,
        string ip
    );

    [LoggerMessage(LogLevel.Error, "invalid refresh token, user: {username} from ip: {ip}")]
    partial void LogInvalidRefreshTokenUserUsernameFromIpIp(string username, string ip);

    [LoggerMessage(
        LogLevel.Information,
        "logout successful, user '{username}' has been logged out from ip {ip}."
    )]
    partial void
        LogLogoutSuccessfulUserUsernameHasBeenLoggedOutFromIpIp(string username, string ip);
}