using System;
using System.Collections.Generic;
using TPPE1.Diagrams;

namespace TPPE1.Sequence
{
    public class Optional : SequenceDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.Optional;

        public SequenceDiagram RepresentedBy { get; set; }

        public override string ToString()
        {
            return $"\t\t<{ElementType} name=\"{Name}\" representedBy=\"{RepresentedBy.Name}\"/>\n";
        }
    }

    public class OptionalList : List<Optional>
    {
        public override string ToString()
        {
            if (Count == 0)
                return null;

            var content = "";
            content += $"\t<Fragments>\n";

            foreach (var optional in this)
            {
                content += optional;
            }

            content += $"\t</Fragments>\n";

            return content;
        }
    }
}
