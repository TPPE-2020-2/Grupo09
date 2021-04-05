using System;
using System.IO;
using TPPE1.Diagrams;
using TPPE1.Elements;

namespace TPPE1
{
    class Program
    {
        private const string ACTIVITY_DIAGRAM_XML_FILE_NAME = "ActivityDiagram.xml";
        private const string SEQUENCE_DIAGRAM_XML_FILE_NAME = "SequenceDiagram.xml";

        static void Main(string[] args)
        {
            // Mock.
            Exemplo();
        }

        static void SaveXml(string fileName, string xmlData)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            File.WriteAllText(fileName, xmlData);
        }

        static void Exemplo()
        {
            // Activity Diagram
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddActivityNode("Memory Persistence");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");
            activityDiagram.AddFinalNode("nome do nodo final");

            activityDiagram.AddTransition("nome da transicao", 0.999f);
            activityDiagram.AddTransition("nome da transicao", 0.999f);

            SaveXml(ACTIVITY_DIAGRAM_XML_FILE_NAME, activityDiagram.ToString());

            // Sequence Root.
            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("System identifies situation", true);
            sequenceDiagram.AddDiagram("SQLite Persistence", true);
            sequenceDiagram.AddDiagram("Memory Persistence", true);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Oxygenation");
            sequenceDiagram.AddLifeline("Persistence");
            sequenceDiagram.AddLifeline("SQLite");
            sequenceDiagram.AddLifeline("Memory");

            sequenceDiagram.AddOptional("[SQLite]", "System identifies situation");
            sequenceDiagram.AddOptional("[Memory]", "System identifies situation");

            sequenceDiagram.AddMessage("System identifies situation", "register", 0.999f, "Oxygenation", "Bus", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replyRegister", 0.999f, "Bus", "Oxygenation", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "sendSituation", 0.999f, "Oxygenation", "Persistence", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "persist", 0.999f, "Persistence", "SQLite", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replyPersist", 0.999f, "Persistence", "Oxygenation", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replySendSituation(Oxygenation)", 0.999f, "Oxygenation", "Bus", Sequence.Message.MessageTypes.Reply);

            sequenceDiagram.AddMessage("SQLite Persistence", "persist", 0.999f, "Persistence", "SQLite", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("SQLite Persistence", "replyPersist", 0.999f, "SQLite", "Persistence", Sequence.Message.MessageTypes.Reply);

            sequenceDiagram.AddMessage("Memory Persistence", "persist", 0.999f, "Persistence", "Memory", Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("Memory Persistence", "replyPersist", 0.999f, "Memory", "Persistence", Sequence.Message.MessageTypes.Reply);

            SaveXml(SEQUENCE_DIAGRAM_XML_FILE_NAME, sequenceDiagram.ToString());
        }
    }
}
