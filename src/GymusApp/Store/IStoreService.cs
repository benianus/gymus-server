namespace gymus_server.GymusApp.Store;

public interface IStoreService
{
    Task AddNewProduct();
    Task RegisterNewSale();
}