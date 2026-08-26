namespace gymus_server.Shared.Exceptions;

public class ForbiddenAccessException(string message) : Exception(message) { }