using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    class Charts
    {
        int[,] BipartiteGraph;
        public int ChartCount { get; set; }
        public Charts(long stockCount, long pointCount, long[][] matrix)
        {
            ChartCount = (int)stockCount;
            BipartiteGraph = new int[stockCount, stockCount];
            for (int i = 0; i < BipartiteGraph.GetLength(0); i++)
                for (int j = 0; j < BipartiteGraph.GetLength(1); j++)
                    BipartiteGraph[i, j] = 1;
            CreateGraph((int)pointCount, matrix);
            return;
        }
        public long NumberOfSheets()
        {
            ResidualNetwork rn = new ResidualNetwork(ChartCount, ChartCount, BipartiteGraph);
            rn.FindBipartite();
            return ChartCount - rn.MaxFlow;
        }
        public void CreateGraph(int pointCount, long[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix.Length; j++)
                    for (int k = 0; k < pointCount; k++)
                        if (matrix[i][k] >= matrix[j][k])
                            BipartiteGraph[i, j] = 0;
            return;
        }
    }
}
