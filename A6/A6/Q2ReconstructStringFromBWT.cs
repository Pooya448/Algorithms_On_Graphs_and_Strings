using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string bwt)
        {
            string bwtSorted = bwt.ToCharArray().OrderBy(x => x).ToString();
            List<(char, int)> bwtIndexed = new List<(char, int)>();
            for (int i = 0; i < bwt.Length; i++)
                bwtIndexed.Add((bwt[i], i));
            List<(char,int)> firstCol = bwtIndexed.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();
            for (int i = 0; i < bwt.Length; i++)
                bwtIndexed[firstCol[i].Item2] = (bwtIndexed[firstCol[i].Item2].Item1, i);
            StringBuilder decodeBWT = new StringBuilder();
            decodeBWT.Append(firstCol[0].Item1.ToString());
            int pivot = 0;
            for (int i = 1; i < bwt.Length; i++)
            {
                decodeBWT.Append(bwtIndexed[pivot].Item1);
                pivot = bwtIndexed[pivot].Item2;
            }
            var res = decodeBWT.ToString();
            return new string(res.Reverse().ToArray());
        }
    }
}
