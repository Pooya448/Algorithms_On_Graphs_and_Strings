using Microsoft.SolverFoundation.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A3
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            var variables = V * 3;
            var constraints = V + matrix.GetLength(0) * 3;
            List<string> result = new List<string>();
            result.Add($"{variables} {constraints}");

            for (int v = 0; v < V; v++)
            {
                StringBuilder s = new StringBuilder();

                for (int j = 1; j <= 3; j++)
                    s.Append($"{(v*3)+j} ");

                s.Append("0");
                result.Add(s.ToString());
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j <= 3; j++)
                    result.Add($"-{(matrix[i, 0] - 1) * 3 + j} -{(matrix[i, 1] - 1) * 3 + j} 0");

            return result.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
