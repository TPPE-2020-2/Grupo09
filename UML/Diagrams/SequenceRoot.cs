using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPPE1.Elements;
using TPPE1.Exceptions;
using TPPE1.Sequence;

namespace TPPE1.Diagrams
{
    public class SequenceRoot
    {
        public ActivityDiagram ActivityDiagram { get; set; }

        public LifelineList Lifelines { get; set; }
        public List<Fragment> Fragments { get; set; }
        public OptionalList Optionals { get; set; }

        public SequenceDiagramDict Diagrams { get; set; }

        private SequenceRoot()
        {

        }

        public SequenceRoot(ActivityDiagram activityDiagram)
        {
            ActivityDiagram = activityDiagram;
        }

        public void AddLifeline(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ActivityDiagramRuleException();

            if (Lifelines == null)
                Lifelines = new LifelineList();

            Lifelines.Add(new Lifeline()
            {
                Name = name
            });
        }

        public Lifeline GetLifeline(string name)
        {
            if (Lifelines == null)
                return null;

            foreach (var lifeline in Lifelines)
            {
                if (lifeline.Name == name)
                {
                    return lifeline;
                }
            }

            return null;
        }

        public void AddDiagram(string name, bool guardCondition)
        {
            if (string.IsNullOrEmpty(name))
                throw new MessageFormatException();

            if (ActivityDiagram == null)
                throw new Exception();

            if (Diagrams == null)
                Diagrams = new SequenceDiagramDict();

            var activity = ActivityDiagram.GetDiagramElement(name, ActivityDiagramElements.ElementTypes.Activity);
            if (activity == null)
                throw new ActivityRepresentationException();

            if (Diagrams.ContainsKey(name))
                throw new ActivityRepresentationException();

            Diagrams.Add(name, new SequenceDiagram(name, guardCondition, this));
        }

        public SequenceDiagram GetDiagram(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new MessageFormatException();

            Diagrams.TryGetValue(name, out var sequence);
            return sequence;
        }

        public void AddMessage(string diagram, string name, float prob, string source, string target, Message.MessageTypes messageType)
        {
            if (string.IsNullOrEmpty(diagram))
                throw new MessageFormatException();

            var sequenceDiagram = GetDiagram(diagram);
            if (sequenceDiagram != null)
            {
                sequenceDiagram.AddMessage(name, prob, source, target, messageType);
            }
        }

        public void AddOptional(string name, string diagram)
        {
            if (string.IsNullOrEmpty(name))
                throw new ActivityDiagramRuleException();

            if (string.IsNullOrEmpty(diagram))
                throw new EmptyOptionalFragment();

            if (Optionals == null)
                Optionals = new OptionalList();

            var sequenceDiagram = GetDiagram(diagram);
            if (sequenceDiagram == null)
                throw new ActivityDiagramRuleException();

            Optionals.Add(new Optional()
            {
                Name = name,
                RepresentedBy = sequenceDiagram
            });
        }

        public void Check()
        {
            if (ActivityDiagram == null)
                throw new Exception();

            if (Diagrams == null)
                throw new Exception();

            List<ActivityDiagramElements> listElements = new List<ActivityDiagramElements>();

            foreach (var element in ActivityDiagram.DiagramElements)
            {
                if (element.Value.ElementType == ActivityDiagramElements.ElementTypes.Activity)
                {
                    listElements.Add(element.Value);
                }
            }

            if(listElements.Count != Diagrams.Count)
                throw new ActivityRepresentationException();
        }

        public override string ToString()
        {
            Check();

            string xml = string.Empty;

            xml = $"<SequenceDiagrams>\n";

            xml += Lifelines.ToString();

            xml += Optionals.ToString();

            xml += Diagrams.ToString();

            xml += $"</SequenceDiagrams>\n";

            return xml;
        }
    }
}
