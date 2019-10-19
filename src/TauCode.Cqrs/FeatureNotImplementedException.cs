using System;

namespace TauCode.Cqrs
{
    [Serializable]
    public class FeatureNotImplementedException : Exception
    {
        public FeatureNotImplementedException()
            : this("Feature is not implemented.")
        {
        }

        public FeatureNotImplementedException(string message)
            : base(message)
        {
        }

        public FeatureNotImplementedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
