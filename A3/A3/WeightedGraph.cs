using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace A3
{
    class WeightedGraph
    {
        public const int Infinity = 2_000_000_000;
        public List<Vertex> Vertices { get; set; }
        public int VertexCount { get; set; }
        public List<Vertex> ProcessVertices { get; set; }
        public List<Edge> ProcessEdges { get; set; }

        public WeightedGraph(long nodeCount, long[][] edges, bool isBiDirectional = false)
        {
            ProcessEdges = new List<Edge>();
            ProcessVertices = new List<Vertex>();
            Vertices = new List<Vertex>();
            VertexCount = (int)nodeCount;
            for (int i = 1; i <= VertexCount; i++)
                Vertices.Add(new Vertex(i));
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                Vertices[(int)edges[i][0] - 1].Edges.Add(new Edge(Vertices[(int)edges[i][0] - 1], Vertices[(int)edges[i][1] - 1], (int)edges[i][2]));
                if (isBiDirectional)
                {
                    Vertices[(int)edges[i][1] - 1].EdgesTranspose.Add(new Edge(Vertices[(int)edges[i][1] - 1], Vertices[(int)edges[i][0] - 1], (int)edges[i][2]));
                }
            }
        }
        
        public void Dijkstra(Vertex start, Vertex target)
        {
            int processCount = VertexCount;
            start.Dist = 0;
            Vertex currentVertex = start;
            List<Edge> process = new List<Edge>();
            while (processCount > 0)
            {
                if (currentVertex.Id == target.Id)
                    return;
                Relax(currentVertex);
                currentVertex.Check = true;
                foreach (var e in currentVertex.Edges)
                    process.Add(e);
                var minEdge = process.Where(e => !e.Check && !e.To.Check).OrderBy(x => x.To.Dist).FirstOrDefault();
                if (minEdge != null)
                {
                    minEdge.Check = true;
                    currentVertex = minEdge.To;
                }
                processCount--;
            }
        }
        public int ShortestPath (int s, int t)
        {
            Dijkstra(Vertices[s - 1], Vertices[t - 1]);
            int pathLength = Vertices[t - 1].Dist == Infinity ? -1 : Vertices[t - 1].Dist;
            return pathLength;
        }
        public bool NegativeCycleDetection()
        {
            Vertices[0].Dist = 0;
            for (int i = 1; i < VertexCount; i++)
                foreach (var v in Vertices)
                    foreach (var e in v.Edges)
                        if (e.To.Dist > e.From.Dist + e.Weight)
                            e.To.Dist = e.From.Dist + e.Weight;
            foreach (var v in Vertices)
                foreach (var e in v.Edges)
                    if (e.To.Dist > e.From.Dist + e.Weight)
                        return true;
            return false;
        }
        public void BellmanFord (Vertex start)
        {
            start.Dist = 0;
            for (int i = 1; i < VertexCount; i++)
                foreach (var v in Vertices)
                    foreach (var e in v.Edges)
                        if (e.From.Dist != Infinity && e.To.Dist > e.From.Dist + e.Weight)
                        {
                            e.To.Dist = e.From.Dist + e.Weight;
                            e.To.Prev = e.From;
                        }
            return;
        }
        public string[] Arbitrage(int start)
        {
            string[] result = new string[VertexCount];
            List<Vertex> changed = new List<Vertex>();
            BellmanFord(Vertices[start - 1]);
            foreach (var v in Vertices)
                foreach (var e in v.Edges)
                    if (e.From.Dist != Infinity && e.To.Dist > e.From.Dist + e.Weight)
                    {
                        e.To.Dist = e.From.Dist + e.Weight;
                        e.To.Prev = e.From;
                        changed.Add(e.To);
                    }
            List<int> infinitPath = new List<int>();
            List<int> arbitrage = new List<int>();

            foreach (var v in changed)
                infinitPath = infinitPath.Union(GetRoute(v, Vertices[start - 1])).ToList();
            foreach (var v in infinitPath)
                arbitrage = arbitrage.Union(BFS(v)).ToList();
            foreach (var i in arbitrage)
                Vertices[i - 1].NegativeCycle = true;

            foreach (var v in Vertices)
                if (v.NegativeCycle)
                    result[v.Id - 1] = "-";
                else if (v.Dist == Infinity)
                    result[v.Id - 1] = "*";
                else
                    result[v.Id - 1] = v.Dist.ToString();
            return result;
        }
        private List<int> GetRoute(Vertex v, Vertex start)
        {
            int pos = 0;
            List<int> route = new List<int>();
            Vertex pivot = v;
            while (pivot.Id != start.Id)
            {
                if (route.Contains(pivot.Id))
                {
                    pos = route.IndexOf(pivot.Id);
                    break;
                }
                route.Add(pivot.Id);
                pivot = pivot.Prev;
            }
            return route.GetRange(pos, route.Count - pos);
        }
        public List<int> BFS (int s)
        {
            Queue<Vertex> bfsQueue = new Queue<Vertex>();
            List<int> res = new List<int>();
            bfsQueue.Enqueue(Vertices[s - 1]);
            while (bfsQueue.Any())
            {
                var pop = bfsQueue.Dequeue();
                res.Add(pop.Id);
                pop.Check = true;
                foreach (var e in pop.Edges)
                    if (!e.To.Check)
                        bfsQueue.Enqueue(e.To);
            }
            return res;
        }
        public void Reset()
        {
            foreach (var v in ProcessVertices)
            {
                v.Check = false;
                v.ForwardCheck = false;
                v.ReverseCheck = false;
                v.ForwardDist = Infinity;
                v.ReverseDist = Infinity;
            }
            foreach (var e in ProcessEdges)
            {
                e.ForwardCheck = false;
                e.ReverseCheck = false;
                e.Check = false;
            }
            ProcessEdges = new List<Edge>();
            ProcessVertices = new List<Vertex>();
            return;
        }
        private void Relax(Vertex v)
        {
            foreach (var edge in v.Edges)
                if (!edge.Check)
                    edge.To.Dist = v.Dist + edge.Weight < edge.To.Dist ? v.Dist + edge.Weight : edge.To.Dist;
        }
        
        private void BidirectionalRelax(Vertex v, bool isForward)
        {
            if (isForward)
            {
                foreach (var edge in v.Edges)
                {
                    if (!edge.ForwardCheck && v.ForwardDist + edge.Weight < edge.To.ForwardDist )
                    {
                        edge.To.ForwardDist = v.ForwardDist + edge.Weight;
                        ProcessVertices.Add(edge.To);
                    }
                }
            }
            else
            {
                foreach (var edge in v.EdgesTranspose)
                {
                    if (!edge.ReverseCheck && v.ReverseDist + edge.Weight < edge.To.ReverseDist)
                    {
                        edge.To.ReverseDist = v.ReverseDist + edge.Weight;
                        ProcessVertices.Add(edge.To);
                    }
                }
            }
            return;
            
        }
    }

    
}
