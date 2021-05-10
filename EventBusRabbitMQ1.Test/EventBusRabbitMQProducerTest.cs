using System;
using EventBusRabbitMQ1.Events;
using EventBusRabbitMQ1.Producer;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace EventBusRabbitMQ1.Test
{
    public class EventBusRabbitMQProducerTest
    {
        private readonly Mock<IRabbitMQPersistentConnection> _mockPersistentConnection;
        private readonly Mock<ILogger<EventBusRabbitMQProducer>> _mockLogger;
        private readonly int _retryCount;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        private readonly Mock<ILogger<DefaultRabbitMQPersistentConnection>> _mockLoggerConn;
        private DefaultRabbitMQPersistentConnection _defaultRabbitMQPersistentConnection;

        public EventBusRabbitMQProducerTest()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            factory.UserName = "guest";

            factory.Password = "guest";
            _mockLoggerConn = new Mock<ILogger<DefaultRabbitMQPersistentConnection>>();
            _defaultRabbitMQPersistentConnection = new DefaultRabbitMQPersistentConnection(factory, 5, _mockLoggerConn.Object);
            _mockPersistentConnection = new Mock<IRabbitMQPersistentConnection>();
            _mockLogger = new Mock<ILogger<EventBusRabbitMQProducer>>();
            _retryCount = 5;
            _eventBusRabbitMQProducer = new EventBusRabbitMQProducer(_mockPersistentConnection.Object, _mockLogger.Object, _retryCount);

        }

        [Fact]
        public void EventBusRabbitMQProducer_Publish_Success()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "dummy1";
            eventMessage.ProductId = "dummy_product_1";
            eventMessage.Price = 10;
            eventMessage.Quantity = 100;
            eventMessage.SellerUserName = "test@test.com";

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            factory.UserName = "guest";

            factory.Password = "guest";




            _defaultRabbitMQPersistentConnection.TryConnect();

            //_eventBusRabbitMQProducer.Publish("orderCreateQueue", eventMessage);
        }

    }
}
