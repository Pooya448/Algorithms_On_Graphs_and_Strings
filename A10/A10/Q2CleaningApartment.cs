using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            List<string> result = new List<string>();
            var variables = V * V;
            for (int v = 1; v <= V; v++)
            {
                StringBuilder s = new StringBuilder();
                for (int j = 1; j <= V; j++)
                    s.Append($"{((v - 1) * V) + j} ");
                s.Append("0");
                result.Add(s.ToString());

                for (int i = (v - 1) * V + 1; i <= (v - 1) * V + V; i++)
                    for (int j = i + 1; j <= (v - 1) * V + V; j++)
                        result.Add($"-{i} -{j} 0");
            }
            for (int i = 1; i <= V; i++)
            {
                StringBuilder t = new StringBuilder();
                for (int j = i; j <= V * V; j+=V)
                    t.Append($"{j} ");
                t.Append("0");
                result.Add(t.ToString());
            }
            for (int i = 1; i <= V; i++)
                for (int j = i; j <= V * V; j += V)
                    for (int k = j + V; k <= V * V; k += V)
                        result.Add($"-{j} -{k} 0");
            var adjacencyMatrix = MatrixMaker(matrix, V);
            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                    if (adjacencyMatrix[i,j] == 0 && i != j)
                        for (int k = 1; k < V; k++)
                            result.Add($"-{(i * V) + k} -{(j * V) + k + 1} 0");
            var constraints = result.Count;
            result.Insert(0, $"{variables} {constraints}");
            return result.ToArray();
        }
        int[,] MatrixMaker(long[,] edges, int v)
        {
            int[,] matrix = new int[v, v];
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                matrix[edges[i, 0] - 1, edges[i, 1] - 1] = 1;
                matrix[edges[i, 1] - 1, edges[i, 0] - 1] = 1;
            }
            return matrix;
        }
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
