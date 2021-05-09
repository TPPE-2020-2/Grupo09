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
            
            SequenceDiagram diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("System identifies situation");

            diagramThatWillReceiveMessage.AddMessage("register", 0.999f, "Oxygenation", "Bus", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("replyRegister", 0.999f, "Bus", "Oxygenation", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("sendSituation", 0.999f, "Oxygenation", "Persistence", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "SQLite", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "Persistence", "Oxygenation", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("replySendSituation(Oxygenation)", 0.999f, "Oxygenation", "Bus", Sequence.Message.MessageTypes.Reply);

            diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("SQLite Persistence");

            diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "SQLite", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "SQLite", "Persistence", Sequence.Message.MessageTypes.Reply);

            diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("Memory Persistence");

            diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "Memory", Sequence.Message.MessageTypes.Reply);
            diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "Memory", "Persistence", Sequence.Message.MessageTypes.Reply);
           
            SaveXml(SEQUENCE_DIAGRAM_XML_FILE_NAME, sequenceDiagram.ToString());
        }
    }
}
