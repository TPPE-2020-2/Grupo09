using System;
using System.Collections.Generic;

namespace TPPE1.Sequence
{
    public class Lifeline : SequenceDiagramElements
    {
        public override ElementTypes ElementType => ElementTypes.Lifeline;

        public override string ToString()
        {
            return $"\t\t<{ElementType} name=\"{Name}\"/>\n";
        }
    }

    public class LifelineList : List<Lifeline>
    {
        public override string ToString()
        {
            if (Count == 0)
                return null;

            var content = "";
            content += $"\t<Lifelines>\n";

            foreach (var lifeline in this)
            {
                content += lifeline;
            }

            content += $"\t</Lifelines>\n";

            return content;
        }
    }
}
