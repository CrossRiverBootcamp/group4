using Account.DAL.Entities;
using Account.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task RemoveEmailVerification(EmailVerificationEntity emailVerificationEntity)
        {
            using var context = _factory.CreateDbContext();
            context.Verifications.Remove(emailVerificationEntity);
            await context.SaveChangesAsync();
        }

       public async Task<bool> CheckVerificationAsync(string email, string verificationCode)
        {
            using var context = _factory.CreateDbContext();
            return await context.Verifications.AnyAsync(v=>v.Email.Equals(email)&&v.VerificationCode.Equals(verificationCode)&&v.ExpirationTime>=DateTime.UtcNow);
        }
        public async Task ResendCodeForExistingEmail(string email)
        {
            using var context = _factory.CreateDbContext();
            var verification=await context.Verifications.FirstOrDefaultAsync(v=>v.Email.Equals(email));
            if (verification != null)
            {
                context.Remove(verification);
                await context.SaveChangesAsync();
            }
        }
    }
}