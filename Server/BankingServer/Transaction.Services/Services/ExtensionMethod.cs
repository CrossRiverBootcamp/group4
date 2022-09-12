using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Entities;
using Transaction.DAL.Interfaces;
using Transaction.DAL.Repositories;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Services
{
    public  class ExtensionMethod
    {
        public static void ExtensionDI(IServiceCollection service, string connectionString)
        {
            service.AddDbContextFactory<TransactionDBContext>(item => item.UseSqlServer(connectionString));
            service.AddScoped<ITransactionService, TransactionService>();
            service.AddScoped<ITransactionRepository, TransactionRepository>();
            service.AddScoped<IUpdateTransactionStatusService, UpdateTransactionStatusService>();
            service.AddScoped<IUpdateTransactionStatusRepository, UpdateTransactionStatusRepository>();
        }
    }
}
