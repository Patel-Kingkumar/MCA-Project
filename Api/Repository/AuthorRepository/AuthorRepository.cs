using Api.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly ApplicationDbContext _Context;
        public AuthorRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Authors> AddAuthors(Authors author)
        {
            var result = await _Context.Authors.AddAsync(author);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Authors> DeleteAuthors(int Id)
        {
            var result = await _Context.Authors.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Authors.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Authors>> GetAuthors()
        {
            return await _Context.Authors.ToListAsync();
        }

        public async Task<Authors> GetAuthors(int Id)
        {
            return await _Context.Authors.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Books>> Search(string Name)
        {
            IQueryable<Books> query = _Context.Books;
            if (query != null)
            {
                query = query.Where(a => a.Author.Name.Contains(Name));
                foreach (var book in query)
                {
                    var author = await _Context.Authors.Where(x => x.Id == book.AuthorId).FirstOrDefaultAsync();
                    var language = await _Context.Languages.Where(x => x.Id == book.LanguageId).FirstOrDefaultAsync();
                    book.Author = author;
                    book.Language = language;
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Authors> UpdateAuthors(Authors author)
        {
            var result = await _Context.Authors.Where(a => a.Id == author.Id).FirstOrDefaultAsync();
            if (result != null)
            {

                result.Name = author.Name;                
                _Context.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
