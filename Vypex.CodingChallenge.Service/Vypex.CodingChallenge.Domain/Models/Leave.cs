using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vypex.CodingChallenge.Domain.Models
{
    public class Leave
    {
        public Guid LeaveId { get; set; } = Guid.NewGuid();
        public Guid Id { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public Employee? Employee { get; private set; }
    }
}
