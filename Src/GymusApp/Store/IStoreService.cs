using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.Shared.Dtos;

namespace gymus_server.GymusApp.Store;

public interface IStoreService {
    Task<PagedResponse<ApiResponse<List<ProductResponseDto>>>> ViewProducts(int page, int pageSize);
    Task<ProductResponseDto> ViewProduct(int productId);
    Task AddNewProduct(ProductCreateRequestDto dto);
    Task RegisterNewSale(int productId, SaleRegisterRequestDto dto);
    Task DeleteProduct(int productId);
    Task UpdateProduct(int productId, ProductUpdateRequestDto dto);
}