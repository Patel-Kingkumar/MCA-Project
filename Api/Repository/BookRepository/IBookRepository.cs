using Api.ViewModel;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookAuthLangViewModel>> GetBooks();
        Task<Books> GetBooks(int Id);
        Task<IEnumerable<Books>> GetMyBooks(int studentId);
        Task<Books> AddBooks(Books book);
        Task<Books> UpdateBooks(Books book);
        Task<Books> UpdateBookQuantity(Books book);
        Task<Books> DeleteBooks(int Id);
        Task<Books> DeleteMyBooks(int bookId);
        Task<IEnumerable<Books>> Search(string Name);
    }
}
