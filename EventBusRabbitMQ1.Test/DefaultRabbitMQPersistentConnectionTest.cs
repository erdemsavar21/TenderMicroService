using System;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace EventBusRabbitMQ1.Test
{
    public class DefaultRabbitMQPersistentConnectionTest
    {
        private Mock<IRabbitMQPersistentConnection> _mockPerConncection;
        private DefaultRabbitMQPersistentConnection _defaultRabbitMQPersistentConnection;
        private readonly Mock<ILogger<DefaultRabbitMQPersistentConnection>> _mockLogger;
        private Mock<IConnectionFactory> _mockIConnectionFactory;

        public DefaultRabbitMQPersistentConnectionTest()
        {
            _mockPerConncection = new Mock<IRabbitMQPersistentConnection>();
            _mockIConnectionFactory = new Mock<IConnectionFactory>();
            _mockLogger = new Mock<ILogger<DefaultRabbitMQPersistentConnection>>();
            _defaultRabbitMQPersistentConnection = new DefaultRabbitMQPersistentConnection(_mockIConnectionFactory.Object, 5, _mockLogger.Object);
        }

        [Fact]
        public void RabbitMQConnection_TryConnect_Success()
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            factory.UserName = "guest";

            factory.Password = "guest";


            _defaultRabbitMQPersistentConnection = new DefaultRabbitMQPersistentConnection(factory, 5, _mockLogger.Object);

            var result = _defaultRabbitMQPersistentConnection.TryConnect();

            Assert.True(result);
           
            
        }

        [Fact]
        public void TryConnect_ConnectionFactoryNull_ReturnFalse()
        {

           

            IConnection connection = null;

            _mockIConnectionFactory.Setup(c => c.CreateConnection()).Returns(connection);

            var result = _defaultRabbitMQPersistentConnection.TryConnect();

            Assert.False(result);


        }
    }
}
