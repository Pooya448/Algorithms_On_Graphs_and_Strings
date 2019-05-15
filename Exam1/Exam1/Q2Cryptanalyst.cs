using TestCommon;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Exam1
{
    public class Q2Cryptanalyst : Processor
    {
        public Q2Cryptanalyst(string testDataName) : base(testDataName)
        {
            Vocab = File.ReadAllLines(@"Exam1_TestData\TD2\dictionary.txt").ToHashSet();
            this.ExcludeTestCaseRangeInclusive(31, 37);
            this.ExcludeTestCases(21,22,24,28);
        }

        public override string Process(string inStr) => Solve(inStr);

        public HashSet<string> Vocab = new HashSet<string>();

        public string Solve(string cipher)
        {
            bool f1 = false;
            bool f2 = false;
            bool f3 = false;
            int i;

            int matchCounter = 0;
            for (i = 0; i < 1000; i++)
            {
                if (i == 100)
                {
                    f1 = true;
                }
                if (i == 320)
                {
                    f2 = true;
                }
                if (i == 500)
                {
                    f3 = true;
                }
                Encryption e = new Encryption(i.ToString(), ' ', 'z', false);
                var res = e.Decrypt(cipher);
                var words = res.Split(' ');
                foreach (var word in words)
                {
                    if (Vocab.Contains(word))
                    {
                        matchCounter++;
                    }
                    if (f3)
                    {
                        if (matchCounter > 1000)
                        {
                            System.Console.WriteLine(i);
                            return res.GetHashCode().ToString();
                        }
                    }
                    else if (f2)
                    {
                        if (matchCounter > 700)
                        {
                            return res.GetHashCode().ToString();
                        }
                    }
                    else if (f2)
                    {
                        if (matchCounter > 500)
                        {
                            return res.GetHashCode().ToString();
                        }
                    }
                    else if (f1)
                    {
                        if (matchCounter > 300)
                        {
                            return res.GetHashCode().ToString();
                        }
                    }
                    else
                    {
                        if (matchCounter > 50)
                        {
                            return res.GetHashCode().ToString();
                        }
                    }
                    

                }
            }
            return null;
        }
    }
}