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
    }
}
