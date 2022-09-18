using Api.DataContext;
using Api.ViewModel;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.BookRepository
{
    public class BookRepository : IBookRepository
    {
        public readonly ApplicationDbContext _Context;
        public BookRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Books> AddBooks(Books book)
        {
            var result = await _Context.Books.AddAsync(book);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Books> DeleteBooks(int Id)
        {
            var result = await _Context.Books.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Books.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Books> GetBooks(int Id)
        {
            return await _Context.Books.Include(x => x.Author).Where(a => a.Id == Id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<BookAuthLangViewModel>> GetBooks()
        {
            List<BookAuthLangViewModel> balv = new List<BookAuthLangViewModel>();
            var books = await _Context.Books.ToListAsync();

            foreach (var book in books)
            {
                var author = await _Context.Authors.Where(x => x.Id == book.AuthorId).FirstOrDefaultAsync();
                var language = await _Context.Languages.Where(x => x.Id == book.LanguageId).FirstOrDefaultAsync();
                balv.Add(new BookAuthLangViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    BookName = book.BookName,
                    Publisher = book.Publisher,
                    PublishDate = book.PublishDate,
                    BookImage = book.BookImage,
                    Description = book.Description,
                    AuthorName = book.Author.Name,
                    Languages = book.Language.Language,
                    Quantity = book.Quantity,                    
                    PageNo = book.PageNo                    
                });
            }
            return balv;
        }

        public async Task<IEnumerable<Books>> Search(string Title)
        {
            IQueryable<Books> query = _Context.Books;
            if (query != null)
            {
                query = query.Where(a => a.Title.Contains(Title));
                foreach(var book in query)
                {
                    var author = await _Context.Authors.Where(x => x.Id == book.AuthorId).FirstOrDefaultAsync();
                    var language = await _Context.Languages.Where(x => x.Id == book.LanguageId).FirstOrDefaultAsync();
                    book.Author = author;
                    book.Language = language;
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Books> UpdateBooks(Books book)
        {
            var result = await _Context.Books.Where(a => a.Id == book.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Title = book.Title;
                result.BookName = book.BookName;
                result.Author.Name = book.Author.Name;
                result.Publisher = book.Publisher;
                result.PublishDate = book.PublishDate;
                result.BookImage = book.BookImage;
                result.Description = book.Description;
                result.Language.Language = book.Language.Language;
                result.PageNo = book.PageNo;
                result.Quantity = book.Quantity;
                _Context.SaveChanges();
                return result;
            }
            return null;
        }

        public async Task<Books> UpdateBookQuantity(Books book)
        {
            var result = await _Context.Books.Where(a => a.Id == book.Id).FirstOrDefaultAsync();
            if (result != null)
            { 
                result.Quantity = book.Quantity;
                _Context.SaveChanges();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Books>> GetMyBooks(int studentId)
        {
            List<Books> BookList = new List<Books>();
            var borrowingBooks = await _Context.Borrowings.Where(x => x.StudentId == studentId).Select(x => x.BookId).ToListAsync();
            foreach (var book in borrowingBooks)
            {
                var mybook = await _Context.Books.Where(x => x.Id == book).FirstOrDefaultAsync();
                BookList.Add(mybook);
            }
            return BookList;
        }

        public async Task<Books> DeleteMyBooks(int bookId)
        {
            var deletingBooks = await _Context.Books.Where(x => x.Id == bookId)./*Select(x => x.Id).*/FirstOrDefaultAsync();
            if (deletingBooks != null)
            {
                _Context.Books.Remove(deletingBooks);
                await _Context.SaveChangesAsync();                
            }
            return null;
        }
       
    }
}
