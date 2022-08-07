using Moq;
using NUnit.Framework;
using TauCode.Cqrs.Abstractions;
using TauCode.Cqrs.Commands;
using TauCode.Cqrs.Exceptions;

namespace TauCode.Cqrs.Tests
{
    [TestFixture]
    public class CommandDispatcherTests
    {
        #region Nested

        public class FooCommand : ICommand
        {
            public string Name { get; set; }
        }

        public class FooNameHolder
        {
            private string _name;

            public void SetName(string name)
            {
                _name = name;
            }

            public string GetName()
            {
                return _name;
            }
        }

        public class FooCommandHandler : ICommandHandler<FooCommand>
        {
            private readonly FooNameHolder _nameHolder;

            public FooCommandHandler(FooNameHolder nameHolder)
            {
                _nameHolder = nameHolder;
            }

            public void Execute(FooCommand command)
            {
                _nameHolder.SetName(command.Name);
            }

            public Task ExecuteAsync(FooCommand command, CancellationToken cancellationToken)
            {
                this.Execute(command);
                return Task.CompletedTask;
            }
        }

        #endregion

        private Mock<ICommandHandlerFactory> _commandHandlerFactoryMock;
        private FooNameHolder _nameHolder;

        [SetUp]
        public void SetUp()
        {
            _nameHolder = new FooNameHolder();
            _commandHandlerFactoryMock = new Mock<ICommandHandlerFactory>();
            _commandHandlerFactoryMock.Setup(x => x.Create<FooCommand>()).Returns(new FooCommandHandler(_nameHolder));
        }

        [Test]
        public void Constructor_ValidArguments_RunsOk()
        {
            // Arrange

            // Act & Assert
            var commandDispatcher = new CommandDispatcher(_commandHandlerFactoryMock.Object);
        }

        [Test]
        public void Constructor_CommandHandlerFactoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CommandDispatcher(null));
            Assert.That(ex.ParamName, Is.EqualTo("commandHandlerFactory"));
        }

        [Test]
        public void Dispatch_ValidState_RunsOk()
        {
            // Arrange
            ICommandDispatcher commandDispatcher = new CommandDispatcher(_commandHandlerFactoryMock.Object);
            var command = new FooCommand { Name = "Maria", };

            // Act
            commandDispatcher.Dispatch(command);

            // Assert
            Assert.That(_nameHolder.GetName(), Is.EqualTo("Maria"));
        }

        [Test]
        public void Dispatch_CommandIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ICommandDispatcher commandDispatcher = new CommandDispatcher(_commandHandlerFactoryMock.Object);

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => commandDispatcher.Dispatch((FooCommand)null));
            Assert.That(ex.ParamName, Is.EqualTo("command"));
        }

        [Test]
        public void Dispatch_CommandHandlerFactoryThrowsExceptionOnCreation_ThrowsCannotCreateCommandHandlerException()
        {
            // Arrange
            _commandHandlerFactoryMock
                .Setup(x => x.Create<FooCommand>())
                .Callback(() => throw new InvalidOperationException("internal error"));
            ICommandDispatcher commandDispatcher = new CommandDispatcher(_commandHandlerFactoryMock.Object);
            var command = new FooCommand { Name = "Maria", };

            // Act
            var ex = Assert.Throws<CqrsException>(() => commandDispatcher.Dispatch(command));

            Assert.That(ex.Message, Is.EqualTo($"Failed to create command handler for command of type '{typeof(FooCommand).FullName}'."));
            var innerEx = ex.InnerException;
            Assert.That(innerEx, Is.Not.Null);
            Assert.That(innerEx, Is.InstanceOf<InvalidOperationException>());
            Assert.That(innerEx.Message, Is.EqualTo("internal error"));
        }

        [Test]
        public void Dispatch_CommandHandlerFactoryFailsToCreateCommandHandler_ThrowsCannotCreateCommandHandlerException()
        {
            // Arrange
            _commandHandlerFactoryMock
                .Setup(x => x.Create<FooCommand>())
                .Returns((FooCommandHandler)null);
            ICommandDispatcher commandDispatcher = new CommandDispatcher(_commandHandlerFactoryMock.Object);
            var command = new FooCommand { Name = "Maria", };

            // Act
            var ex = Assert.Throws<CqrsException>(() => commandDispatcher.Dispatch(command));

            Assert.That(ex.Message, Is.EqualTo("'CommandHandlerFactory.Create' returned 'null'."));
            var innerEx = ex.InnerException;
            Assert.That(innerEx, Is.Null);
        }
    }
}
