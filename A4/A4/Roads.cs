using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    class Roads
    {
        public const int Infinity = 2_000_000_000;
        public List<Point> Points { get; set; }
        public int PointCount { get; set; }
        public Roads(long pointCount, long[][] edges,long[][] points, bool isBiDirectional = false)
        {
            Points = new List<Point>();
            PointCount = (int)pointCount;
            for (int i = 0; i < points.GetLength(0); i++)
                Points.Add(new Point(i, (int)points[i][0], (int)points[i][1]));
            for (int i = 0; i < edges.GetLength(0); i++)
                Points[(int)edges[i][0] - 1].Edges.Add(new Edge(Points[(int)edges[i][0] - 1], Points[(int)edges[i][1] - 1], (int)edges[i][2]));

        }
        public long A(int s, int t)
        {
            if (s == t)
                return 0;
            var start = Points[s - 1];
            var target = Points[t - 1];
            int processCount = PointCount;
            start.Dist = 0;
            Point pivot = start;
            PriorityQueue queue = new PriorityQueue(2000);
            CalcHeuristics(start,target);
            while (processCount > 0)
            {
                pivot.Check = true;
                if (pivot.Id == target.Id)
                {
                    var r = pivot.Dist;
                    foreach (var p in Points)
                        Reset(p);
                    return (long)r;
                }
                foreach (var edge in pivot.Edges)
                {
                    if (!edge.To.Check && pivot.Dist + edge.Weight < edge.To.Dist)
                    {
                        edge.To.Dist = pivot.Dist + edge.Weight;
                        if (edge.To.Index >= 0)
                            queue.ChangePriority(edge.To, edge.To.Dist + edge.To.Heuristic);
                        else
                            queue.Add(edge.To, edge.To.Dist + edge.To.Heuristic);
                    }
                }
                var nextPivot = queue.ExtraxtMin();
                if (nextPivot != null)
                {
                    nextPivot.Check = true;
                    pivot = nextPivot;
                }
                processCount--;
            }
            var result = target.Dist == Infinity ? -1 : (long)target.Dist;
            foreach (var p in Points)
                Reset(p);
            return result;
        }

        private void CalcHeuristics(Point start, Point target)
        {
            foreach (var p in Points)
                p.Heuristic = CalcDistance(p, target);
        }

        public double CalcDistance(Point p1, Point p2)
            => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

        private void Reset(Point p)
        {
            p.Dist = Infinity;
            p.Check = false;
            p.Heuristic = 0;
            p.Index = -1;
        }
    }
}
