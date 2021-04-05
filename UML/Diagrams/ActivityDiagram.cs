using System;
using System.Collections.Generic;
using System.Linq;
using TPPE1.Exceptions;

namespace TPPE1.Elements
{
    public class ActivityDiagram
    {
        public bool AcceptDuplicate { get; set; }

        public string Name { get; set; }

        public List<KeyValuePair<string, ActivityDiagramElements>> DiagramElements { get; set; }
        public List<KeyValuePair<string, Transition>> Transitions { get; set; }

        private ActivityDiagram()
        {

        }

        public ActivityDiagram(string name, bool duplicate)
        {
            Name = name;
            AcceptDuplicate = duplicate;
        }

        public ActivityDiagramElements GetDiagramElement(string name, ActivityDiagramElements.ElementTypes type = ActivityDiagramElements.ElementTypes.None)
        {
            if (DiagramElements == null)
                return null;

            foreach (var element in DiagramElements)
            {
                if (element.Key == name)
                {
                    if (type == ActivityDiagramElements.ElementTypes.None)
                    {
                        return element.Value;
                    }
                    else
                    {
                        if (type == element.Value.ElementType)
                        {
                            return element.Value;
                        }
                    }
                }
            }

            return null;
        }

        public ActivityDiagramElements GetDiagramElement(ActivityDiagramElements.ElementTypes type)
        {
            if (DiagramElements == null)
                return null;

            foreach (var element in DiagramElements)
            {
                if (type == element.Value.ElementType)
                {
                    return element.Value;
                }
            }

            return null;
        }

        public void AddStartNode(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception();

            if (DiagramElements != null)
                throw new Exception();

            DiagramElements = new List<KeyValuePair<string, ActivityDiagramElements>>
            {
                new KeyValuePair<string, ActivityDiagramElements>(name,
                new StartNode
                {
                    Name = name
                })
            };
        }

        public void AddActivityNode(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception();

            if (DiagramElements == null)
                throw new ActivityDiagramRuleException();

            if (!AcceptDuplicate)
            {
                if (GetDiagramElement(name, ActivityDiagramElements.ElementTypes.Activity) != null)
                {
                    throw new Exception();
                }
            }

            DiagramElements.Add(new KeyValuePair<string, ActivityDiagramElements>(name,
                        new ActivityNode
                        {
                            Name = name
                        })
                    );
        }

        public void AddDecisionNode(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception();

            if (DiagramElements == null)
                throw new ActivityDiagramRuleException();

            if (!AcceptDuplicate)
            {
                if (GetDiagramElement(name, ActivityDiagramElements.ElementTypes.DecisionNode) != null)
                {
                    throw new Exception();
                }
            }

            DiagramElements.Add(new KeyValuePair<string, ActivityDiagramElements>(name,
                    new DecisionNode
                    {
                        Name = name
                    })
                );
        }

        public void AddMergeNode(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception();

            if (DiagramElements == null)
                throw new ActivityDiagramRuleException();

            if (!AcceptDuplicate)
            {
                if (GetDiagramElement(name, ActivityDiagramElements.ElementTypes.MergeNode) != null)
                {
                    throw new Exception();
                }
            }

            DiagramElements.Add(new KeyValuePair<string, ActivityDiagramElements>(name,
                    new MergeNode
                    {
                        Name = name
                    })
                );
        }

        public void AddFinalNode(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception();

            if (DiagramElements == null)
                throw new ActivityDiagramRuleException();

            if (!AcceptDuplicate)
            {
                if (GetDiagramElement(name, ActivityDiagramElements.ElementTypes.FinalNode) != null)
                {
                    throw new Exception();
                }
            }

            DiagramElements.Add(new KeyValuePair<string, ActivityDiagramElements>(name,
                    new FinalNode
                    {
                        Name = name
                    })
                );
        }

        public Transition GetTransition(string name)
        {
            if (Transitions == null)
                return null;

            foreach (var element in Transitions)
            {
                if (element.Key == name)
                {
                    return element.Value;
                }
            }

            return null;
        }

        public void AddTransition(string name, float prob)
        {
            if(string.IsNullOrEmpty(name))
                throw new Exception();

            if (Transitions == null)
                Transitions = new List<KeyValuePair<string, Transition>>();

            if (!AcceptDuplicate)
            {
                if (GetTransition(name) != null)
                    throw new Exception();
            }

            Transitions.Add(new KeyValuePair<string, Transition>(name,
                    new Transition
                    {
                        Name = name,
                        Prob = prob
                    })
                );
        }

        public bool Check()
        {
            var startNode = GetDiagramElement(ActivityDiagramElements.ElementTypes.StartNode);
            var finalNode = GetDiagramElement(ActivityDiagramElements.ElementTypes.FinalNode);

            // Um Diagrama de Atividades tem apenas um nodo inicial e pelo menos um nodo final.
            if (startNode == null || finalNode == null)
            {
                throw new ActivityDiagramRuleException();
            }

            return true;
        }

        public override string ToString()
        {
            Check();

            string xml = string.Empty;

            xml = $"<ActivityDiagram name=\"{Name}\">\n";

            if (DiagramElements != null && DiagramElements.Count > 0)
            {
                xml += $"\t<ActivityDiagramElements>\n";

                foreach (var element in DiagramElements)
                {
                    xml += $"\t\t<{element.Value.ElementType} name=\"{element.Value.Name}\"/>\n";
                }

                xml += $"\t</ActivityDiagramElements>\n";
            }

            if (Transitions != null && Transitions.Count > 0)
            {
                xml += $"\t<ActivityDiagramTransitions>\n";

                foreach (var transition in Transitions)
                {
                    xml += $"\t\t<Transition name=\"{transition.Value.Name}\" prob=\"{transition.Value.Prob}\"/>\n";
                }

                xml += $"\t</ActivityDiagramTransitions>\n";
            }

            xml += $"</ActivityDiagram>\n";

            return xml;
        }
    }
}
