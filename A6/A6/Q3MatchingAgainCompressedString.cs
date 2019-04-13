using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            long[] res = new long[n];
            List<(char, int)> bwtIndexed = new List<(char, int)>();
            for (int i = 0; i < text.Length; i++)
                bwtIndexed.Add((text[i], i));
            List<(char, int)> firstCol = bwtIndexed.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();
            for (int i = 0; i < text.Length; i++)
                bwtIndexed[firstCol[i].Item2] = (bwtIndexed[firstCol[i].Item2].Item1, i);
            for (int k = 0; k < patterns.Length; k++)
            {
                int highPointer = 0, lowPointer = text.Length;
                var pattern = patterns[k];
                for (int i = pattern.Length - 1; i >= 0; i--)
                {
                    bool highPointerSet = false;
                    var lp = lowPointer;
                    for (int j = highPointer; j < lp; j++)
                        if (bwtIndexed[j].Item1 == pattern[i])
                        {
                            if (!highPointerSet)
                            {
                                highPointer = bwtIndexed[j].Item2;
                                highPointerSet = true;
                            }
                            lowPointer = bwtIndexed[j].Item2 + 1;
                        }
                    if (!highPointerSet)
                    {
                        res[k] = 0;
                        goto NextPattern;
                    }
                }
                res[k] = lowPointer - highPointer;
                NextPattern:;
            }
            return res;
        }
    }
}
