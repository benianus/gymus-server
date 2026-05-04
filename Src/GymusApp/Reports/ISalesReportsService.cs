namespace gymus_server.GymusApp.Reports;

public interface ISalesReportsService
{
    public Task<int> TotalSales();
    public Task<int> MonthlySales();
    public Task<int> TotalStoreSales();
    public Task<int> MonthlyStoreSales();
    public Task<int> TotalSessions();
    public Task<int> MonthlySessions();
    public Task<int> TotalMemberships();
    public Task<int> MonthlyMemberships();
    public Task<int> TotalActiveMemberships();
    public Task<int> MonthlyActiveMemberships();
}