using DataAccessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Areas.Client.DataContext
{
    public class AuthDataDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDataDbContext(DbContextOptions<AuthDataDbContext> options) :  base(options)
        {
        }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Borrowings> Borrowings { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Students> Students { get; set; }
    }
}
