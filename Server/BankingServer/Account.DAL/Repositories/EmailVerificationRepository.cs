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
            return await context.Verifications.AnyAsync(v=>v.Email.Equals(email)&&v.VerificationCode.Equals(verificationCode));
        }
        public async Task ResendCodeForExistingEmail(string email)
        {
            //checks if verification code was already entered into the db and if it was removes it so a new one can be sent 
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