namespace gymus_server.GymusApp.Store.Dtos.Requests;

public record ProductCreateRequestDto(
    string ProductName,
    int Quantity,
    decimal Price,
    IFormFile ProductImage,
    string ProductDescription
);