using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Repositories
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly IDbContextFactory<AccountDBContext> _factory;
        public EmailVerificationRepository(IDbContextFactory<AccountDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddEmailVerification(EmailVerificationEntity emailVerificationEntity)
        {
            using var context = _factory.CreateDbContext();
            await context.Verifications.AddAsync(emailVerificationEntity);
            await context.SaveChangesAsync();
        }
    }
}
