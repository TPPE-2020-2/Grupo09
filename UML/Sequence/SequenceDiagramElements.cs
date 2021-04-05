using System;
using System.Collections.Generic;
using System.Linq;

namespace TPPE1.Sequence
{
    public abstract class SequenceDiagramElements
    {
        public enum ElementTypes
        {
            None,

            Lifeline,
            Optional,
            Fragment,
            Message,
        }

        public string Name { get; set; }

        public abstract ElementTypes ElementType { get; }
    }
}
