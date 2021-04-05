using System;
using System.Collections.Generic;
using TPPE1.Diagrams;

namespace TPPE1.Sequence
{
    public class Optional : SequenceDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.Optional;

        public SequenceDiagram RepresentedBy { get; set; }
    }
}
