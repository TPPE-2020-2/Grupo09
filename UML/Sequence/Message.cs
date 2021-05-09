using System;
using System.Collections.Generic;
using System.Linq;

namespace TPPE1.Sequence
{
    public class Message : SequenceDiagramElements
    {
        public enum MessageTypes
        {
            Synchronous,
            Asynchronous,
            Reply
        }

        public override ElementTypes ElementType => ElementTypes.Message;

        public float Prob { get; set; }
        public Lifeline Source { get; set; }
        public Lifeline Target { get; set; }

        public MessageTypes Type { get; set; }

        public override string ToString()
        {
            return $"\t\t<{ElementType} name=\"{Name}\" prob=\"{Prob}\" source=\"{Source.Name}\" target=\"{Target.Name}\"/>\n";
        }
    }

    public class MessageList : List<Message>
    {
        public override string ToString()
        {
            if (Count == 0)
                return null;

            var content = "";

            foreach (var message in this)
            {
                content += message;
            }

            return content;
        }
    }
}
