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

        private ActivityDiagram() { }

        public ActivityDiagram(string name, bool duplicate)
        {
            Name = name;
            AcceptDuplicate = duplicate;
        }

        private bool CheckDiagramElementsIsValid()
        {
            return (DiagramElements != null);
        }

        private bool CheckTransitionIsValid()
        {
            return (Transitions != null) ? true : false;
        }

        private void ThrowDiagramElementsInvalid()
        {
            if (!CheckDiagramElementsIsValid())
            {
                throw new ActivityDiagramRuleException();
            }
        }

        private void ThrowInvalidInputGetElement(string name, ActivityDiagramElements.ElementTypes type)
        {
            if (string.IsNullOrEmpty(name) && type == ActivityDiagramElements.ElementTypes.None)
                throw new ArgumentOutOfRangeException();
        }

        private void ThrowDiagramElementsIsCreated()
        {
            if (CheckDiagramElementsIsValid())
            {
                throw new ActivityDiagramRuleException();
            }
        }

        private void ThrowDiagramNameIsInvalid(string node)
        {
            if (string.IsNullOrEmpty(node))
                throw new Exception();
        }

        private void ThrowTransitionIsInvalid()
        {
            if (!CheckTransitionIsValid())
                throw new Exception();
        }

        private bool CompareElementType(ActivityDiagramElements.ElementTypes a, ActivityDiagramElements.ElementTypes b)
        {
            return (a == b);
        }

        private bool CompareName(string a, string b)
        {
            return (a == b);
        }

        public ActivityDiagramElements GetDiagramElement(string name = "", ActivityDiagramElements.ElementTypes type = ActivityDiagramElements.ElementTypes.None)
        {
            ThrowDiagramElementsInvalid();
            ThrowInvalidInputGetElement(name, type);

            bool compareName = !string.IsNullOrEmpty(name);

            foreach (var element in DiagramElements)
            {
                if (compareName)
                {
                    if (CompareName(element.Key, name) && (CompareElementType(type, ActivityDiagramElements.ElementTypes.None) ||
                        CompareElementType(type, element.Value.ElementType)))
                    {
                        return element.Value;
                    }
                }
                else
                {
                    if (CompareElementType(type, element.Value.ElementType))
                    {
                        return element.Value;
                    }
                }
            }

            return null;
        }

        public ActivityDiagramElements GetDiagramElement(ActivityDiagramElements.ElementTypes type)
        {
            return GetDiagramElement("", type);
        }

        private void AddNode(ActivityDiagramElements.ElementTypes type, string name)
        {
            if (!AcceptDuplicate)
            {
                if (GetDiagramElement(name, type) != null)
                {
                    throw new Exception();
                }
            }
            ActivityDiagramElements node = default;
            node = type switch
            {
                ActivityDiagramElements.ElementTypes.StartNode => new StartNode { Name = name },
                ActivityDiagramElements.ElementTypes.Activity => new ActivityNode { Name = name },
                ActivityDiagramElements.ElementTypes.DecisionNode => new DecisionNode { Name = name },
                ActivityDiagramElements.ElementTypes.MergeNode => new MergeNode { Name = name },
                ActivityDiagramElements.ElementTypes.FinalNode => new FinalNode { Name = name },
                _ => throw new ArgumentOutOfRangeException(),
            };

            DiagramElements.Add(new KeyValuePair<string, ActivityDiagramElements>(name, node));
        }

        public void AddStartNode(string name)
        {
            ValidateNameAndElements(name, true);

            DiagramElements = new List<KeyValuePair<string, ActivityDiagramElements>>();

            AddNode(ActivityDiagramElements.ElementTypes.StartNode, name);
        }

        public void AddActivityNode(string name)
        {
            ValidateNameAndElements(name);

            AddNode(ActivityDiagramElements.ElementTypes.Activity, name);
        }

        public void AddDecisionNode(string name)
        {
            ValidateNameAndElements(name);

            AddNode(ActivityDiagramElements.ElementTypes.DecisionNode, name);
        }

        public void AddMergeNode(string name)
        {
            ValidateNameAndElements(name);

            AddNode(ActivityDiagramElements.ElementTypes.MergeNode, name);
        }

        private void ValidateNameAndElements(string name, bool checkDiagramCreated = false)
        {
            ThrowDiagramNameIsInvalid(name);

            if (checkDiagramCreated)
            {
                ThrowDiagramElementsIsCreated();
            }
            else
            {
                ThrowDiagramElementsInvalid();
            }
        }

        public void AddFinalNode(string name)
        {
            ValidateNameAndElements(name);

            AddNode(ActivityDiagramElements.ElementTypes.FinalNode, name);
        }

        public Transition GetTransition(string name)
        {
            ThrowDiagramNameIsInvalid(name);
            ThrowTransitionIsInvalid();

            foreach (var element in Transitions)
            {
                if (CompareName(element.Key, name))
                {
                    return element.Value;
                }
            }

            return null;
        }

        private void TryAddTranstion(string name, float prob)
        {
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

        public void AddTransition(string name, float prob)
        {
            ThrowDiagramNameIsInvalid(name);

            if (!CheckTransitionIsValid())
            {
                Transitions = new List<KeyValuePair<string, Transition>>();
            }

            TryAddTranstion(name, prob);
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