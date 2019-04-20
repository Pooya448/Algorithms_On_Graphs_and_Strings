using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve,"\n");

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> results = new List<long>();
            SuffixArray s = new SuffixArray(text + '$');
            var suffixarray = s.ConstructSuffixArray();
            foreach (var pattern in patterns)
            {
                var res = s.Search(pattern);
                foreach (var index in res)
                    if (!results.Contains(index))
                        results.Add(index);
            }
            if (!results.Any())
                return new long[] { -1 };
            return results.ToArray();
        }
    }
}
