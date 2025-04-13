using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vypex.CodingChallenge.API.Repositories;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.API.Services.LeaveService
{
    public class LeaveService : Repository<Leave>, ILeaveService
    {
        private readonly CodingChallengeContext context;
        public LeaveService(CodingChallengeContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Leave>> All()
        {
            return await GetAllAsync();
        }

      
        public async Task<bool> Delete(List<Leave> leaves)
        {
            DeleteRange(leaves);
            await SaveAsync();
            return true;
         

        }
        public async Task<bool> Add(Leave leave)
        {
            await AddAsync(leave);
            await SaveAsync();
            return true;
        }
        public IEnumerable<Leave> GetEmployeeLeave(Guid EmployeeId) => GetAllQuery().Where(emp => emp.Id == EmployeeId);
    }
}
