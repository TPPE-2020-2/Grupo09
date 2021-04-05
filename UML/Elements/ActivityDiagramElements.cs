using System;
using TPPE1.Exceptions;

namespace TPPE1.Elements
{
    public abstract class ActivityDiagramElements
    {
        public enum ElementTypes
        {
            None,

            StartNode,
            Activity,
            DecisionNode,
            MergeNode,
            FinalNode,
        }

        public enum TransitionType
        {
            Source,
            Destination
        }

        public string Name { get; set; }
        public int SourceCount { get; set; }
        public int DestinationCount { get; set; }

        public abstract ElementTypes ElementType { get; }

        public virtual bool CheckSource() => true;
        public virtual bool CheckDestination() => true;

        public void AddSource()
        {
            if (!CheckSource())
                throw new ActivityDiagramRuleException();

            SourceCount++;
        }

        public void AddDestination()
        {
            if (!CheckDestination())
                throw new ActivityDiagramRuleException();

            DestinationCount++;
        }
    }
}
