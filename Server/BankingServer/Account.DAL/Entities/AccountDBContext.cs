using Microsoft.EntityFrameworkCore;

namespace Account.DAL.Entities
{
    public class AccountDBContext:DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; } 
        public DbSet<AccountEntity> Accounts { get; set; }
        public AccountDBContext(DbContextOptions options) :base(options)
        {

        }

    }
}
