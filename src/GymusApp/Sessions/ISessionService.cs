namespace gymus_server.GymusApp.Sessions;

public interface ISessionService
{
    Task ViewSessions();
    Task RegisterSession();
}