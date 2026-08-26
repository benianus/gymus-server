using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Reports;

[Authorize]
[ApiController]
[Route("api/reports")]
public class RevenueReportsController(IRevenueReportsService revenueReportsService)
    : ControllerBase {
    [Authorize(Roles = "Owner")]
    [HttpGet("total-revenue")]
    public async Task<IActionResult> GetTotalRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-revenue")]
    public async Task<IActionResult> GetMonthlyRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-store-revenue")]
    public async Task<IActionResult> GetTotalStoreRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalStoreRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-store-revenue")]
    public async Task<IActionResult> GetMonthlyStoreRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyStoreRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-sessions-revenue")]
    public async Task<IActionResult> GetTotalSessionRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalSessionRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-sessions-revenue")]
    public async Task<IActionResult> GetMonthlySessionRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlySessionRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-memberships-revenue")]
    public async Task<IActionResult> GetTotalMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalMembershipsRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-memberships-revenue")]
    public async Task<IActionResult> GetMonthlyMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.MonthlyMembershipsRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("total-active-memberships-revenue")]
    public async Task<IActionResult> GetTotalActiveMembershipsRevenue() =>
        Ok(new ApiResponse<decimal>(await revenueReportsService.TotalActiveMembershipsRevenue()));

    [Authorize(Roles = "Owner")]
    [HttpGet("monthly-active-memberships-revenue")]
    public async Task<IActionResult> GetMonthlyActiveMembershipsRevenue() => Ok(
        new ApiResponse<decimal>(await revenueReportsService.MonthlyActiveMembershipsRevenue())
    );
}