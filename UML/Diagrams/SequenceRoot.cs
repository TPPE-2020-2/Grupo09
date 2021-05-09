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

        public List<Lifeline> Lifelines { get; set; }
        public List<Fragment> Fragments { get; set; }
        public List<Optional> Optionals { get; set; }

        public Dictionary<string, SequenceDiagram> Diagrams { get; set; }

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
                Lifelines = new List<Lifeline>();

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
                Diagrams = new Dictionary<string, SequenceDiagram>();

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
     
        public void AddOptional(string name, string diagram)
        {
            if (string.IsNullOrEmpty(name))
                throw new ActivityDiagramRuleException();

            if (string.IsNullOrEmpty(diagram))
                throw new EmptyOptionalFragment();

            if (Optionals == null)
                Optionals = new List<Optional>();

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

            if (Lifelines != null && Lifelines.Count > 0)
            {
                xml += $"\t<Lifelines>\n";

                foreach (var lifelines in Lifelines)
                {
                    xml += $"\t\t<{lifelines.ElementType} name=\"{lifelines.Name}\"/>\n";
                }

                xml += $"\t</Lifelines>\n";
            }

            if (Optionals != null && Optionals.Count > 0)
            {
                xml += $"\t<Fragments>\n";

                foreach (var optional in Optionals)
                {
                    xml += $"\t\t<{optional.ElementType} name=\"{optional.Name}\" representedBy=\"{optional.RepresentedBy.Name}\"/>\n";
                }

                xml += $"\t</Fragments>\n";
            }

            if (Diagrams != null && Diagrams.Count > 0)
            {
                foreach (var diagram in Diagrams)
                {
                    xml += $"\t<SequenceDiagram name=\"{diagram.Key}\">\n";

                    foreach (var message in diagram.Value.Messages)
                    {
                        xml += $"\t\t<{message.ElementType} name=\"{message.Name}\" prob=\"{message.Prob}\" source=\"{message.Source.Name}\" target=\"{message.Target.Name}\"/>\n";
                    }

                    xml += $"\t</SequenceDiagram>\n";
                }
            }

            xml += $"</SequenceDiagrams>\n";

            return xml;
        }
    }
}
