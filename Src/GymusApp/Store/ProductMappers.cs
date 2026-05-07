using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.GymusApp.Store.Models;

namespace gymus_server.GymusApp.Store.Mappers;

public static class ProductMappers {
    public static Product ToEntity(
        this ProductCreateRequestDto dto,
        string productImageFilePath
    ) =>
        new() {
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductImage = productImageFilePath,
            Quantity = dto.Quantity,
            Price = dto.Price
        };

    public static ProductResponseDto ToDto(this Product entity) =>
        new(
            entity.Id,
            entity.ProductName,
            entity.ProductImage,
            entity.ProductDescription,
            entity.Quantity,
            entity.Price,
            entity.AddedBy,
            entity.CreatedAt,
            entity.UpdatedAt
        );
}