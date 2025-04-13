using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vypex.CodingChallenge.API.Repositories;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.API.Services.EmployeeService
{
    public class EmployeeService : Repository<Employee>, IEmployeeService
    {
        private readonly CodingChallengeContext context;
        public EmployeeService(CodingChallengeContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Employee>> All()
        {
            return await GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(Guid EmployeeId) => await GetOneGuidIdAsync(EmployeeId);
    }
}
