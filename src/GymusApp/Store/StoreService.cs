namespace gymus_server.GymusApp.Store;

public class StoreService(StoreRepository storeRepository): IStoreService
{
    public Task ViewProducts()
    {
        throw new NotImplementedException();
    }
    public Task ViewProduct(int productId)
    {
        throw new NotImplementedException();
    }
    public Task AddNewProduct()
    {
        throw new NotImplementedException();
    }
    public Task RegisterNewSale()
    {
        throw new NotImplementedException();
    }
}