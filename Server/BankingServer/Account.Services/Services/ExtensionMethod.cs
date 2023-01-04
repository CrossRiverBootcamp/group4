using Account.DAL.Interfaces;
using Account.DAL.Repositories;
using Account.DAL.Entities;
using Account.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Account.Services.Services
{
    public static class ExtensionMethod
    {
        public static void ExtensionDI(this IServiceCollection service, string connectionString)
        {
            service.AddDbContextFactory<AccountDBContext>(item => item.UseSqlServer(connectionString));
            service.AddScoped<IAccountRepository, AccountRepository>();
            service.AddScoped<IAccountService, AccountService>();
            service.AddScoped<IAccountSagaService, AccountSagaService>();
            service.AddScoped<IAccountSagaRepository, AccountSagaRepository>();
            service.AddScoped<IOperationService, OperationService>();
            service.AddScoped<IOperationRepository, OperationRepository>();
            service.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();
            service.AddScoped<IEmailVerificationService, EmailVerificationService>();
            service.AddScoped<ICashboxService, CashboxService>();
            service.AddScoped<ICashboxRepository, CashboxRepository>();
        }
    }
}
