using Dapper;
using gymus_server.Shared.Infrastructures;

namespace gymus_server.GymusApp.Reports;

public class RevenueReportsRepository(IDbConnectionFactory connection) {
    public async Task<decimal> TotalRevenue() {
        const string query = """
                                select * from get_total_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> MonthlyRevenue() {
        const string query = """
                                select * from get_monthly_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> TotalStoreRevenue() {
        const string query = """
                                select * from get_total_store_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> MonthlyStoreRevenue() {
        const string query = """
                                select * from get_monthly_store_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> TotalSessionRevenue() {
        const string query = """
                                select * from get_total_sessions_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> MonthlySessionRevenue() {
        const string query = """
                                select * from get_monthly_sessions_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> TotalMembershipsRevenue() {
        const string query = """
                                select * from get_total_memberships_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> MonthlyMembershipsRevenue() {
        const string query = """
                                select * from get_monthly_memberships_revenue();
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> TotalActiveMembershipsRevenue() {
        const string query = """
                                select * from get_total_active_memberships_revenue()
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }

    public async Task<decimal> MonthlyActiveMembershipsRevenue() {
        const string query = """
                                select * from get_monthly_active_memberships_revenue()
                             """;
        using var conn = connection.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal>(query);
    }
}