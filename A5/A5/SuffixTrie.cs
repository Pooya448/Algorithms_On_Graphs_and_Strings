using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    class SuffixTrie
    {
        public string String { get; set; }
        public int Counter { get; set; }
        public Character Root { get; set; }
        public SuffixTrie(string str = null)
        {
            String = str;
            Root = new Character(0, '0',-1);
            Counter = 1;
        }
        public void SetString(string str)
            => String = str;

        public void AddString(string s)
        {
            var pivot = Root;
            for (int i = 0; i < s.Length; i++)
            {
                foreach (var c in pivot.Descendants)
                    if (c.Val == s[i])
                    {
                        pivot = c;
                        goto nextOuterLoop;
                    }
                var tempChar = new Character(Counter++, s[i], pivot.Id);
                pivot.Descendants.Add(tempChar);
                pivot = tempChar;
                nextOuterLoop:;
                if (i == s.Length - 1)
                    pivot.Last = true;
            }
        }
        public void FindAllPatterns(string str)
        {
            for (int i = 0; i < str.Length; i++)
                AddString(str.Substring(i));
        }
        public bool Find(int index)
        {
            var pivot = Root;
            for (int i = index; i < String.Length; i++)
            {
                foreach (var c in pivot.Descendants)
                    if (c.Val == String[i])
                    {
                        pivot = c;
                        if (pivot.Last)
                            return true;
                        goto nextOuterLoop;
                    }
                return false;
                nextOuterLoop:;
            }
            return false;
        }
        public string Traversal()
        {
            if (Counter == 1)
                return string.Empty;
            StringBuilder str = new StringBuilder();
            Stack<Character> dfsStack = new Stack<Character>();
            var pivot = Root;
            dfsStack.Push(pivot);
            while (dfsStack.Any())
            {
                pivot = dfsStack.Pop();
                if (pivot.Descendants.Any())
                    for (int i = pivot.Descendants.Count - 1; i >= 0; i--)
                        dfsStack.Push(pivot.Descendants[i]);
                if (pivot.Prev != -1)
                    str.AppendLine($"{pivot.Prev}->{pivot.Id}:{pivot.Val}");
            }
            return str.ToString();
        }
    }
    class Character
    {
        public int Id { get; set; }
        public int Prev { get; set; }
        public char Val { get; set; }
        public bool Last { get; set; }
        public List<Character> Descendants { get; set; }
        public Character(int id, char val, int p)
        {
            Descendants = new List<Character>();
            Id = id;
            Prev = p;
            Val = val;
        }
    }
}
