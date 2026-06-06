using ContentNegotiationDemo.Models;

namespace ContentNegotiationDemo.Data
{
    public static class EmployeeRepository
    {
        private static readonly List<Employee> Employees =
        [
            new Employee { Id = 1, Name = "Anand", Department = "IT", Salary = 55000, Email = "anand@demo.com" },
            new Employee { Id = 2, Name = "Priya", Department = "HR", Salary = 48000, Email = "priya@demo.com" },
            new Employee { Id = 3, Name = "Karthik", Department = "Finance", Salary = 62000, Email = "karthik@demo.com" }
        ];

        public static IEnumerable<Employee> GetAll()
        {
            return Employees;
        }

        public static Employee? GetById(int id)
        {
            return Employees.FirstOrDefault(employee => employee.Id == id);
        }

        public static Employee Add(CreateEmployeeRequest request)
        {
            var employee = new Employee
            {
                Id = Employees.Max(employee => employee.Id) + 1,
                Name = request.Name,
                Department = request.Department,
                Salary = request.Salary,
                Email = request.Email
            };

            Employees.Add(employee);
            return employee;
        }
    }
}
