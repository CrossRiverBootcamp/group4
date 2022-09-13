using Microsoft.EntityFrameworkCore;

namespace Transaction.DAL.Entities
{
    public  class TransactionDBContext:DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public TransactionDBContext(DbContextOptions<TransactionDBContext> options) : base(options)
        {

        }

    }
}
