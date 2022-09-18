using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.LanguageRepository
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Languages>> GetLanguages();
        Task<Languages> GetLanguages(int Id);
        Task<Languages> AddLanguages(Languages language);
        Task<Languages> UpdateLanguages(Languages language);
        Task<Languages> DeleteLanguages(int Id);
        Task<IEnumerable<Books>> Search(string language);
    }
}
