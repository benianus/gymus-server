using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Reports;

[ApiController]
[Route("api/reports")]
public class ReportsController(
    ISalesReportsService salesReportsService,
    IRevenueReportsService revenueReportsService
) : ControllerBase {
    [HttpGet("total-sales")]
    public async Task<IActionResult> GetTotalSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.TotalSales()));

    [HttpGet("monthly-sales")]
    public async Task<IActionResult> GetMonthlySales() =>
        Ok(new ApiResponse<int>(await salesReportsService.MonthlySales()));

    [HttpGet("total-store-sales")]
    public async Task<IActionResult> GetTotalStoreSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.TotalStoreSales()));

    [HttpGet("monthly-store-sales")]
    public async Task<IActionResult> GetMonthlyStoreSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.MonthlyStoreSales()));

    [HttpGet("total-sessions-sales")]
    public async Task<IActionResult> GetTotalSessions() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalSessions())
    );

    [HttpGet("monthly-sessions-sales")]
    public async Task<IActionResult> GetMonthlySessions() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlySessions())
    );

    [HttpGet("total-memberships-sales")]
    public async Task<IActionResult> GetTotalMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalMemberships())
    );

    [HttpGet("monthly-memberships-sales")]
    public async Task<IActionResult> GetMonthlyMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlyMemberships())
    );

    [HttpGet("total-active-memberships-sales")]
    public async Task<IActionResult> GetTotalActiveMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalActiveMemberships())
    );

    [HttpGet("monthly-active-memberships-sales")]
    public async Task<IActionResult> GetMonthlyActiveMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlyActiveMemberships())
    );
}