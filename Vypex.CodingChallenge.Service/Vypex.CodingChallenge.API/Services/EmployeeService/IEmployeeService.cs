using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.API.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> All();
        Task<Employee> GetEmployeeById(Guid EmployeeId);
    }
}