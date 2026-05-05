using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Reports;

[ApiController]
[Route("api/reports")]
public class RevenueReportsController(IRevenueReportsService revenueReportsService)
    : ControllerBase {
    [HttpGet("total-revenue")]
    public async Task<IActionResult> GetTotalRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalRevenue()));

    [HttpGet("monthly-revenue")]
    public async Task<IActionResult> GetMonthlyRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyRevenue()));

    [HttpGet("total-store-revenue")]
    public async Task<IActionResult> GetTotalStoreRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalStoreRevenue()));

    [HttpGet("monthly-store-revenue")]
    public async Task<IActionResult> GetMonthlyStoreRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyStoreRevenue()));

    [HttpGet("total-sessions-revenue")]
    public async Task<IActionResult> GetTotalSessionRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalSessionRevenue()));

    [HttpGet("monthly-sessions-revenue")]
    public async Task<IActionResult> GetMonthlySessionRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlySessionRevenue()));

    [HttpGet("total-memberships-revenue")]
    public async Task<IActionResult> GetTotalMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalMembershipsRevenue()));

    [HttpGet("monthly-memberships-revenue")]
    public async Task<IActionResult> GetMonthlyMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyMembershipsRevenue()));

    [HttpGet("total-active-memberships-revenue")]
    public async Task<IActionResult> GetTotalActiveMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalActiveMembershipsRevenue()));

    [HttpGet("monthly-active-memberships-revenue")]
    public async Task<IActionResult> GetMonthlyActiveMembershipsRevenue() => Ok(
        new ApiResponse<decimal>(await revenueReportsService.MonthlyActiveMembershipsRevenue())
    );
}