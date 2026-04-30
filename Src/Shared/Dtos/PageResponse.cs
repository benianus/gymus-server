namespace gymus_server.Shared.Dtos;

public record PageResponse<T>(
    T Data,
    int PageSize,
    long TotalItems,
    int TotalPages,
    bool HasNext,
    bool HasPrevious
);