namespace gymus_server.GymusApp.Reports;

public class SalesReportsService(SalesReportsRepository salesReportsRepository)
    : ISalesReportsService {
    public async Task<int> TotalSales() => await salesReportsRepository.TotalSales();

    public async Task<int> MonthlySales() => await salesReportsRepository.MonthlySales();

    public async Task<int> TotalStoreSales() => await salesReportsRepository.TotalStoreSales();

    public async Task<int> MonthlyStoreSales() => await salesReportsRepository.MonthlyStoreSales();

    public async Task<int> TotalSessions() => await salesReportsRepository.TotalSessions();

    public async Task<int> MonthlySessions() => await salesReportsRepository.MonthlySessions();

    public async Task<int> TotalMemberships() => await salesReportsRepository.TotalMemberships();

    public async Task<int> MonthlyMemberships() =>
        await salesReportsRepository.MonthlyMemberships();

    public async Task<int> TotalActiveMemberships() =>
        await salesReportsRepository.TotalActiveMemberships();

    public async Task<int> MonthlyActiveMemberships() =>
        await salesReportsRepository.MonthlyActiveMemberships();
}