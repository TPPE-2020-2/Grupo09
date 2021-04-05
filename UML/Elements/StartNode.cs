using System;

namespace TPPE1.Elements
{
    public class StartNode : ActivityDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.StartNode;

        public override bool CheckSource() => (SourceCount == 0) ? true : false;

        public override bool CheckDestination() => false;
    }
}
