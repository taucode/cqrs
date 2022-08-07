namespace TauCode.Cqrs.Exceptions
{
    [Serializable]
    public class CqrsException : Exception
    {
        public CqrsException(string message)
            : base(message)
        {
        }

        public CqrsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
