using System;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using Tender.Order.Consumers;
using Xunit;

namespace Tender.Order.Test
{
    public class EventBusOrderCreateConsumerTest
    {
        private readonly Mock<IRabbitMQPersistentConnection> _mockPersistentConnection;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private EventBusOrderCreateConsumer _eventBusOrderCreateConsumer;
        private readonly Mock<ILogger<DefaultRabbitMQPersistentConnection>> _mockLogger;

        public EventBusOrderCreateConsumerTest()
        {
            _mockPersistentConnection = new Mock<IRabbitMQPersistentConnection>();
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _eventBusOrderCreateConsumer = new EventBusOrderCreateConsumer(_mockPersistentConnection.Object, _mockMediator.Object, _mockMapper.Object);
            _mockLogger = new Mock<ILogger<DefaultRabbitMQPersistentConnection>>();
        }

        [Fact]
        public void EventBusOrderCreateConsumer_Publish_Success()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            factory.UserName = "guest";

            factory.Password = "guest";


            var _defaultRabbitMQPersistentConnection = new DefaultRabbitMQPersistentConnection(factory, 5, _mockLogger.Object);

            _mockPersistentConnection.Setup(p => p.TryConnect()).Returns(_defaultRabbitMQPersistentConnection.TryConnect());

            //_defaultRabbitMQPersistentConnection.TryConnect();

     
            
            //_eventBusOrderCreateConsumer.Consume();

        }
    }
}
