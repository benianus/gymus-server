namespace gymus_server.GymusApp.Store;

public interface IStoreService
{
    Task ViewProducts();
    Task ViewProduct(int productId);
    Task AddNewProduct();
    Task RegisterNewSale();
}