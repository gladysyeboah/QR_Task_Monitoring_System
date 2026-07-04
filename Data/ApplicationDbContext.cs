using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QR_Field_Monitoring_System.Models;

namespace QR_Field_Monitoring_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FieldTask> FieldTasks { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Photo> Photos { get; set; }
    }
}