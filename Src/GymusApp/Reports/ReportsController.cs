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
}