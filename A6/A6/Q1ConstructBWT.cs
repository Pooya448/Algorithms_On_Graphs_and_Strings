using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            string[] Matrix = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
                Matrix[i] = text.Substring(i) + text.Substring(0, i);
            Matrix = Matrix.OrderBy(x => x).ToArray();
            string BWT = string.Empty;
            for (int i = 0; i < Matrix.GetLength(0); i++)
                BWT += Matrix[i].Last();
            return BWT;
        }
    }
}
