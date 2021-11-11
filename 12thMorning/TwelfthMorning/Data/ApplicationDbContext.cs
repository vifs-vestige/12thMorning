using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TwelfthMorning.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Blog> Blog { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}