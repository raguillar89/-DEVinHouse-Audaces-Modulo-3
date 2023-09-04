using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FichaCadastroRabbitMQ
{
    public interface IMessageRabbitMQ
    {
        ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }
        IModel ConfigureVirtualHost();
        void ExchangeDeclare();
        void QueueDeclare();
        void QueueBind();
        void BasicPublish();
        void BasicConsume(IBasicConsumer basicConsumer);
        EventingBasicConsumer InstanciarEventingBasicConsumer();
    }
}
