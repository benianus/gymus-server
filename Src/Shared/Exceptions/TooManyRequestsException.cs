namespace gymus_server.Shared.Exceptions;

public class TooManyRequestsException(
    string message = "Too many login attempts. Please try again later."
) : Exception(message) { }