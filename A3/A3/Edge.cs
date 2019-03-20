using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Edge
    {
        public Edge(Vertex from, Vertex to, int w)
        {
            From = from;
            To = to;
            Weight = w;
            Check = false;
            ForwardCheck = false;
            ReverseCheck = false;
        }

        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Weight { get; set; }
        public bool Check { get; set; }
        public bool ForwardCheck { get; set; }
        public bool ReverseCheck { get; set; }
    }
}
