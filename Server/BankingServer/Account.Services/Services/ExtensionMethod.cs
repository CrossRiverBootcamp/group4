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

namespace Account.Services.Services
{
    public class ExtensionMethod
    {
        public void ExtensionDI(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<AccountDBContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();

        }
    }
}
