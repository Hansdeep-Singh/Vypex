using Microsoft.AspNetCore.Mvc;
using Vypex.CodingChallenge.API.Services.EmployeeService;
using Vypex.CodingChallenge.API.Services.LeaveService;
using Vypex.CodingChallenge.Domain;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ILeaveService leaveService;
        public EmployeesController(IEmployeeService employeeService, ILeaveService leaveService)
        {
            this.employeeService = employeeService;
            this.leaveService = leaveService;
        }
        [HttpGet("GetEmployees")]
        public async Task<IEnumerable<EmployeeView>> GetEmployees()
        {
            List<EmployeeView> employeeViews = new();
            if (this.employeeService is not null)
            {
                foreach (var employee in (await this.employeeService.All()))
                {
                    double days = 0;
                    foreach (var employeeLeave in leaveService.GetEmployeeLeave(employee.Id))
                    {
                        if (employeeLeave.LeaveTo.Day == employeeLeave.LeaveFrom.Day) days += 1;
                        days += (employeeLeave.LeaveTo - employeeLeave.LeaveFrom).TotalDays;
                    }
                    var employeeView = new EmployeeView
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        TotalLeave = Convert.ToInt16(days),
                    };
                    employeeViews.Add(employeeView);
                }
                return employeeViews;
            }
            IEnumerable<Employee> employees = new List<Employee>();
            return employeeViews;
        }

        [HttpGet("GetLeaves")]
        public async Task<IEnumerable<Leave>> GetLeaves()
        {
            if (this.leaveService is not null) return await this.leaveService.All();
            IEnumerable<Leave> leaves = new List<Leave>();
            return leaves;
        }

        [HttpGet("GetLeavesByEmployeeId/{id}")]
        public IEnumerable<Leave> GetLeavesByEmployeeId(string id)
        {
            if (this.leaveService is not null) return this.leaveService.GetEmployeeLeave(Guid.Parse(id));
            IEnumerable<Leave> leaves = new List<Leave>();
            return leaves;
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<Employee> GetEmployeeById(string id) => await this.employeeService.GetEmployeeById(Guid.Parse(id));

        [HttpPost("Leave")]
        public async Task<bool> Leave(Leave leave)
        {
            if (leave.LeaveFrom.Day != leave.LeaveTo.Day)
            {
                leave.LeaveFrom = leave.LeaveFrom.AddDays(1);
                leave.LeaveTo = leave.LeaveTo.AddDays(1);
            }
            if (leaveService is not null) await leaveService.Add(leave);
            return true;
        }
        [HttpPost("DeleteEmployeeLeave")]
        public async Task<bool> DeleteEmployeeLeave(Employee employee)
        {
            if (leaveService is not null) await leaveService.Delete(leaveService.GetEmployeeLeave(employee.Id).ToList());
            return true;
        }

        [HttpPost("DeleteLeaveRequests")]
        public async Task<bool> DeleteLeaveRequests(string[] leaves)
        {
            List<Leave> leavesList = new();
            
            if (leaveService is not null)
            {
                foreach (var item in leaves)
                {
                    Leave leave = new();
                    leave.LeaveId = Guid.Parse(item);
                    leavesList.Add(leave);
                }
                await leaveService.Delete(leavesList);
            }
            return true;
        }

        [HttpDelete("DeleteEmployeesLeave")]
        public async Task<bool> DeleteEmployeesLeave()
        {
            if (leaveService is not null) await leaveService.Delete((await leaveService.All()).ToList());
            return true;
        }
    }
}
