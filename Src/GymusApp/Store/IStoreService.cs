using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;

namespace gymus_server.GymusApp.Store;

public interface IStoreService
{
    Task<List<ProductResponseDto>> ViewProducts(int page, int pageSize);
    Task<ProductResponseDto> ViewProduct(int productId);
    Task AddNewProduct(ProductCreateRequestDto dto);
    Task RegisterNewSale(int productId, SaleRegisterRequestDto dto);
}