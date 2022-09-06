using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
