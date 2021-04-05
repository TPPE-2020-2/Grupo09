using System;

namespace TPPE1.Exceptions
{
    public class MessageFormatException : Exception
    {
        public MessageFormatException()
        {
        }

        public MessageFormatException(string message)
            : base(message)
        {
        }

        public MessageFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
