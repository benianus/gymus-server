using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Employees;

public class EmployeeService(ICrudRepository<Employee, int> employeeService)
    : ICrudService<Employee, int>
{
    public async Task<List<Employee>> GetAll()
    {
        return await employeeService.GetAll();
    }

    public async Task<Employee?> GetOne(int id)
    {
        return await employeeService.GetOne(id);
    }

    public async Task<Employee?> Create(Employee data)
    {
        if (IsSalaryNegativeOrZero(data.Salary)) return null;
        return await employeeService.Create(data);
    }

    public async Task<Employee?> Update(Employee data, int id)
    {
        if (IsSalaryNegativeOrZero(data.Salary)) return null;
        return await employeeService.Update(data, id);
    }

    public async Task<bool> Delete(int id)
    {
        return await employeeService.Delete(id);
    }

    private static bool IsSalaryNegativeOrZero(decimal salary)
    {
        return decimal.IsNegative(salary) || salary == 0;
    }
}