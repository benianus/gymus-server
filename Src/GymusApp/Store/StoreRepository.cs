using Dapper;
using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.GymusApp.Store.Models;
using Npgsql;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Store;

public class StoreRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public async Task<List<ProductResponseDto>> ViewProducts(int page, int pageSize)
    {
        const string query = """
                             select
                                 id,
                                 product_name,
                                 quantity,
                                 price,
                                 product_image,
                                 product_description,
                                 created_at,
                                 updated_at
                             from products
                             order by id desc
                             limit @pageSize
                             offset (@page - 1) * @pageSize;
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var products = connection.Query<ProductResponseDto>(
            query,
            new { page, pageSize }
        );

        return products.ToList();
    }

    public async Task<ProductResponseDto?> ViewProduct(int productId)
    {
        const string query = """
                             select
                                 id,
                                 product_name,
                                 quantity,
                                 price,
                                 product_image,
                                 product_description,
                                 created_at,
                                 updated_at
                             from products where id = @productId
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        return connection.Query<ProductResponseDto>(query, new { productId }).FirstOrDefault();
    }

    public async Task<int> AddNewProduct(Product dto)
    {
        var insertedId = -1;
        const string query = """
                                insert into products (product_name, quantity, price, added_by, product_image, product_description) 
                                values (@productName, @quantity, @price, @addedBy, @productImage, @productDescription)
                                returning id;
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        try
        {
            insertedId = connection.ExecuteScalar<int>(
                query,
                new
                {
                    productName = dto.ProductName,
                    quantity = dto.Quantity,
                    price = dto.Price,
                    addedBy = 5,
                    productImage = dto.ProductImage,
                    productDescription = dto.ProductDescription
                }
            );
        }
        catch (Exception e)
        {
            DeleteFile(dto.ProductImage);
            Console.WriteLine(e.Message);
            throw;
        }

        return insertedId;
    }

    public async Task<int> RegisterNewSale(int productId, SaleRegisterRequestDto dto)
    {
        const string query = """
                                select * from register_new_sale(
                                _product_id := @productId, 
                                _quantity := @quantity, 
                                _total_price := @totalPrice, 
                                _made_by := @madeBy) as inserted_id
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var insertedId = connection.ExecuteScalar<int>(
            query,
            new { productId, quantity = dto.Quantity, totalPrice = dto.TotalPrice, madeBy = 5 }
        );
        return insertedId;
    }
}