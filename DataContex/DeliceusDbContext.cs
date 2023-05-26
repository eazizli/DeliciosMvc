using DeliciousMvC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliciousMvC.DataContex
{
    public class DeliceusDbContext :IdentityDbContext<AppUser>
    {
        public DeliceusDbContext(DbContextOptions<DeliceusDbContext> opt):base(opt)
        {

        }
      public DbSet<Chefs> Chefs { get; set; }
    }
}
