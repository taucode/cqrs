using System;

namespace TauCode.Cqrs.Queries
{
    [Serializable]
    public class CannotCreateQueryHandlerException : Exception
    {
        private const string MESSAGE = "Attempt to create the query handler failed.";

        public CannotCreateQueryHandlerException()
            : this(MESSAGE)
        {
        }

        public CannotCreateQueryHandlerException(Exception innerException)
            : base(MESSAGE, innerException)
        {
        }

        public CannotCreateQueryHandlerException(string message)
            : base(message)
        {
        }

        public CannotCreateQueryHandlerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}