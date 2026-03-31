namespace gymus_server.Shared.Utlis;

public static class Helpers
{
    public static bool IsIdValid(int id)
    {
        return int.IsNegative(id) || id == 0;
    }
}