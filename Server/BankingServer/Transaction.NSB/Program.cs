using Messages;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        Console.Title = "Transaction.NSB";
        var endpointConfiguration = new EndpointConfiguration("Transaction.NSB");
        endpointConfiguration.EnableOutbox();
        endpointConfiguration.EnableInstallers();
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(@"Data Source=.;Initial Catalog=BankPersistence;Integrated Security=True");
        });
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        transport.ConnectionString("host=localhost");
        var routing = transport.Routing();
        routing.RouteToEndpoint(assembly: typeof(TransactionPayload).Assembly, destination: "Account.NSB");
    }
}