
using Microsoft.EntityFrameworkCore;
using Account.Services.Interfaces;
using Account.Services.Services;
using NServiceBus;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("Account.Api");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    endpointConfiguration.SendFailedMessagesTo("error");
    //endpointConfiguration.SendOnly();
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(builder.Configuration.GetConnectionString("myPersistenceCon"));
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    persistence.TablePrefix("Account");
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
//builder.Services.AddDbContextFactory<AccountDBContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));
//builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(typeof(Program));

ExtensionMethod.ExtensionDI( builder.Services, builder.Configuration.GetConnectionString("myContextCon"));


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
