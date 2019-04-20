using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A7;

namespace A7
{
    class Program
    {
        static void Main(string[] args)
        {
            SuffixArray ss = new SuffixArray("ATATATA$");
            var X = ss.ConstructSuffixArray();
            foreach (var item in X)
            {
                //Console.WriteLine(item);
            }
            var DD = ss.Search("TATAT");
            foreach (var item in DD)
            {
                Console.WriteLine(item);
            }
            foreach (var item in ss.Order)
            {
                //Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
