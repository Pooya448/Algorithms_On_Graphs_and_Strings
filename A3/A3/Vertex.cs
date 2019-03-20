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
        public int ReverseDist { get; set; }
        //public List<Vertex> ConnectedToVertices { get; set; }
        //public List<Vertex> ConnectedVertices { get; set; }
        public List<Edge> Edges { get; set; }
        public List<Edge> EdgesTranspose { get; set; }
        public int Id { get; set; }
        public bool Check { get; set; }
        public bool ReverseCheck { get; set; }
        public bool ForwardCheck { get; set; }

        public bool NegativeCycle { get; set; }
        public Vertex Prev { get; set; }
        public Vertex(int id)
        {
            Id = id;
            Check = false;
            ReverseCheck = false;
            ForwardCheck = false;
            //ConnectedVertices = new List<Vertex>();
            //ConnectedToVertices = new List<Vertex>();
            Edges = new List<Edge>();
            EdgesTranspose = new List<Edge>();
            Dist = Infinity;
            ReverseDist = Infinity;
            ForwardDist = Infinity;
            NegativeCycle = false;
            return;
        }
    }
}
