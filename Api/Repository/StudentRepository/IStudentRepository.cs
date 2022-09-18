using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.StudentRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Students>> GetStudents();
        Task<Students> GetStudents(int Id);
        Task<Students> AddStudents(Students student);
        Task<Students> UpdateStudents(Students student);
        Task<Students> DeleteStudents(int Id);
        Task<IEnumerable<Students>> Search(string Name);
    }
}
