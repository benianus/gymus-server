using Dapper;
using gymus_server.Shared.Infrastructures;

namespace gymus_server.GymusApp.Reports;

public class ReportsRepository(IDbConnectionFactory connection) {
    public async Task<int> TotalSales() {
        const string query = """
                                select total_sales from get_total_sales() as total_sales
                             """;
        return await connection.CreateConnection().ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlySales() {
        const string query = """
                                select * from get_monthly_sales()
                             """;
        return await connection.CreateConnection().ExecuteScalarAsync<int>(query);
    }

    public async Task<int> TotalStoreSales() {
        const string query = """
                             select * from get_total_store_sales();
                             """;
        return await connection.CreateConnection().ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlyStoreSales() {
        const string query = """
                             select * from get_monthly_store_sales();
                             """;
        return await connection.CreateConnection().ExecuteScalarAsync<int>(query);
    }
}