﻿using Microsoft.EntityFrameworkCore;

namespace Account.DAL.Entities
{
    public class AccountDBContext:DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; } 
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<OperationEntity> Operations { get; set; }
        public DbSet<EmailVerificationEntity> Verifications { get; set; }
        public DbSet<CashboxEntity> Cashboxes { get; set; }
        public AccountDBContext(DbContextOptions<AccountDBContext> options) :base(options)
        {

        }
    }
}
