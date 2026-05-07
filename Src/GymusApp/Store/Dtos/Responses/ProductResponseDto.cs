namespace gymus_server.GymusApp.Store.Dtos.Responses;

public record ProductResponseDto(
    int Id,
    string ProductName,
    string ProductImage,
    string ProductDescription,
    int Quantity,
    decimal Price,
    int AddedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt
);