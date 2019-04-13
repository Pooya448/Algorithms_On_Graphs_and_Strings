using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    class SuffixTree
    {
        public Branch Root { get; set; }
        public int Counter { get; set; }
        public string String { get; set; }
        public SuffixTree(string str)
        {
            String = str;
            Counter = 0;
        }
        public void AddString(int index)
        {
            var pivot = Root;
            for (int i = index; i < String.Length; i++)
            {
                foreach (var c in pivot.Descendants)
                {
                    nextOuterLoop:;
                    if (c.Val[Counter] == String[i])
                    {
                        pivot = c;
                        Counter++;
                        goto nextOuterLoop;
                    }
                    var temp = new Branch(String[i].ToString(), pivot.Id);
                    pivot.Descendants.Add(temp);
                    pivot = temp;
                }
                    
                
                //nextOuterLoop:;
                if (i == String.Length - 1)
                    pivot.Last = true;
            }
        }
        public void CreateTree(string str)
        {
            for (int i = 0; i < str.Length; i++)
                AddString(i);
        }
    }
    class Branch
    {
        public int Id { get; set; }
        public int Prev { get; set; }
        public string Val { get; set; }
        public bool Last { get; set; }
        public List<Branch> Descendants { get; set; }
        public Branch(string val, int p)
        {
            Descendants = new List<Branch>();
            //Id = id;
            Prev = p;
            Val = val;
        }
    }
}
