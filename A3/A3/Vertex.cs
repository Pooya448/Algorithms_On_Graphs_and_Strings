using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Vertex
    {
        public const int Infinity = 2_000_000_000;
        public int Dist { get; set; }
        public int ForwardDist { get; set; }
        public int BackwardDist { get; set; }
        public List<Edge> Edges { get; set; }
        public List<Edge> EdgesTranspose { get; set; }
        public int Id { get; set; }
        public bool Check { get; set; }
        public bool BackwardCheck { get; set; }
        public bool ForwardCheck { get; set; }
        //public bool IsInBwdQueue { get; set; }
        //public bool IsInFwdQueue { get; set; }
        public bool NegativeCycle { get; set; }
        public int FwdIndex { get; set; }
        public int BwdIndex { get; set; }
        public Vertex Prev { get; set; }
        public Vertex(int id)
        {
            //IsInBwdQueue = false;
            //IsInFwdQueue = false;
            Id = id;
            Check = false;
            BackwardCheck = false;
            ForwardCheck = false;
            Edges = new List<Edge>();
            EdgesTranspose = new List<Edge>();
            Dist = Infinity;
            BackwardDist = Infinity;
            ForwardDist = Infinity;
            FwdIndex = -1;
            BwdIndex = -1;
            NegativeCycle = false;
            return;
        }
    }
}
