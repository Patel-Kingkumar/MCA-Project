using Api.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.LanguageRepository
{
    public class LanguageRepository : ILanguageRepository
    {
        public readonly ApplicationDbContext _Context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Languages> AddLanguages(Languages language)
        {
            var result = await _Context.Languages.AddAsync(language);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Languages> DeleteLanguages(int Id)
        {
            var result = await _Context.Languages.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Languages.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Languages>> GetLanguages()
        {
            return await _Context.Languages.ToListAsync();
        }

        public async Task<Languages> GetLanguages(int Id)
        {
            return await _Context.Languages.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Books>> Search(string language)
        {
            IQueryable<Books> query = _Context.Books;
            if (query != null)
            {
                query = query.Where(a => a.Language.Language.Contains(language));
                foreach (var book in query)
                {
                    var author = await _Context.Authors.Where(x => x.Id == book.AuthorId).FirstOrDefaultAsync();
                    var languages = await _Context.Languages.Where(x => x.Id == book.LanguageId).FirstOrDefaultAsync();
                    book.Author = author;
                    book.Language = languages;
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Languages> UpdateLanguages(Languages language)
        {
            var result = await _Context.Languages.Where(a => a.Id == language.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Language = language.Language;                
                _Context.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
