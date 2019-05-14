using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    public class SuffixArray
    {
        public int[] Order { get; set; }
        public int[] ClassArray { get; set; }
        public int Size { get; set; }
        public int Length { get; set; }
        public string StringInstance { get; set; }
        public string Pattern { get; set; }
        public SuffixArray(string Str)
        {
            StringInstance = Str;
            Size = Str.Length;
            Order = new int[Str.Length];
        }
        public int[] ConstructSuffixArray()
        {
            SortCharacters();
            ComputeCharClasses();
            Length = 1;
            while (Length < Size)
            {
                SortDoubled();
                UpdateClasses();
                Length = 2 * Length;
            }
            return Order;
        }
        public void UpdateClasses()
        {
            int n = Order.Length;
            int[] newClass = new int[n];
            newClass[Order[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                int cur = Order[i], prev = Order[i - 1];
                int mid = cur + Length;
                int midPrev = (prev + Length) % n;
                if (ClassArray[cur] != ClassArray[prev] || ClassArray[mid] != ClassArray[midPrev])
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }
            ClassArray = newClass;
            return;
        }
        public void SortDoubled()
        {
            int[] count = new int[Size];
            for (int i = 0; i < count.Length; i++)
                count[i] = 0;
            int[] newOrder = new int[Size];
            for (int i = 0; i < Size; i++)
                count[ClassArray[i]] += 1;
            for (int i = 1; i < Size; i++)
                count[i] = count[i] + count[i - 1];
            for (int i = Size - 1; i >= 0; i--)
            {
                int start = (Order[i] - Length + Size) % Size;
                int cl = ClassArray[start];
                count[cl] -= 1;
                newOrder[count[cl]] = start;
            }
            Order = newOrder;
            return;
        }
        public void SortCharacters()
        {
            Order = new int[Size];
            int[] count = new int['z'];
            for (int i = 0; i < Size; i++)
                count[StringInstance[i]]++;
            for (int i = 1; i < 'z'; i++)
                count[i] = count[i] + count[i - 1];
            for (int i = Size - 1; i >= 0; i--)
            {
                var c = StringInstance[i];
                count[c] -= 1;
                Order[count[c]] = i;
            }
            return;
        }
        public void ComputeCharClasses()
        {
            ClassArray = new int[Size];
            ClassArray[Order[0]] = 0;
            for (int i = 1; i < Size; i++)
                if (StringInstance[Order[i]] != StringInstance[Order[i - 1]])
                    ClassArray[Order[i]] = ClassArray[Order[i - 1]] + 1;
                else
                    ClassArray[Order[i]] = ClassArray[Order[i - 1]];
            return;
        }
        public int[] Search(string pattern)
        {
            Pattern = pattern;
            List<int> result = new List<int>();
            int min = 0;
            int max = Size;
            while (min < max)
            {
                int mid = (min + max) / 2;
                if (CompareStrings(Order[mid]) > 0)
                    min = mid + 1;
                else
                    max = mid;
            }
            if (min < Order.Length && !StartsWith(Order[min]))
                return new int[] { };
            int start = min;
            max = Size;
            while (min < max)
            {
                int mid = (min + max) / 2;
                if (StartsWith(Order[mid]))
                    min = mid + 1;
                else
                    max = mid;
            }
            int end = max;
            if (start > end)
                return new int[] { };
            for (int i = end - 1; i >= start; i--)
                result.Add(Order[i]);
            return result.ToArray();
        }

        public int CompareStrings(int s)
        {
            int patCounter = 0;
            int i;
            for (i = s; i < StringInstance.Length && patCounter < Pattern.Length; i++, patCounter++)
                if (StringInstance[i] == Pattern[patCounter])
                    continue;
                else if (StringInstance[i] > Pattern[patCounter])
                    return -1;
                else
                    return 1;
            if (i == StringInstance.Length && patCounter == Pattern.Length)
                return 0;
            if (i == StringInstance.Length)
                return 1;
            if (patCounter == Pattern.Length)
                return -1;
            return 0;
        }
        public bool StartsWith(int s)
        {
            if (s + Pattern.Length >= StringInstance.Length)
                return false;
            for (int i = s,patCounter = 0; patCounter < Pattern.Length; patCounter++, i++)
                if (StringInstance[i] == Pattern[patCounter])
                    continue;
                else
                    return false;
            return true;
        }
    }
}
