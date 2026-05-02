namespace gymus_server.GymusApp.Reports;

public class ReportsRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");
}