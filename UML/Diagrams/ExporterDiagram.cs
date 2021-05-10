using System.Collections.Generic;
using TPPE1.Elements;

namespace UML.Diagrams
{
    public class ExporterDiagram
    {
        private string _name = default;
        private readonly List<KeyValuePair<string, ActivityDiagramElements>> _elements = default;
        private readonly List<KeyValuePair<string, Transition>> _transitions = default;

        private string _data = default;

        public string Data => _data;

        private ExporterDiagram() { }

        public ExporterDiagram(string name, List<KeyValuePair<string, ActivityDiagramElements>> elements, List<KeyValuePair<string, Transition>> transitions)
        {
            _name = name;

            _elements = elements;
            _transitions = transitions;

            _data = string.Empty;
        }

        public string Export()
        {
            InitializeDiagram();

            Elements();
            Transitions();

            EndDiagram();

            return _data;
        }

        private void InitializeDiagram()
        {
            WriteDataLine($"<ActivityDiagram name=\"{_name}\">", 0, false);
        }

        private void Elements()
        {
            if (_elements != null && _elements.Count > 0)
            {
                WriteDataLine("<ActivityDiagramElements>", 1);

                ProcessElements();

                WriteDataLine("</ActivityDiagramElements>", 1);
            }
        }

        private void ProcessElements()
        {
            foreach (var element in _elements)
            {
                WriteDataLine($"<{element.Value.ElementType} name=\"{element.Value.Name}\"/>", 2);
            }
        }

        private void Transitions()
        {
            if (_transitions != null && _transitions.Count > 0)
            {
                WriteDataLine("<ActivityDiagramTransitions>", 1);

                ProcessTransitions();

                WriteDataLine("</ActivityDiagramTransitions>", 1);
            }
        }

        private void ProcessTransitions()
        {
            foreach (var transition in _transitions)
            {
                WriteDataLine($"<Transition name=\"{transition.Value.Name}\" prob=\"{transition.Value.Prob}\"/>", 2);
            }
        }

        private void EndDiagram()
        {
            WriteDataLine("</ActivityDiagram>");
        }

        private void WriteDataLine(string data, int tabCount = 0, bool append = true)
        {
            WriteData($"{data}\n", tabCount, append);
        }

        private void WriteData(string data, int tabCount = 0, bool append = true)
        {
            var tmpStr = "";

            for(int i = 0; i < tabCount; i++)
            {
                tmpStr += "\t";
            }

            tmpStr += data;

            _data = (append) ? (_data + tmpStr) : tmpStr;
        }
    }
}
