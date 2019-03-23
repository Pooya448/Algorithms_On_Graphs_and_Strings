using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);


        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            Plain p = new Plain(pointCount, points);
            p.CalcMST(p.Points[0]);
            return Math.Round(p.MST.OrderByDescending(x => x).ToArray()[clusterCount - 2], 6);
        }
    }
}
