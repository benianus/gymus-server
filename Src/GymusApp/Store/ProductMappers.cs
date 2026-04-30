using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.GymusApp.Store.Models;

namespace gymus_server.GymusApp.Store.Mappers;

public static class ProductMappers
{
    public static Product ToEntity(
        this ProductCreateRequestDto dto,
        string productImageFilePath
    )
    {
        return new Product
        {
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductImage = productImageFilePath,
            Quantity = dto.Quantity,
            Price = dto.Price
        };
    }

    public static ProductResponseDto ToDto(this Product entity)
    {
        return new ProductResponseDto(
            entity.Id,
            entity.ProductName,
            entity.Quantity,
            entity.Price,
            entity.ProductImage,
            entity.ProductDescription,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}