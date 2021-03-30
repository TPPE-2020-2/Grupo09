using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1;
using TP1.Classes;

namespace Tests
{
    public static class TestData
    {
        public static readonly Node InitialNode = new Node()
        {
            Name = "StartNode",
            Source = null,
            Target = "1",
            Prob = 0.0f,
            NodeType = NodeTypes.InitialNode
        };
    }
}
