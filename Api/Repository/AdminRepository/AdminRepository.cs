using Api.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        public readonly ApplicationDbContext _Context;
        public AdminRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Admins> AddAdmins(Admins admin)
        {
            var result = await _Context.Admins.AddAsync(admin);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Admins> DeleteAdmins(int Id)
        {
            var result = await _Context.Admins.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Admins.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Admins>> GetAdmins()
        {
            return await _Context.Admins.ToListAsync();
        }

        public async Task<Admins> GetAdmins(int Id)
        {
            return await _Context.Admins.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Admins>> Search(string Name)
        {
            IQueryable<Admins> query = _Context.Admins;
            if (query != null)
            {
                query = query.Where(a => a.Name.Contains(Name));
            }
            return await query.ToListAsync();
        }

        public async Task<Admins> UpdateAdmins(Admins admin)
        {
            var result = await _Context.Admins.Where(a => a.Id == admin.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = admin.Name;
                result.MobileNo = admin.MobileNo;
                result.Email = admin.Email;
                result.Password = admin.Password;
                result.AdminImage = admin.AdminImage;
                _Context.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
