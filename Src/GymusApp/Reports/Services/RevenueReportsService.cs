namespace gymus_server.GymusApp.Reports;

public class RevenueReportsService(RevenueReportsRepository revenueReportsRepository)
    : IRevenueReportsService {
    public async Task<decimal> TotalRevenue() => await revenueReportsRepository.TotalRevenue();

    public async Task<decimal> MonthlyRevenue() => await revenueReportsRepository.MonthlyRevenue();

    public async Task<decimal> TotalStoreRevenue() =>
        await revenueReportsRepository.TotalStoreRevenue();

    public async Task<decimal> MonthlyStoreRevenue() =>
        await revenueReportsRepository.MonthlyStoreRevenue();

    public async Task<decimal> TotalSessionRevenue() =>
        await revenueReportsRepository.TotalSessionRevenue();

    public async Task<decimal> MonthlySessionRevenue() =>
        await revenueReportsRepository.MonthlySessionRevenue();

    public async Task<decimal> TotalMembershipsRevenue() =>
        await revenueReportsRepository.TotalActiveMembershipsRevenue();

    public async Task<decimal> MonthlyMembershipsRevenue() =>
        await revenueReportsRepository.MonthlyMembershipsRevenue();

    public async Task<decimal> TotalActiveMembershipsRevenue() =>
        await revenueReportsRepository.TotalActiveMembershipsRevenue();

    public async Task<decimal> MonthlyActiveMembershipsRevenue() =>
        await revenueReportsRepository.MonthlyActiveMembershipsRevenue();
}