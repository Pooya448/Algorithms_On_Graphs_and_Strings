using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Point
    {
        public const int Infinity = 2_000_000_000;
        public double Dist { get; set; }
        public List<Point> ConnectedPoints { get; set; }
        public List<Edge> Edges { get; set; }
        public int Id { get; set; }
        public bool Check { get; set; }
        public Point Prev { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsInQueue { get; internal set; }
        public int Index { get; set; }
        public double Heuristic { get; set; }

        public Point(int id, int x,int y)
        {
            IsInQueue = false;
            Id = id;
            X = x;
            Y = y;
            Check = false;
            Edges = new List<Edge>();
            ConnectedPoints = new List<Point>();
            Dist = Infinity;
            Index = -1;
            Heuristic = -1;
            return;
        }
    }
}
