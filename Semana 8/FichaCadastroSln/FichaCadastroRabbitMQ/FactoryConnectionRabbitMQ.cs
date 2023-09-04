using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    public class FactoryConnectionRabbitMQ : IFactoryConnectionRabbitMQ
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly ConnectionFactory _connectionFactory;

        public FactoryConnectionRabbitMQ()
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };
        }

        public IModel CreateConnection(string virtualHost)
        {
            _connectionFactory.VirtualHost = virtualHost;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            return _channel;
        }
    }
}