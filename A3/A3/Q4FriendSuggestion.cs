using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long NodeCount, long EdgeCount, 
                              long[][] edges, long QueriesCount, 
                              long[][]Queries)
        {
            Dictionary<long[], long> resDict = new Dictionary<long[], long>();
            long[] result = new long[QueriesCount];
            for (int i = 0; i < QueriesCount; i++)
            {
                WeightedGraph g = new WeightedGraph(NodeCount, edges, true);
                if (resDict.ContainsKey(Queries[i]))
                {
                    result[i] = resDict[Queries[i]];
                }
                else
                {
                    resDict[Queries[i]] = g.BidirectionalDijkstra((int)Queries[i][0], (int)Queries[i][1]);
                    result[i] = resDict[Queries[i]];
                }
            }
            
            return result;
        }
    }
}
