using System;
using System.Collections.Generic;
using TPPE1.Exceptions;
using TPPE1.Sequence;

namespace TPPE1.Diagrams
{
    public class SequenceDiagram
    {
        public bool GuardCondition { get; set; }
        public string Name { get; set; }
        public SequenceRoot SequenceRoot { get; set; }
        public MessageList Messages { get; set; }

        private SequenceDiagram()
        {

        }

        public SequenceDiagram(string name, bool guardCondition, SequenceRoot root)
        {
            Name = name;
            GuardCondition = guardCondition;

            if (!GuardCondition)
                throw new EmptyGuardConditionException();

            SequenceRoot = root;
        }

        public void AddMessage(string name, float prob, string source, string target, Message.MessageTypes messageType)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target) || prob < 0)
                throw new MessageFormatException();

            var lifeLineSource = SequenceRoot.GetLifeline(source);
            var lifeLineTarget = SequenceRoot.GetLifeline(target);

            if (lifeLineSource == null)
                throw new MessageFormatException();

            if (lifeLineTarget == null)
                throw new MessageFormatException();

            if (Messages == null)
                Messages = new MessageList();

            Messages.Add(new Message()
            {
                Name = name,
                Prob = prob,
                Source = lifeLineSource,
                Target = lifeLineTarget,
                Type = messageType
            });
        }

        public override string ToString()
        {
            var content = $"\t<SequenceDiagram name=\"{Name}\">\n";

            content += Messages?.ToString();

            content += $"\t</SequenceDiagram>\n";

            return content;
        }
    }

    public class SequenceDiagramDict : Dictionary<string, SequenceDiagram>
    {
        public override string ToString()
        {
            if (Count == 0)
                return null;

            var content = "";
            foreach (var diagram in this)
            {
                content += diagram.Value;
            }

            return content;
        }
    }
}
