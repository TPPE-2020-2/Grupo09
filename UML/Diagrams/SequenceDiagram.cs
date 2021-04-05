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
        public List<Message> Messages { get; set; }

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
                Messages = new List<Message>();

            Messages.Add(new Message()
            {
                Name = name,
                Prob = prob,
                Source = lifeLineSource,
                Target = lifeLineTarget,
                Type = messageType
            });
        }
    }
}
