namespace gymus_server.GymusApp.Reports;

public interface IRevenueReportsService {
    public Task<decimal> TotalRevenue();
    public Task<decimal> MonthlyRevenue();
    public Task<decimal> TotalStoreRevenue();
    public Task<decimal> MonthlyStoreRevenue();
    public Task<decimal> TotalSessionRevenue();
    public Task<decimal> MonthlySessionRevenue();
    public Task<decimal> TotalMembershipsRevenue();
    public Task<decimal> MonthlyMembershipsRevenue();
    public Task<decimal> TotalActiveMembershipsRevenue();
    public Task<decimal> MonthlyActiveMembershipsRevenue();
}