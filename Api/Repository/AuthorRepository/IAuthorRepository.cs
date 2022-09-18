using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.AuthorRepository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Authors>> GetAuthors();
        Task<Authors> GetAuthors(int Id);
        Task<Authors> AddAuthors(Authors author);
        Task<Authors> UpdateAuthors(Authors author);
        Task<Authors> DeleteAuthors(int Id);
        Task<IEnumerable<Books>> Search(string Name);
    }
}
