using System;
using TPPE1.Diagrams;
using TPPE1.Elements;
using TPPE1.Exceptions;
using Xunit;

namespace TestProject
{
    public class ActivityDiagramTest
    {
        [Fact]
        public void CreateActivityDiagramTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);

            Assert.True(activityDiagram != null);
            Assert.Equal("nome do diagrama", activityDiagram.Name);
            Assert.True(activityDiagram.AcceptDuplicate);
        }

        [Fact]
        public void ActivityDiagram_Create_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.Check());
        }

        [Fact]
        public void ActivityDiagram_Add_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.Check());
        }

        [Fact]
        public void ActivityDiagram_Add2_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.AddActivityNode("nome da atividade"));
        }

        [Fact]
        public void ActivityDiagram_Add3_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.AddDecisionNode("nome do nodo de decisao"));
        }

        [Fact]
        public void ActivityDiagram_Add4_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.AddMergeNode("nome do nodo de fusao"));
        }

        [Fact]
        public void ActivityDiagram_Add5_ActivityDiagramRuleExceptionTest()
        {
            ActivityDiagram activityDiagram = new ActivityDiagram("nome do diagrama", true);
            Assert.Throws<ActivityDiagramRuleException>(() => activityDiagram.AddFinalNode("nome do nodo final"));
        }

        [Fact]
        public void ActivityDiagram_Add5_ActivityRepresentationExceptionTest()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddFinalNode("nome do nodo final");

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            Assert.Throws<ActivityRepresentationException>(() => sequenceDiagram.AddDiagram("System identifies situation", true));
        }
           
        [Fact]
        public void ActivityDiagram_Add5_ActivityRepresentationExceptionTest2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("System identifies situation");
            activityDiagram.AddActivityNode("SQLite Persistence");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");
            activityDiagram.AddFinalNode("nome do nodo final");

            activityDiagram.AddTransition("nome da transicao", 0.999f);
            activityDiagram.AddTransition("nome da transicao", 0.999f);

            SequenceRoot sequenceDiagram = new SequenceRoot(activityDiagram);

            sequenceDiagram.AddDiagram("System identifies situation", true);

            sequenceDiagram.AddLifeline("Bus");
            sequenceDiagram.AddLifeline("Oxygenation");
            sequenceDiagram.AddLifeline("Persistence");
            sequenceDiagram.AddLifeline("SQLite");
            sequenceDiagram.AddLifeline("Memory");

            sequenceDiagram.AddOptional("[SQLite]", "System identifies situation");
            sequenceDiagram.AddOptional("[Memory]", "System identifies situation");

            SequenceDiagram diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("System identifies situation");

            if (diagramThatWillReceiveMessage != null)
            {
                diagramThatWillReceiveMessage.AddMessage("register", 0.999f, "Oxygenation", "Bus", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("replyRegister", 0.999f, "Bus", "Oxygenation", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("sendSituation", 0.999f, "Oxygenation", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "SQLite", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "Persistence", "Oxygenation", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("replySendSituation(Oxygenation)", 0.999f, "Oxygenation", "Bus", TPPE1.Sequence.Message.MessageTypes.Reply);

            }


            diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("SQLite Persistence");

            if (diagramThatWillReceiveMessage != null)
            {

                diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "SQLite", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "SQLite", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);
            }


            diagramThatWillReceiveMessage = sequenceDiagram.GetDiagram("Memory Persistence");

            if (diagramThatWillReceiveMessage != null)
            {

                diagramThatWillReceiveMessage.AddMessage("persist", 0.999f, "Persistence", "Memory", TPPE1.Sequence.Message.MessageTypes.Reply);
                diagramThatWillReceiveMessage.AddMessage("replyPersist", 0.999f, "Memory", "Persistence", TPPE1.Sequence.Message.MessageTypes.Reply);
            }

     
            Assert.Throws<ActivityRepresentationException>(() => sequenceDiagram.Check());
        }

        [Fact]
        public void ActivityDiagram_Xml()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");
            activityDiagram.AddFinalNode("nome do nodo final");

            activityDiagram.AddTransition("nome da transicao", 0.999f);
            activityDiagram.AddTransition("nome da transicao", 0.999f);

            var xmlFinal = "<ActivityDiagram name=\"nome do diagrama\">\n\t<ActivityDiagramElements>\n\t\t<StartNode name=\"nome do nodo inicial\"/>\n\t\t<Activity name=\"nome da atividade\"/>\n\t\t<DecisionNode name=\"nome do nodo de decisao\"/>\n\t\t<MergeNode name=\"nome do nodo de fusao\"/>\n\t\t<FinalNode name=\"nome do nodo final\"/>\n\t</ActivityDiagramElements>\n\t<ActivityDiagramTransitions>\n\t\t<Transition name=\"nome da transicao\" prob=\"0.999\"/>\n\t\t<Transition name=\"nome da transicao\" prob=\"0.999\"/>\n\t</ActivityDiagramTransitions>\n</ActivityDiagram>\n";

            Assert.Equal(xmlFinal, activityDiagram.ToString());
        }

        [Fact]
        public void ActivityDiagram_AddTransition()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddTransition("nome da transicao", 0.999f);

            Assert.Equal("nome da transicao", activityDiagram.Transitions[0].Value.Name);
        }

        [Fact]
        public void ActivityDiagram_AddTransition2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddTransition("nome da transicao", 0.999f);
            activityDiagram.AddTransition("nome da transicao2", 0.999f);

            Assert.Equal(2, activityDiagram.Transitions.Count);
        }

        [Fact]
        public void ActivityDiagram_getTransition()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddTransition("nome da transicao", 0.999f);

            Assert.Equal(0.999f, activityDiagram.Transitions[0].Value.Prob);
        }

        [Fact]
        public void ActivityDiagram_getTransition2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddTransition("nome da transicao", 0.999f);

            Assert.Equal(0.999f, activityDiagram.GetTransition("nome da transicao").Prob);
        }

        [Fact]
        public void ActivityDiagram_AddActivityNode()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement("nome do nodo inicial") != null);
        }

        [Fact]
        public void ActivityDiagram_AddActivityNode2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddActivityNode("nome da atividade");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement(ActivityDiagramElements.ElementTypes.Activity) != null);
        }

        [Fact]
        public void ActivityDiagram_AddDecisionNode()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement("nome do nodo de decisao") != null);
        }

        [Fact]
        public void ActivityDiagram_AddDecisionNode2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddDecisionNode("nome do nodo de decisao");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement(ActivityDiagramElements.ElementTypes.DecisionNode) != null);
        }

        [Fact]
        public void ActivityDiagram_AddMergeNode()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement("nome do nodo de fusao") != null);
        }

        [Fact]
        public void ActivityDiagram_AddMergeNode2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddMergeNode("nome do nodo de fusao");

            Assert.True(activityDiagram.GetDiagramElement(ActivityDiagramElements.ElementTypes.MergeNode) != null);
        }

        [Fact]
        public void ActivityDiagram_AddFinalNode()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddFinalNode("nome do nodo final");

            Assert.True(activityDiagram.GetDiagramElement("nome do nodo final") != null);
        }

        [Fact]
        public void ActivityDiagram_AddFinalNode2()
        {
            ActivityDiagram activityDiagram = new("nome do diagrama", true);

            activityDiagram.AddStartNode("nome do nodo inicial");
            activityDiagram.AddFinalNode("nome do nodo final");

            Assert.True(activityDiagram.GetDiagramElement(ActivityDiagramElements.ElementTypes.FinalNode) != null);
        }
    }
}
