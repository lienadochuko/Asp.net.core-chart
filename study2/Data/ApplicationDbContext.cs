using Microsoft.EntityFrameworkCore;
using study2.Models;
using System.Transactions;

namespace study2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transactions> Transactiones { get; set; }
        public DbSet<Category> Categories { get; set; } 
    }
}
