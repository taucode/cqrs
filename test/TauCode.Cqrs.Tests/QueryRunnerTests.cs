using Moq;
using NUnit.Framework;
using TauCode.Cqrs.Exceptions;
using TauCode.Cqrs.Queries;

namespace TauCode.Cqrs.Tests
{
    [TestFixture]
    public class QueryRunnerTests
    {
        #region Nested

        public class HelloQuery : Query<HelloQueryResult>
        {
            public string Name { get; set; }
        }

        public class HelloQueryResult
        {
            public string Greeting { get; set; }
        }

        public class Greeter
        {
            public string GetGreeting(string name)
            {
                return $"Hello, {name}!";
            }
        }

        public class HelloQueryHandler : IQueryHandler<HelloQuery>
        {
            private readonly Greeter _greeter;

            public HelloQueryHandler(Greeter greeter)
            {
                _greeter = greeter;
            }

            public void Execute(HelloQuery query)
            {
                var greeting = _greeter.GetGreeting(query.Name);
                var queryResult = new HelloQueryResult
                {
                    Greeting = greeting,
                };
                query.SetResult(queryResult);
            }

            public Task ExecuteAsync(HelloQuery query, CancellationToken cancellationToken)
            {
                this.Execute(query);
                return Task.CompletedTask;
            }
        }

        #endregion

        private Mock<IQueryHandlerFactory> _queryHandlerFactoryMock;
        private Greeter _greeter;

        [SetUp]
        public void SetUp()
        {
            _greeter = new Greeter();
            _queryHandlerFactoryMock = new Mock<IQueryHandlerFactory>();
            _queryHandlerFactoryMock.Setup(x => x.Create<HelloQuery>()).Returns(new HelloQueryHandler(_greeter));
        }

        [Test]
        public void Constructor_ValidArguments_RunsOk()
        {
            // Arrange

            // Act & Assert
            var queryRunner = new QueryRunner(_queryHandlerFactoryMock.Object);
        }

        [Test]
        public void Constructor_QueryHandlerFactoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new QueryRunner(null));
            Assert.That(ex.ParamName, Is.EqualTo("queryHandlerFactory"));
        }

        [Test]
        public void Run_ValidState_RunsOk()
        {
            // Arrange
            IQueryRunner queryRunner = new QueryRunner(_queryHandlerFactoryMock.Object);
            var query = new HelloQuery { Name = "Maria", };

            // Act
            queryRunner.Run(query);

            // Assert
            Assert.That(query.GetResult().Greeting, Is.EqualTo("Hello, Maria!"));
        }

        [Test]
        public void Run_QueryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IQueryRunner queryRunner = new QueryRunner(_queryHandlerFactoryMock.Object);
            HelloQuery query = null;

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => queryRunner.Run(query));
            Assert.That(ex.ParamName, Is.EqualTo("query"));
        }

        [Test]
        public void Run_QueryHandlerFactoryThrowsExceptionOnCreation_ThrowsCannotNotCreateQueryHandlerException()
        {
            // Arrange
            _queryHandlerFactoryMock
                .Setup(x => x.Create<HelloQuery>())
                .Callback(() => throw new InvalidOperationException("internal error"));
            IQueryRunner queryRunner = new QueryRunner(_queryHandlerFactoryMock.Object);
            var query = new HelloQuery() { Name = "Maria", };

            // Act
            var ex = Assert.Throws<CqrsException>(() => queryRunner.Run(query));

            Assert.That(ex.Message, Is.EqualTo($"Failed to create query handler for command of type '{typeof(HelloQuery).FullName}'."));
            var innerEx = ex.InnerException;
            Assert.That(innerEx, Is.Not.Null);
            Assert.That(innerEx, Is.InstanceOf<InvalidOperationException>());
            Assert.That(innerEx.Message, Is.EqualTo("internal error"));
        }

        [Test]
        public void Run_QueryHandlerFactoryFailsToCreateQueryHandler_ThrowsCannotCreateQueryHandlerException()
        {
            // Arrange
            _queryHandlerFactoryMock
                .Setup(x => x.Create<HelloQuery>())
                .Returns((HelloQueryHandler)null);
            IQueryRunner queryRunner = new QueryRunner(_queryHandlerFactoryMock.Object);
            var query = new HelloQuery() { Name = "Maria", };

            // Act
            var ex = Assert.Throws<CqrsException>(() => queryRunner.Run(query));

            Assert.That(ex.Message, Is.EqualTo("'QueryHandlerFactory.Create' returned 'null'."));
            var innerEx = ex.InnerException;
            Assert.That(innerEx, Is.Null);
        }
    }
}
