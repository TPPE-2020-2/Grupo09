using System;

namespace TPPE1.Exceptions
{
    public class EmptyGuardConditionException : Exception
    {
        public EmptyGuardConditionException()
        {
        }

        public EmptyGuardConditionException(string message)
            : base(message)
        {
        }

        public EmptyGuardConditionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
