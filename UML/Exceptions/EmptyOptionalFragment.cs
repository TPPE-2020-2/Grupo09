using System;

namespace TPPE1.Exceptions
{
    public class EmptyOptionalFragment : Exception
    {
        public EmptyOptionalFragment()
        {
        }

        public EmptyOptionalFragment(string message)
            : base(message)
        {
        }

        public EmptyOptionalFragment(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
