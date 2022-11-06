
using Microsoft.EntityFrameworkCore;
using Account.Services.Interfaces;
using Account.Services.Services;
using NServiceBus;
using System.Data.SqlClient;
using Account.Api;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("Account.Api");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    endpointConfiguration.SendFailedMessagesTo("error");
    //Configure number of retries
    var recoverability = endpointConfiguration.Recoverability();
    recoverability.Immediate(
        immediate =>
        {
            immediate.NumberOfRetries(2);
        });

    recoverability.Delayed(
        delayed =>
        {
            delayed.NumberOfRetries(3);
            delayed.TimeIncrease(TimeSpan.FromSeconds(10));
        });
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(builder.Configuration.GetConnectionString("myPersistenceCon"));
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    persistence.TablePrefix("Account");
    dialect.Schema("dboAccount");
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(builder.Configuration.GetConnectionString("NSB"));
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    return endpointConfiguration;
});
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<InitBalance>(builder.Configuration.GetSection(nameof(InitBalance)));
//Extension method for dependency injection
ExtensionMethod.ExtensionDI( builder.Services, builder.Configuration.GetConnectionString("myContextCon"));
IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration
            (configuration).CreateLogger();

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
