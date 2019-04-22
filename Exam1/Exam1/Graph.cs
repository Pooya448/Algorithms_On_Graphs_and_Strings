using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    class Vertex
    {
        public List<Vertex> ConnectedVertices { get; set; }
        public List<Vertex> ConnectedToVertices { get; set; }
        public Vertex Prev { get; set; }
        public int Id { get; set; }
        public bool Check { get; set; }
        public int SccId { get; set; }
        public int PostVisit { get; set; }
        public int PreVisit { get; set; }
        public int Depth { get; set; }
        public int Centrality { get; set; }
        public bool? Color { get; set; }
        public Vertex(int id)
        {
            Centrality = 0;
            Id = id;
            Depth = 2000000;
            Color = null;
            ConnectedVertices = new List<Vertex>();
            ConnectedToVertices = new List<Vertex>();
            return;
        }
    }
    class Graph
    {
        public int CcNumber { get; private set; }
        public List<Vertex> Vertices { get; set; }
        public List<long> TopoligicalSort { get; set; }
        public int VertexCount { get; set; }
        public Graph(long nodeCount, long[][] edges, bool isDirected = false, bool transpose = false)
        {
            Vertices = new List<Vertex>();
            CcNumber = 0;
            VertexCount = (int)nodeCount;
            for (int i = 1; i <= VertexCount; i++)
                Vertices.Add(new Vertex(i));
            if (isDirected)
                for (int i = 0; i < edges.GetLength(0); i++)
                {
                    Vertices[(int)edges[i][0] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][1] - 1]);
                    Vertices[(int)edges[i][1] - 1].ConnectedToVertices.Add(Vertices[(int)edges[i][0] - 1]);
                }
            else
                for (int i = 0; i < edges.GetLength(0); i++)
                {
                    Vertices[(int)edges[i][0] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][1] - 1]);
                    Vertices[(int)edges[i][1] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][0] - 1]);
                }
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i].ConnectedVertices = Vertices[i].ConnectedVertices.OrderByDescending(x => x.Id).ToList();
            }
            return;
        }
        public long[] FindCentrality()
        {
            List<long> res = new List<long>();
            for (int i = 0; i < Vertices.Count; i++)
                for (int j = 0; j < Vertices.Count; j++)
                    FindShortestPath(i, j);
            foreach (var v in Vertices)
                res.Add(v.Centrality);
            return res.ToArray();
        }
        public void FindShortestPath(int start, int end)
        {
            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(Vertices[start]);
            while (q.Any())
            {
                var temp = q.Dequeue();
                if (temp == Vertices[end])
                    break;
                temp.Check = true;
                foreach (var v in temp.ConnectedVertices)
                    if (!v.Check && v.Prev == null)
                    {
                        q.Enqueue(v);
                        v.Prev = temp;
                    }
            }
            Reconstruct(Vertices[end],Vertices[start]);
            Reset();
            return;
        }
        public void Reconstruct(Vertex s, Vertex t)
        {
            var pivot = s.Prev;
            if (s.Prev == null)
                return;
            while (pivot.Id != t.Id)
            {
                pivot.Centrality++;
                pivot = pivot.Prev;
            }
        }
        
        public void Reset()
        {
            foreach (var item in Vertices)
            {
                item.Check = false;
                item.Prev = null;
            }
        }
    }
}
