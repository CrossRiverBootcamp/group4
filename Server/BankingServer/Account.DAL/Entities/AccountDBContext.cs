using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Entities
{
    internal class AccountDBContext:DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; } 
        public DbSet<AccountEntity> Accounts { get; set; }

    }
}
