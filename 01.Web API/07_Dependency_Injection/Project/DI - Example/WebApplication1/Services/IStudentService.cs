using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStudentService
    {
        public IEnumerable<Student> GetStudents();
    }
}
