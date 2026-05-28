using WebApplication1.Models;
using WebApplication1.Services; 

namespace WebApplication1.Implementation
{
    public class StudentService1
    {
        public List<Student> Students = new List<Student>()
        {
            new Student() { Id = 1, Name = "John Doe", Email = "Student1@gmail.com" },
            new Student() { Id = 2, Name = "Jane Smith", Email = "Student2@gmail.com" },
            new Student() { Id = 3, Name = "Michael Johnson", Email =  "Student3@gmail.com" }
        };


        public IEnumerable<Student> GetStudents()
        {
            return Students;
        }

    }
 
}
