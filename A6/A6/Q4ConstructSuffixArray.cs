using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            (string, int)[] suffixes = new (string, int)[text.Length];
            for (int i = 0; i < text.Length; i++)
                suffixes[i] = (text.Substring(i),i);
            return suffixes.OrderBy(x => x.Item1).Select(t => (long)t.Item2).ToArray();
        }
    }
}
