using Dapper;
using gymus_server.Shared.Infrastructures;

namespace gymus_server.GymusApp.Reports;

public class SalesReportsRepository(IDbConnectionFactory connection) {
    public async Task<int> TotalSales() {
        const string query = """
                                select total_sales from get_total_sales() as total_sales
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlySales() {
        const string query = """
                                select * from get_monthly_sales()
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> TotalStoreSales() {
        const string query = """
                             select * from get_total_store_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlyStoreSales() {
        const string query = """
                             select * from get_monthly_store_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> TotalSessions() {
        const string query = """
                             select * from get_total_sessions_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlySessions() {
        const string query = """
                             select * from get_monthly_sessions_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> TotalMemberships() {
        const string query = """
                             select * from get_total_memberships_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlyMemberships() {
        const string query = """
                             select * from get_monthly_memberships_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> TotalActiveMemberships() {
        const string query = """
                             select * from get_total_active_memberships_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> MonthlyActiveMemberships() {
        const string query = """
                             select * from get_monthly_active_memberships_sales();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(query);
    }
}