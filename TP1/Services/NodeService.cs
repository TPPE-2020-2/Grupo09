using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Classes;

namespace TP1.Services
{
    public class NodeService
    {
        public Node CreateInitialNode(string name)
        {
            return new Node()
            {
                Name = name,
                Source = null,
                Target = "1",
                Prob = 0.0f,
                NodeType = NodeTypes.InitialNode
            };
        }
    }
}
