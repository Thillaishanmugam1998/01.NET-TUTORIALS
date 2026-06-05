using ModelBindingDemo.API.Models;

namespace ModelBindingDemo.API.Data;

public static class StudentRepository
{
    private static readonly List<Student> Students =
    [
        new() { Id = 1, Name = "Arun Kumar", Email = "arun@example.com", Age = 20, Department = "CSE" },
        new() { Id = 2, Name = "Meena Ravi", Email = "meena@example.com", Age = 22, Department = "ECE" },
        new() { Id = 3, Name = "Karthik Raj", Email = "karthik@example.com", Age = 24, Department = "IT" }
    ];

    public static List<Student> GetAll()
    {
        return Students;
    }

    public static Student? GetById(int id)
    {
        return Students.FirstOrDefault(student => student.Id == id);
    }

    public static List<Student> Search(string? department, int? minimumAge)
    {
        IEnumerable<Student> query = Students;

        if (!string.IsNullOrWhiteSpace(department))
        {
            query = query.Where(student =>
                student.Department.Equals(department, StringComparison.OrdinalIgnoreCase));
        }

        if (minimumAge.HasValue)
        {
            query = query.Where(student => student.Age >= minimumAge.Value);
        }

        return query.ToList();
    }

    public static Student Add(StudentCreateRequest request)
    {
        var student = new Student
        {
            Id = Students.Max(student => student.Id) + 1,
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Department = request.Department
        };

        Students.Add(student);
        return student;
    }
}
