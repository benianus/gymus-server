namespace gymus_server.Shared.Dtos;

public record ApiResponse<T>
{
    public T? Data { get; set; }
    public string Status { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse(T? data)
    {
        Data = data;
        Status = "SUCCESS";
        Errors = null;
    }

    public ApiResponse(List<string> errors)
    {
        Data = default;
        Status = "ERROR";
        Errors = errors;
    }
}