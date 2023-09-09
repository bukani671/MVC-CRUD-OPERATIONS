using Microsoft.EntityFrameworkCore;
using MVC_CRUD_APP.Models.Domain;

namespace MVC_CRUD_APP.Data
{
    public class MVCCrudDbContext : DbContext
    {
        public MVCCrudDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
    }
}
