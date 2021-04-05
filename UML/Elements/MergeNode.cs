using System;

namespace TPPE1.Elements
{
    public class MergeNode : ActivityDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.MergeNode;

        public override bool CheckSource() => (SourceCount != 0) ? false : true;
    }
}
