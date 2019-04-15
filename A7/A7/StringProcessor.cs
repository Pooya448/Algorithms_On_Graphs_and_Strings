using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    class StringProcessor
    {
        public string StringInstance { get; set; }
        public string Pattern { get; set; }
        public int[] PrefixArray { get; set; }
        private int PatternCursor;
        public StringProcessor(string str, string pattern)
        {
            StringInstance = str;
            Pattern = pattern;
        }
        public long[] FindOccurrences()
        {
            List<long> res = new List<long>();
            PatternCursor = 0;
            PrefixArray = new int[Pattern.Length + StringInstance.Length + 1];
            var text = Pattern + "$" + StringInstance;
            PrefixArray[0] = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[PatternCursor] == text[i])
                    PrefixArray[i] = ++PatternCursor;
                else
                    while (PatternCursor > 0)
                    {
                        PatternCursor = PrefixArray[PatternCursor - 1];
                        if (text[PatternCursor] == text[i])
                        {
                            PrefixArray[i] = ++PatternCursor;
                            break;
                        }
                    }
                if (PrefixArray[i] == Pattern.Length)
                    res.Add(i - 2 * Pattern.Length);
            }
            if (!res.Any())
                return new long[] { -1 };
            return res.ToArray();
        }
    }
}
