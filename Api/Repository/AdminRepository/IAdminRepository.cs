using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.AdminRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admins>> GetAdmins();
        Task<Admins> GetAdmins(int Id);
        Task<Admins> AddAdmins(Admins admin);
        Task<Admins> UpdateAdmins(Admins admin);
        Task<Admins> DeleteAdmins(int Id);
        Task<IEnumerable<Admins>> Search(string Name);
    }
}
