namespace DataAccess.Exceptions
{
    using System;


    [Serializable]
    public class AccessViolationException : ApplicationException
    {
        public AccessViolationException() { }
        public AccessViolationException(string message) : base(message) { }
        public AccessViolationException(string message, Exception inner) : base(message, inner) { }
        protected AccessViolationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
