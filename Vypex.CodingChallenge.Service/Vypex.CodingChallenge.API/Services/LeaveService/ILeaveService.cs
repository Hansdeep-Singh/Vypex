using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.API.Services.LeaveService
{
    public interface ILeaveService
    {
        Task<bool> Add(Leave leave);
        Task<IEnumerable<Leave>> All();
        Task<bool> Delete(List<Leave> leaves);
        IEnumerable<Leave> GetEmployeeLeave(Guid EmployeeId);
     
    }
}