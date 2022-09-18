using Api.ViewModel;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.BorrowingRepository
{ 
    public interface IBorrowingRepository
    {
        public Task<IEnumerable<BorrowingStudBookViewModel>> GetBorrowings();
        public Task<Borrowings> GetBorrowings(int bookId);
        public Task<Borrowings> AddBorrowings(Borrowings borrowing);
        public Task<Borrowings> UpdateBorrowings(Borrowings borrowing);
        public Task<Borrowings> DeleteBorrowings(int sid, int bookid);
        //Task<IEnumerable<Borrowings>> Search(int StudentId);
    }
}
