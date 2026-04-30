using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Reports;

[ApiController]
[Route("api/reports")]
public class ReportsController(IReportsService reportsService) : ControllerBase { }