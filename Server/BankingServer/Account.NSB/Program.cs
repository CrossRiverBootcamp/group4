using NServiceBus;
using System.Data.SqlClient;
class Program
{
    static async Task Main()
    {
        Console.Title = "Account.NSB";
        var endpointConfiguration = new EndpointConfiguration("Account.NSB");
        endpointConfiguration.EnableOutbox();
        endpointConfiguration.EnableInstallers();
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        transport.ConnectionString("host=localhost");
        var connection = @"Data Source=.;Initial Catalog=BankPersistence;Integrated Security=True";
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        //var subscriptions = persistence.SubscriptionSettings();
        //subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(connection);
            });
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        persistence.TablePrefix("Account");
        var endpointInstance = await Endpoint.Start(endpointConfiguration);
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}