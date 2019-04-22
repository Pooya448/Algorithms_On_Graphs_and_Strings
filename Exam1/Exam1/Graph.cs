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
            Depth = int.MaxValue;
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
            return;
        }
        public long[] FindCentrality()
        {
            List<long> res = new List<long>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                for (int j = 0; j < Vertices.Count; j++)
                {
                    FindShortestPath(i, j);
                }
            }
            foreach (var v in Vertices)
            {
                res.Add(v.Centrality);
            }
            return res.OrderByDescending(x => x).ToArray();
        }
        public void FindShortestPath(int start, int end)
        {
            Queue<Vertex> bfsQueue = new Queue<Vertex>();
            Vertex startVertex = Vertices[start];
            Vertex target = Vertices[end];
            if (startVertex == target)
                return;
            startVertex.Check = true;
            startVertex.Depth = 0;
            bfsQueue.Enqueue(startVertex);
            while (bfsQueue.Any())
            {
                var temp = bfsQueue.Dequeue();
                if (temp == target)
                {
                    break;
                }
                temp.Check = true;
                foreach (var item in temp.ConnectedVertices)
                    if (!item.Check)
                    {
                        if (temp.Depth + 1 < item.Depth)
                        {
                            item.Depth = temp.Depth + 1;
                            item.Prev = temp;
                        }
                        bfsQueue.Enqueue(item);
                    }
            }
            if (target.Depth == int.MaxValue || target.Depth < 2)
            {
                Reset();
                return;
            }
            Reconstruct(target, startVertex);
            Reset();
            return;
        }
        public void Reconstruct(Vertex s,Vertex t)
        {
            Vertex pivot = s;
            while (pivot != t)
            {
                if (pivot.Prev != null)
                {
                    if (pivot.Prev.Prev != null)
                    {
                        pivot.Prev.Centrality++;
                        var temp = pivot.Prev;
                        pivot = temp;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                
            }
        }
        
        public void Reset()
        {
            foreach (var item in Vertices)
            {
                item.Depth = 0;
                item.Check = false;
                item.Prev = null;
            }
        }
    }
}
