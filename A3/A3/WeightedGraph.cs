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

        public WeightedGraph(long nodeCount, long[][] edges, bool isBiDirectional = false)
        {
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
        private void Relax(Vertex v)
        {
            foreach (var edge in v.Edges)
                if (!edge.Check)
                    edge.To.Dist = v.Dist + edge.Weight < edge.To.Dist ? v.Dist + edge.Weight : edge.To.Dist;
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
        public int BiDijkstra (int s, int t)
        {
            if (s == t)
            {
                return 0;
            }
            int processCount = VertexCount;
            var start = Vertices[s - 1];
            var target = Vertices[t - 1];
            if (!start.Edges.Any())
            {
                return -1;
            }
            var forwardPivot = start;
            var backwardPivot = target;
            forwardPivot.ForwardDist = 0;
            backwardPivot.BackwardDist = 0;

            PriorityQueue fwdQueue = new PriorityQueue(true,3200);
            PriorityQueue bwdQueue = new PriorityQueue(false,3200);

            while (processCount > 0)
            {
                // Forward Dijkstra
                forwardPivot.ForwardCheck = true;
                if (forwardPivot.Id == target.Id)
                {
                    var r = forwardPivot.ForwardDist;
                    foreach (var v in Vertices)
                        Reset(v);
                    return r;
                }
                if (forwardPivot.BackwardCheck)
                    break;

                foreach (var edge in forwardPivot.Edges)
                {
                    if (!edge.To.ForwardCheck && forwardPivot.ForwardDist + edge.Weight < edge.To.ForwardDist)
                    {
                        edge.To.ForwardDist = forwardPivot.ForwardDist + edge.Weight;
                        if (edge.To.FwdIndex >= 0)
                            fwdQueue.ChangePriority(edge.To, edge.To.ForwardDist);
                        else
                            fwdQueue.Add(edge.To, edge.To.ForwardDist);
                    }
                }
                var nextFwdPivot = fwdQueue.ExtraxtMin();
                if (nextFwdPivot != null)
                {
                    forwardPivot = nextFwdPivot;
                }
                // Backward Dijkstra
                backwardPivot.BackwardCheck = true;
                if (backwardPivot.Id == start.Id)
                {
                    var r = backwardPivot.BackwardDist;
                    foreach (var v in Vertices)
                        Reset(v);
                    return r;
                }
                if (backwardPivot.ForwardCheck)
                    break;
                foreach (var edge in backwardPivot.EdgesTranspose)
                {
                    if (!edge.To.BackwardCheck && backwardPivot.BackwardDist + edge.Weight < edge.To.BackwardDist)
                    {
                        edge.To.BackwardDist = backwardPivot.BackwardDist + edge.Weight;
                        if (edge.To.BwdIndex >= 0)
                            bwdQueue.ChangePriority(edge.To, edge.To.BackwardDist);
                        else
                            bwdQueue.Add(edge.To, edge.To.BackwardDist);
                    }
                }
                var nextBwdPivot = bwdQueue.ExtraxtMin();
                if (nextBwdPivot != null)
                    backwardPivot = nextBwdPivot;
                // Finishing Conditions
                if (--processCount == 0)
                {
                    foreach (var v in Vertices)
                        Reset(v);
                    return -1;
                }
            }
            int shortestLength = Infinity;
            foreach (var vertex in Vertices)
            {
                if (vertex.BackwardDist != Infinity &&
                    vertex.ForwardDist != Infinity &&
                    vertex.BackwardDist + vertex.ForwardDist < shortestLength)
                    shortestLength = vertex.BackwardDist + vertex.ForwardDist;
                Reset(vertex);
            }
            return shortestLength;
        }
        public void Reset(Vertex vertex)
        {
            vertex.FwdIndex = -1;
            vertex.BwdIndex = -1;
            vertex.ForwardCheck = false;
            vertex.BackwardCheck = false;
            vertex.BackwardDist = Infinity;
            vertex.ForwardDist = Infinity;
            return;
        }
    }

    
}
