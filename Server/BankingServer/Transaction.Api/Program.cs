
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using System.Data.SqlClient;
using Transaction.DAL.Entities;
using Transaction.DAL.Interfaces;
using Transaction.DAL.Repositories;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("Transaction");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    //endpointConfiguration.SendOnly();
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(@"Data Source=.;Initial Catalog=BankPersistence;Integrated Security=True");
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("dbo");
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString("host=localhost");
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    return endpointConfiguration;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<TransactionDBContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
