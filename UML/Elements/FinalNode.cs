using System;

namespace TPPE1.Elements
{
    public class FinalNode : ActivityDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.FinalNode;

        public override bool CheckSource() => false;
    }
}
