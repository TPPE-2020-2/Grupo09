using System;

namespace TPPE1.Exceptions
{
    public class ActivityRepresentationException : Exception
    {
        public ActivityRepresentationException()
        {
        }

        public ActivityRepresentationException(string message)
            : base(message)
        {
        }

        public ActivityRepresentationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
