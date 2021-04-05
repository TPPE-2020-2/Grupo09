using System;

namespace TPPE1.Exceptions
{
    public class ActivityDiagramRuleException : Exception
    {
        public ActivityDiagramRuleException()
        {
        }

        public ActivityDiagramRuleException(string message)
            : base(message)
        {
        }

        public ActivityDiagramRuleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
