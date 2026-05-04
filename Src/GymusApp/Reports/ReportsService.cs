namespace gymus_server.GymusApp.Reports;

public class ReportsService(ReportsRepository reportsRepository)
    : ISalesReportsService, IRevenueReportsService {
    public Task<decimal> TotalRevenue() => throw new NotImplementedException();

    public Task<decimal> MonthlyRevenue() => throw new NotImplementedException();

    public Task<decimal> TotalStoreRevenue() => throw new NotImplementedException();

    public Task<decimal> MonthlyStoreRevenue() => throw new NotImplementedException();

    public Task<decimal> TotalSessionRevenue() => throw new NotImplementedException();

    public Task<decimal> MonthlySessionRevenue() => throw new NotImplementedException();

    public Task<decimal> TotalMembershipsRevenue() => throw new NotImplementedException();

    public Task<decimal> MonthlyMembershipsRevenue() => throw new NotImplementedException();

    public Task<decimal> TotalActiveMembershipsRevenue() => throw new NotImplementedException();

    public Task<decimal> MonthlyActiveMembershipsRevenue() => throw new NotImplementedException();

    public async Task<int> TotalSales() => await reportsRepository.TotalSales();

    public async Task<int> MonthlySales() => await reportsRepository.MonthlySales();

    public async Task<int> TotalStoreSales() => await reportsRepository.TotalStoreSales();

    public async Task<int> MonthlyStoreSales() => await reportsRepository.MonthlyStoreSales();

    public Task<int> TotalSessions() => throw new NotImplementedException();

    public Task<int> MonthlySessions() => throw new NotImplementedException();

    public Task<int> TotalMemberships() => throw new NotImplementedException();

    public Task<int> MonthlyMemberships() => throw new NotImplementedException();

    public Task<int> TotalActiveMemberships() => throw new NotImplementedException();

    public Task<int> MonthlyActiveMemberships() => throw new NotImplementedException();
}