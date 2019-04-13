using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> l = new List<long>();
            SuffixTrie s = new SuffixTrie();
            foreach (var str in patterns)
                s.AddString(str);
            s.SetString(text);
            for (int i = 0; i < text.Length; i++)
                if (s.Find(i))
                    l.Add(i);
            if (!l.Any())
                return new long[] { -1 };
            return l.ToArray();
        }
    }
}
