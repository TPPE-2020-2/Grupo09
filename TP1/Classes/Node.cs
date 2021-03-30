using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Classes
{
    public class Node
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public float Prob { get; set; }
        public NodeTypes NodeType { get; set; }

        public Node()
        {

        }
    }
}
