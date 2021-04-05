using System;

namespace TPPE1.Elements
{
    public class DecisionNode : ActivityDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.DecisionNode;

        public override bool CheckDestination() => (DestinationCount != 0) ? false : true;
    }
}
