namespace gymus_server.GymusApp.Auth;

public partial class UserController {
    [LoggerMessage(
        LogLevel.Information,
        "register failed, user '/{username}'/ enter invalid input from ip: {ip}."
    )]
    partial void LogRegisterFailedUserUsernameEnterInvalidInputFromIpIp(string username, string ip);
}