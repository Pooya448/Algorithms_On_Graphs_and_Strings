﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Edge
    {
        public Edge(Point p1, Point p2, double w)
        {
            From = p1;
            Point2 = p2;
            Weight = w;
            Check = false;
        }

        public Point From { get; set; }
        public Point Point2 { get; set; }
        public double Weight { get; set; }
        public bool Check { get; set; }
    }
}
