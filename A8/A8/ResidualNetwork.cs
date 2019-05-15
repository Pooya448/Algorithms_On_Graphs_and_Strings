using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    public class ResidualNetwork
    {
        public int N { get; set; }
        public int M { get; set; }
        public int[,] AdjacencyMatrix { get; private set; }
        public bool[] IsChecked { get; private set; }
        public int[] Prevs { get; set; }
        public int NodeCount { get; private set; }
        public long MaxFlow { get; set; }
        public ResidualNetwork(int n,int m, long[][] edges, bool b)
        {
            M = m;
            N = n;
            NodeCount = n + m + 2;
            MaxFlow = 0;
            AdjacencyMatrix = new int[NodeCount, NodeCount];
            IsChecked = new bool[NodeCount];
            Prevs = new int[NodeCount];
            Prevs = Enumerable.Repeat(-1, NodeCount).ToArray();

            for (int i = 0; i < edges.Length; i++)
                for (int j = 0; j < edges[i].Length; j++)
                    if (edges[i][j] == 1)
                        AdjacencyMatrix[i + 1, j + N + 1] = 1;

            for (int i = 1; i <= N; i++)
                AdjacencyMatrix[0, i] = 1;

            for (int i = N + 1; i <= N + M; i++)
                AdjacencyMatrix[i, N + M + 1] = 1;
        }
        public ResidualNetwork(long nodeCount, long[][] edges)
        {
            MaxFlow = 0;
            NodeCount = (int)nodeCount;
            AdjacencyMatrix = new int[NodeCount, NodeCount];
            IsChecked = new bool[NodeCount];
            Prevs = new int[NodeCount];
            Prevs = Enumerable.Repeat(-1, NodeCount).ToArray();

            for (int i = 0; i < edges.GetLength(0); i++)
                AdjacencyMatrix[edges[i][0] - 1, edges[i][1] - 1] += (int)edges[i][2];
        }
        public long[] FindBipartite()
        {
            long[] result = new long[N];
            result = Enumerable.Repeat((long)-1, N).ToArray();

            FindMaxFlow();

            for (int i = N + 1; i <= N + M; i++)
                for (int j = 1; j <= N; j++)
                    if (AdjacencyMatrix[i,j] == 1)
                        result[j - 1] = i - N;
            return result;
        }
        public long FindMaxFlow()
        {
            while (FindPath())
                CalculateMaxFlow();
            return MaxFlow;
        }
        public bool FindPath()
        {
            int end = NodeCount - 1;
            int start = 0;
            Queue<int> bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(start);
            IsChecked[start] = true;
            while (bfsQueue.Any())
            {
                var temp = bfsQueue.Dequeue();
                if (temp == end)
                    return true;
                for (int i = 0; i < AdjacencyMatrix.GetLength(1); i++)
                    if (AdjacencyMatrix[temp,i] > 0 && !IsChecked[i])
                    {
                        Prevs[i] = temp;
                        bfsQueue.Enqueue(i);
                        IsChecked[i] = true;
                    }
            }
            return false;
        }
        public void CalculateMaxFlow()
        {
            List<int> path = new List<int>();
            List<int> values = new List<int>();
            var pivot = NodeCount - 1;
            if (Prevs[pivot] == -1)
                return;
            while (pivot != 0)
            {
                values.Add(AdjacencyMatrix[Prevs[pivot], pivot]);
                path.Add(pivot);
                pivot = Prevs[pivot];
            }
            path.Add(0);
            var minFlow = values.Min();
            path.Reverse();
            for (int i = 0; i < path.Count - 1; i++)
            {
                AdjacencyMatrix[path[i], path[i + 1]] -= minFlow;
                AdjacencyMatrix[path[i + 1], path[i]] += minFlow;
            }
            MaxFlow += minFlow;
            Reset();
            return;
        }
        public void Reset()
        {
            for (int i = 0; i < IsChecked.Length; i++)
                IsChecked[i] = false;
            for (int i = 0; i < Prevs.Length; i++)
                Prevs[i] = -1;
        }
    }
}
