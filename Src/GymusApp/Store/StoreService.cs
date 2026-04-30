using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.GymusApp.Store.Mappers;
using gymus_server.Shared.Exceptions;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Store;

public class StoreService(StoreRepository storeRepository) : IStoreService
{
    public async Task<List<ProductResponseDto>> ViewProducts(int page, int pageSize)
    {
        return await storeRepository.ViewProducts(page, pageSize);
    }

    public async Task<ProductResponseDto> ViewProduct(int productId)
    {
        return await storeRepository.ViewProduct(productId)
            ?? throw new NotFoundException("Product not found");
    }

    public async Task AddNewProduct(ProductCreateRequestDto dto)
    {
        var productPhotoFilePath = await UploadFile(dto.ProductImage)
                                ?? throw new NotFoundException("Product image is required");
        var insertedId = await storeRepository.AddNewProduct(dto.ToEntity(productPhotoFilePath));
        if (insertedId < 0) throw new Exception("Product failed to add");
    }

    public async Task RegisterNewSale(int productId, SaleRegisterRequestDto dto)
    {
        var insertedId = await storeRepository.RegisterNewSale(productId, dto);
        if (insertedId < 0)
            throw new Exception(
                """
                Sale failed to register, product out of stock , quantity more than what's in stock
                or wrong total price (verify frontend part)
                """
            );
    }
}