namespace gymus_server.GymusApp.Store.Dtos.Requests;

public record ProductUpdateRequestDto(
    string? ProductName,
    int? Quantity,
    decimal? Price,
    IFormFile? ProductImage,
    string? ProductDescription
);