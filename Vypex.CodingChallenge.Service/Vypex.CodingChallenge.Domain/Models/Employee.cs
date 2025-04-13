namespace Vypex.CodingChallenge.Domain.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
    public class EmployeeView : Employee
    {
        public double TotalLeave { get; set; }
    }
}
