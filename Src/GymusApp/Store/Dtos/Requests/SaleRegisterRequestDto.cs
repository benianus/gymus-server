namespace gymus_server.GymusApp.Store.Dtos.Requests;

public record SaleRegisterRequestDto(
    int Quantity,
    decimal TotalPrice
);