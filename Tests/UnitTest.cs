using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP1.Classes;
using TP1.Services;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        private NodeService nodeService;

        [TestInitialize]
        public void Setup()
        {
            nodeService = new NodeService();
        }
        [TestMethod]
        public void CreateInitialNode()
        {
            var initialNode = nodeService.CreateInitialNode("StartNode");

            Assert.AreEqual(TestData.InitialNode.Name, initialNode.Name);
            Assert.AreEqual(TestData.InitialNode.Source, initialNode.Source);
            Assert.AreEqual(TestData.InitialNode.Target, initialNode.Target);
            Assert.AreEqual(TestData.InitialNode.Prob, initialNode.Prob);
        }

        [TestMethod]
        public void CreateFinalNode()
        {
            //Assert.
        }
    }
}
