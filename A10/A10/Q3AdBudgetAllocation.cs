using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) 
            : base(testDataName)
        {
            ExcludeTestCases(1, 14, 35);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < eqCount; i++)
            {
                List<int> vars = new List<int>();

                for (int j = 0; j < varCount; j++)
                    if (A[i][j] != 0)
                        vars.Add(j);

                int jMax = (int)Math.Pow(2, vars.Count);

                for (int j = 0; j < jMax; j++)
                {
                    long sum = 0;
                    var binResult = GetBinary(j, vars.Count);

                    for (int k = 0; k < binResult.Length; k++)
                        sum += binResult[k] * A[i][vars[k]];

                    if (sum > b[i])
                    {
                        StringBuilder strBuilder = new StringBuilder();
                        for (int m = 0; m < binResult.Length; m++)
                            if (binResult[m] == 0)
                                strBuilder.Append($"-{vars[m]+1} ");
                            else if (binResult[m] == 1)
                                strBuilder.Append($"{vars[m]+1} ");

                        strBuilder.Append("0");
                        result.Add(strBuilder.ToString());
                    }
                }
            }
            result.Insert(0, $"{varCount} {result.Count.ToString()}");
            return result.ToArray();
        }
        static int[] GetBinary(int n, int no)
        {
            // method is from https://www.dotnetperls.com/integer-increment-binary with changes made to it
            char[] b = new char[32];
            int pos = 31;
            int i = 0;
            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                    b[pos] = '1';
                else
                    b[pos] = '0';
                pos--;
                i++;
            }
            int[] binary = new int[no];
            for (int j = b.Length - 1, k = no - 1; k >= 0 ; j--,k--)
                binary[k] = int.Parse(b[j].ToString());
            return binary;
        }
        
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
