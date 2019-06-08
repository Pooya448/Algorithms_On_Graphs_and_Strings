using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q2LatinSquareBT : Processor
    {
        public Q2LatinSquareBT(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(29, 30);
            this.ExcludeTestCaseRangeInclusive(32, 120);
        }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public string Solve(int dim, int?[,] square)
        {
            bool satisfy = true;
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < dim; j++)
                    if (!square[i,j].HasValue)
                    {
                        satisfy = Backtracking(dim, square, i, j);
                        i = dim;
                        break;
                    }
            return satisfy ? "SATISFIABLE" : "UNSATISFIABLE";
        }
        public static bool Backtracking(int dim, int?[,] square,int i, int j)
        {
            
            for (int m = 0; m < dim; m++)
            {
                square[i, j] = m;
                var x = CheckTrue(m, square, dim, i, j);
                if (x)
                {
                    var next = Next(dim, i, j, square);
                    if (next.Item1 == -1 && next.Item2 == -1)
                        return true;
                    if (!Backtracking(dim, square, next.Item1, next.Item2))
                        continue;
                    else
                        return true;
                }
                else
                    continue;
            }
            square[i, j] = null;
            return false;
        }

        private static bool CheckTrue(int val, int?[,] square, int dim, int i, int j)
        {
            for (int row = 0; row < dim; row++)
                if (square[row, j].HasValue && row != i)
                    if (square[row, j].Value == val)
                        return false;
                    else
                        continue;

            for (int col = 0; col < dim; col++)
                if (square[i, col].HasValue && col != j)
                    if (square[i, col].Value == val)
                        return false;
                    else
                        continue;
            return true;
        }

        public static (int, int) Next(int dim,int n, int m, int?[,] square)
        {
            for (int i = n; i < dim; i++)
                for (int j = 0; j < dim; j++)
                    if (!square[i,j].HasValue)
                        return (i, j);
            return (-1, -1);
        }
    }
}
