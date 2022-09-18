using Api.DataContext;
using Api.ViewModel;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.BorrowingRepository
{
    public class BorrowingRepository : IBorrowingRepository
    {
        public readonly ApplicationDbContext _Context;
        public BorrowingRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Borrowings> AddBorrowings(Borrowings borrowing)
        {
            var result = await _Context.Borrowings.AddAsync(borrowing);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Borrowings> DeleteBorrowings(int sid,int bookid)
        {
            var result = await _Context.Borrowings.Where(a => a.StudentId == sid && a.BookId == bookid).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Borrowings.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<BorrowingStudBookViewModel>> GetBorrowings()        
        {
            //return await _Context.Borrowings.ToListAsync();
            List<BorrowingStudBookViewModel> bsbv = new List<BorrowingStudBookViewModel>();
            var borrows = await _Context.Borrowings.ToListAsync();
            foreach (var borrow in borrows)
            {
                var student = await _Context.Students.Where(x => x.Id == borrow.StudentId).FirstOrDefaultAsync();
                var book = await _Context.Books.Where(x => x.Id == borrow.BookId).FirstOrDefaultAsync();
                bsbv.Add(new BorrowingStudBookViewModel()
                {
                    Id = borrow.Id,
                    StudentId = borrow.StudentId,
                    BookId = borrow.BookId,
                    RetriveDate = borrow.RetriveDate,
                    DueDate = borrow.DueDate,
                    SubmitDate = borrow.SubmitDate,
                    StudentName = borrow.Student.FirstName,
                    Books = borrow.Book.BookName
                });
            }
            return bsbv;
        }

        public async Task<Borrowings> GetBorrowings(int bookId)
        {
            return await _Context.Borrowings.Where(a => a.BookId == bookId).FirstOrDefaultAsync(); // 16
        }

        public async Task<Borrowings> UpdateBorrowings(Borrowings borrowing) // 05-08-2022
        {
            var result = await _Context.Borrowings.Where(a => a.Id == borrowing.Id).FirstOrDefaultAsync(); // 16
            if (result != null)
            {
                result.Id = borrowing.Id; 
                result.SubmitDate = borrowing.SubmitDate;
                _Context.SaveChanges();
                return result;                
             }
            return null;
        }
    }
}
