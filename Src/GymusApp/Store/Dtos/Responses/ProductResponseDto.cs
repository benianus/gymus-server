namespace gymus_server.GymusApp.Store.Dtos.Responses;

public record ProductResponseDto(
    int Id,
    string ProductName,
    int Quantity,
    decimal Price,
    string ProductImage,
    string ProductDescription,
    DateTime CreatedAt,
    DateTime UpdatedAt
);