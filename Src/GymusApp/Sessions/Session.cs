namespace gymus_server.GymusApp.Sessions;

public record Session(
    int Id,
    string FullName,
    string SessionTypeName,
    DateTime CreatedAt,
    long TotalItems
);