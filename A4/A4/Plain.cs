using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace A4
{
    class Plain
    {
        public const int Infinity = 2_000_000_000;
        public List<Point> Points { get; set; }
        public List<double> MST { get; set; }
        public int PointCount { get; set; }

        public Plain(long pointCount, long[][] points)
        {
            MST = new List<double>();
            Points = new List<Point>();
            PointCount = (int)pointCount;
            for (int i = 0; i < points.GetLength(0); i++)
                Points.Add(new Point(i,(int)points[i][0],(int)points[i][1]));
            for (int i = 0; i < Points.Count; i++)
                for (int j = i + 1; j < Points.Count; j++)
                {
                    Points[i].ConnectedPoints.Add(Points[j]);
                    Points[j].ConnectedPoints.Add(Points[i]);
                }
        }
        public double CalcMST(Point start)
        {
            int processCount = PointCount - 1;
            start.Dist = 0;
            Point currentPoint = start;
            List<Point> process = new List<Point>();
            while (processCount > 0)
            {
                Relax(currentPoint);
                currentPoint.Check = true;
                foreach (var p in currentPoint.ConnectedPoints)
                    if (!p.Check && !p.IsInQueue)
                    {
                        process.Add(p);
                        p.IsInQueue = true;
                    }
                var minKey = process.OrderBy(x => x.Dist).FirstOrDefault();
                if (minKey != null)
                {
                    MST.Add(CalcDistance(minKey, minKey.Prev));
                    process.Remove(minKey);
                    currentPoint = minKey;
                }
                processCount--;
            }
            return Math.Round(MST.Sum(),6);
        }
        private void Relax(Point p)
        {
            foreach (var point in p.ConnectedPoints)
                if (!point.Check && point.Dist > CalcDistance(p, point))
                {
                    point.Dist = CalcDistance(p, point);
                    point.Prev = p;
                }
        }
        public double CalcDistance(Point p1, Point p2)
            => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
}
