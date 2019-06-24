using System;

namespace TauCode.Cqrs.Commands
{
    [Serializable]
    public class CannotCreateCommandHandlerException : Exception
    {
        private const string MESSAGE = "Attempt to create the command handler failed.";

        public CannotCreateCommandHandlerException()
            : this(MESSAGE)
        {
        }

        public CannotCreateCommandHandlerException(Exception innerException)
            : base(MESSAGE, innerException)
        {
        }

        public CannotCreateCommandHandlerException(string message)
            : base(message)
        {
        }

        public CannotCreateCommandHandlerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}