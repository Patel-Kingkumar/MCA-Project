using Api.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        public readonly ApplicationDbContext _Context;
        public StudentRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Students> AddStudents(Students student)
        {
            var result = await _Context.Students.AddAsync(student);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Students> DeleteStudents(int Id)
        {
            var result = await _Context.Students.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Students.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Students>> GetStudents()
        {
            return await _Context.Students.ToListAsync();
        }

        public async Task<Students> GetStudents(int Id)
        {
            return await _Context.Students.Include(x => x.BookBorrowing).ThenInclude(y => y.Book).Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Students>> Search(string FirstName)
        {
            IQueryable<Students> query = _Context.Students;
            if (query != null)
            {
                query = query.Where(a => a.FirstName.Contains(FirstName));
            }
            return await query.ToListAsync();
        }

        public async Task<Students> UpdateStudents(Students student)
        {
            var result = await _Context.Students.Where(a => a.Id == student.Id).FirstOrDefaultAsync();
            if (result != null)
            {

                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Age = student.Age;
                result.MobileNo = student.MobileNo;
                result.Email = student.Email;
                result.Password = student.Password;
                result.StudentImage = student.StudentImage;
                _Context.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
