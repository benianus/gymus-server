using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Reports;

[Authorize]
[ApiController]
[Route("api/reports")]
public class SalesReportsController(ISalesReportsService salesReportsService) : ControllerBase {
    [Authorize(Roles = "Owner")]
    [HttpGet("total-sales")]
    public async Task<IActionResult> GetTotalSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.TotalSales()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-sales")]
    public async Task<IActionResult> GetMonthlySales() =>
        Ok(new ApiResponse<int>(await salesReportsService.MonthlySales()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-store-sales")]
    public async Task<IActionResult> GetTotalStoreSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.TotalStoreSales()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-store-sales")]
    public async Task<IActionResult> GetMonthlyStoreSales() =>
        Ok(new ApiResponse<int>(await salesReportsService.MonthlyStoreSales()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-sessions-sales")]
    public async Task<IActionResult> GetTotalSessions() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalSessions())
    );

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-sessions-sales")]
    public async Task<IActionResult> GetMonthlySessions() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlySessions())
    );

    [Authorize(Roles = "Owner")]
    [HttpGet("total-memberships-sales")]
    public async Task<IActionResult> GetTotalMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalMemberships())
    );

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-memberships-sales")]
    public async Task<IActionResult> GetMonthlyMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlyMemberships())
    );

    [Authorize(Roles = "Owner")]
    [HttpGet("total-active-memberships-sales")]
    public async Task<IActionResult> GetTotalActiveMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.TotalActiveMemberships())
    );

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-active-memberships-sales")]
    public async Task<IActionResult> GetMonthlyActiveMemberships() => Ok(
        new ApiResponse<int>(await salesReportsService.MonthlyActiveMemberships())
    );
}