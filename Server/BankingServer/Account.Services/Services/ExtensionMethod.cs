using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.DAL.Interfaces;
using Account.DAL.Repositories;
using Account.DAL.Entities;
using Account.Services.Services;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Account.Services.Services
{
    public class ExtensionMethod
    {
        public static void ExtensionDI(IServiceCollection service,string connectionString)
        {
          service.AddDbContextFactory<AccountDBContext>(item=>item.UseSqlServer(connectionString));
          service.AddScoped<IAccountRepository, AccountRepository>();
          service.AddScoped<IAccountService, AccountService>();
          service.AddScoped<IAccountSagaService, AccountSagaService>();
          service.AddScoped<IAccountSagaRepository, AccountSagaRepository>();
          service.AddScoped<IOperationService, OperationService>();
          service.AddScoped<IOperationRepository, OperationRepository>();
        }
    }
}
