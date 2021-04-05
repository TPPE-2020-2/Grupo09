using System;
using TPPE1.Diagrams;
using TPPE1.Elements;
using TPPE1.Exceptions;
using Xunit;

namespace TestProject
{
    public class SequenceDiagramTest
    {
        [Fact]
        public void CreateSequenceDiagramTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("nome da atividade", true);

            Assert.True(sequenceDiagram != null);
        }

        [Fact]
        public void AddSequenceDiagramTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("nome da atividade", true);

            Assert.Single(sequenceDiagram.Diagrams);
        }

        [Fact]
        public void SequenceDiagramTest_Add_EmptyGuardConditionExceptionTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            Assert.Throws<EmptyGuardConditionException>(() => sequenceDiagram.AddDiagram("nome da atividade", false));
        }

        [Fact]
        public void AddSequenceDiagramTest2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddActivityNode("Memory Persistence");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("System identifies situation", true);
            sequenceDiagram.AddDiagram("SQLite Persistence", true);
            sequenceDiagram.AddDiagram("Memory Persistence", true);

            sequenceDiagram.Diagrams.TryGetValue("System identifies situation", out var sequence);

            Assert.Equal(sequenceDiagram, sequence.SequenceRoot);
        }

        [Fact]
        public void GetSequenceDiagramTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddActivityNode("Memory Persistence");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("System identifies situation", true);
            sequenceDiagram.AddDiagram("SQLite Persistence", true);
            sequenceDiagram.AddDiagram("Memory Persistence", true);
            
            Assert.Equal("System identifies situation", sequenceDiagram.GetDiagram("System identifies situation").Name);
        }

        [Fact]
        public void GetSequenceDiagramTest2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddActivityNode("Memory Persistence");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("System identifies situation", true);
            sequenceDiagram.AddDiagram("SQLite Persistence", true);
            sequenceDiagram.AddDiagram("Memory Persistence", true);

            sequenceDiagram.Diagrams.TryGetValue("System identifies situation", out var sequence);

            Assert.Equal("System identifies situation", sequenceDiagram.GetDiagram("System identifies situation").Name);
            Assert.Equal("System identifies situation", sequence.Name);
        }

        [Fact]
        public void SequenceDiagramTest_AddFragment_EmptyOptionalFragmentTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            Assert.Throws<EmptyOptionalFragment>(() => sequenceDiagram.AddOptional("Test", null));
        }

        [Fact]
        public void SequenceDiagra_AddFragment()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);

            sequenceDiagram.AddOptional("Optinal1", "System identifies situation");

            Assert.Equal("Optinal1", sequenceDiagram.Optionals[0].Name);
        }

        [Fact]
        public void SequenceDiagra_AddFragment2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);

            sequenceDiagram.AddOptional("Optinal1", "System identifies situation");

            Assert.Single(sequenceDiagram.Optionals);
        }

        [Fact]
        public void SequenceDiagramTest_AddMessage_MessageFormatExceptionTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);

            Assert.Throws<MessageFormatException>(() => sequenceDiagram.AddMessage("System identifies situation", "", 0, "", "", TPPE1.Sequence.Message.MessageTypes.Asynchronous));
        }

        [Fact]
        public void SequenceDiagramTest_AddMessage_MessageFormatExceptionTest2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);

            Assert.Throws<MessageFormatException>(() => sequenceDiagram.AddMessage("System identifies situation", "asdad", 0, "", "", TPPE1.Sequence.Message.MessageTypes.Asynchronous));
        }

        [Fact]
        public void SequenceDiagramTest_AddMessage_MessageFormatExceptionTest3()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);

            Assert.Throws<MessageFormatException>(() => sequenceDiagram.AddMessage("System identifies situation", "", 0, "asdad", "", TPPE1.Sequence.Message.MessageTypes.Asynchronous));
        }

        [Fact]
        public void SequenceDiagramTest_AddMessage_MessageFormatExceptionTest4()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);
            sequenceDiagram.AddDiagram("System identifies situation", true);
            sequenceDiagram.AddOptional("[SQLite]", "System identifies situation");
            sequenceDiagram.AddOptional("[Memory]", "System identifies situation");

            Assert.Throws<MessageFormatException>(() => sequenceDiagram.AddMessage("System identifies situation", "asdad", 0.1f, "[SQLite]", "[Memory]", TPPE1.Sequence.Message.MessageTypes.Asynchronous));
        }

        [Fact]
        public void SequenceDiagram_AddLifelines()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            Assert.Single(sequenceDiagram.Lifelines);
        }

        [Fact]
        public void SequenceDiagram_AddLifelines2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Bus1");
            Assert.Equal(2, sequenceDiagram.Lifelines.Count);
        }

        [Fact]
        public void SequenceDiagram_AddLifelines3()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Bus1");
            Assert.Equal(2, sequenceDiagram.Lifelines.Count);
        }

        [Fact]
        public void SequenceDiagram_AddLifelines4()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            Assert.Equal("Bus", sequenceDiagram.Lifelines[0].Name);
        }

        [Fact]
        public void SequenceDiagram_AddLifelines5()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Test");
            Assert.Equal("Bus", sequenceDiagram.Lifelines[0].Name);
        }

        [Fact]
        public void SequenceDiagram_GetLifeline()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Test");
            
            Assert.Equal(sequenceDiagram.GetLifeline("Bus").Name, sequenceDiagram.Lifelines[0].Name);
        }

        [Fact]
        public void SequenceDiagram_GetLifeline2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Test");

            Assert.Equal(sequenceDiagram.GetLifeline("Bus").Name, sequenceDiagram.Lifelines[0].Name);
            Assert.Equal(sequenceDiagram.GetLifeline("Test").Name, sequenceDiagram.Lifelines[1].Name);
        }

        [Fact]
        public void SequenceDiagram_Xml()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddActivityNode("Memory Persistence");
            activityDiagram.AddFinalNode("nome do nodo final");

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

            sequenceDiagram.AddMessage("System identifies situation", "register", 0.999f, "Oxygenation", "Bus", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replyRegister", 0.999f, "Bus", "Oxygenation", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "sendSituation", 0.999f, "Oxygenation", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "persist", 0.999f, "Persistence", "SQLite", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replyPersist", 0.999f, "Persistence", "Oxygenation", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("System identifies situation", "replySendSituation(Oxygenation)", 0.999f, "Oxygenation", "Bus", TPPE1.Sequence.Message.MessageTypes.Reply);

            sequenceDiagram.AddMessage("SQLite Persistence", "persist", 0.999f, "Persistence", "SQLite", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("SQLite Persistence", "replyPersist", 0.999f, "SQLite", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);

            sequenceDiagram.AddMessage("Memory Persistence", "persist", 0.999f, "Persistence", "Memory", TPPE1.Sequence.Message.MessageTypes.Reply);
            sequenceDiagram.AddMessage("Memory Persistence", "replyPersist", 0.999f, "Memory", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);

            var xmlFinal = "<SequenceDiagrams>\n\t<Lifelines>\n\t\t<Lifeline name=\"Bus\"/>\n\t\t<Lifeline name=\"Oxygenation\"/>\n\t\t<Lifeline name=\"Persistence\"/>\n\t\t<Lifeline name=\"SQLite\"/>\n\t\t<Lifeline name=\"Memory\"/>\n\t</Lifelines>\n\t<Fragments>\n\t\t<Optional name=\"[SQLite]\" representedBy=\"System identifies situation\"/>\n\t\t<Optional name=\"[Memory]\" representedBy=\"System identifies situation\"/>\n\t</Fragments>\n\t<SequenceDiagram name=\"System identifies situation\">\n\t\t<Message name=\"register\" prob=\"0.999\" source=\"Oxygenation\" target=\"Bus\"/>\n\t\t<Message name=\"replyRegister\" prob=\"0.999\" source=\"Bus\" target=\"Oxygenation\"/>\n\t\t<Message name=\"sendSituation\" prob=\"0.999\" source=\"Oxygenation\" target=\"Persistence\"/>\n\t\t<Message name=\"persist\" prob=\"0.999\" source=\"Persistence\" target=\"SQLite\"/>\n\t\t<Message name=\"replyPersist\" prob=\"0.999\" source=\"Persistence\" target=\"Oxygenation\"/>\n\t\t<Message name=\"replySendSituation(Oxygenation)\" prob=\"0.999\" source=\"Oxygenation\" target=\"Bus\"/>\n\t</SequenceDiagram>\n\t<SequenceDiagram name=\"SQLite Persistence\">\n\t\t<Message name=\"persist\" prob=\"0.999\" source=\"Persistence\" target=\"SQLite\"/>\n\t\t<Message name=\"replyPersist\" prob=\"0.999\" source=\"SQLite\" target=\"Persistence\"/>\n\t</SequenceDiagram>\n\t<SequenceDiagram name=\"Memory Persistence\">\n\t\t<Message name=\"persist\" prob=\"0.999\" source=\"Persistence\" target=\"Memory\"/>\n\t\t<Message name=\"replyPersist\" prob=\"0.999\" source=\"Memory\" target=\"Persistence\"/>\n\t</SequenceDiagram>\n</SequenceDiagrams>\n";

            Assert.Equal(xmlFinal, sequenceDiagram.ToString());
        }
    }
}
