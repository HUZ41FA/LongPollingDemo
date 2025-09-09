using LongPollingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace LongPollingDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Message> Messages { get; set; }
    }
}
