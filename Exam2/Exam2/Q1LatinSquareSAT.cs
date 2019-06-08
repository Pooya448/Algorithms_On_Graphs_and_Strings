using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;


        public string Solve(int dim, int?[,] square)
        {
            int resultsCount = 0;
            StringBuilder strResults = new StringBuilder();
            int n = square.GetLength(0);
            int n2 = n * n;
            int n3 = n * n * n;
            StringBuilder strBuilder;
            for (int i = 0; i < n2; i++)
            {
                strBuilder = new StringBuilder();
                for (int j = 1; j <= n; j++)
                {
                    strBuilder.Append($"{i * n + j} ");
                    resultsCount++;
                }
                strBuilder.Append("\n");
                strResults.Append(strBuilder.ToString());
                resultsCount++;
                for (int j = 1; j <= n; j++)
                    for (int k = j + 1; k <= n; k++)
                    {
                        strResults.Append($"-{i * n + j} -{i * n + k}\n");
                        resultsCount++;
                    }
            }
            
            for (int m = 0; m < n; m++)
                for (int i = m * n2 + 1; i <= m * n2 + n; i++)
                {
                    strBuilder = new StringBuilder();
                    for (int k = i; k <= n * n * (m + 1); k += n)
                    {
                        strBuilder.Append($"{k} ");
                        for (int j = k + n; j <= n * n * (m + 1); j += n)
                        {
                            strResults.Append($"-{k} -{j}\n");
                            resultsCount++;
                        }
                    }
                    strResults.Append(strBuilder.ToString() + "\n");
                    resultsCount++;
                }
            for (int m = 0; m < n; m++)
                for (int i = m * n + 1; i <= m * n + n; i++)
                {
                    strBuilder = new StringBuilder();
                    for (int k = i; k <= n * n * n; k += n * n)
                    {
                        strBuilder.Append($"{k} ");
                        for (int j = k + n * n; j <= n * n * n; j += n * n)
                        {
                            strResults.Append($"-{k} -{j}\n");
                            resultsCount++;
                        }
                    }
                    strResults.Append(strBuilder.ToString() + "\n");
                    resultsCount++;
                }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (square[i, j].HasValue)
                    {
                        var x = i * n + j;
                        strResults.Append($"{x * n + square[i, j].Value + 1} \n");
                        resultsCount++;
                    }
            strResults.Insert(0, $"{resultsCount} {n3}\n");
            return strResults.ToString();
        }
    }
}
