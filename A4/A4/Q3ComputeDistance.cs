using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long, long[][], long[][], long, long[][], long[]>)Solve);


        public long[] Solve(long nodeCount, 
                            long edgeCount,
                            long[][] points,
                            long[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            Roads r = new Roads(nodeCount, edges, points);
            Dictionary<long[], long> resDict = new Dictionary<long[], long>();
            long[] result = new long[queriesCount];
            for (int i = 0; i < queriesCount; i++)
                if (resDict.ContainsKey(queries[i]))
                    result[i] = resDict[queries[i]];
                else
                {
                    resDict[queries[i]] = r.A((int)queries[i][0], (int)queries[i][1]);
                    result[i] = resDict[queries[i]];
                }
            return result;
        }
    }
}
