using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Entity<T> where T : class
    {
        public T Item { get; set; }
        public double Priority { get; set; }
        public Entity(T item, double priority)
        {
            Item = item;
            Priority = priority;
        }
    }
    public class PriorityQueue<T> where T : class
    {
        public Entity<T>[] Elements { get; set; }
        public int Cursor { get; set; }

        public PriorityQueue(int size = 1000)
        {
            Elements = new Entity<T>[size];
            Cursor = 0;
        }
        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = Elements[firstIndex];
            Elements[firstIndex] = Elements[secondIndex];
            Elements[secondIndex] = temp;
        }
        public bool IsEmpty()
        {
            return Cursor == 0;
        }
        public Entity<T> Peek()
        {
            if (Cursor == 0)
                return null;

            return Elements[0];
        }
        public void ChangePriority(T query,int newPriority)
        {
            var index = FindIndex(query);
            if (index != -1)
            {
                var currentPriority = Elements[index].Priority;
                Elements[index].Priority = newPriority;
                if (newPriority < currentPriority)
                    SiftUp(index);
                else
                    SiftDown(index);
            }
            return;
        }
        public int FindIndex(T query)
        {
            for (int i = 0; i < Cursor; i++)
            {
                if (Elements[i].Item == query)
                {
                    return i;
                }
            }
            return -1;
        }
        public Entity<T> ExtraxtMin()
        {
            if (Cursor == 0)
                return null;

            var result = Elements[0];
            Elements[0] = Elements[Cursor - 1];
            Cursor--;

            SiftDown(0);

            return result;
        }
        private void SiftDown(int index)
        {
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).Priority < GetLeftChild(index).Priority)
                {
                    smallerIndex = GetRightChildIndex(index);
                }
                if (Elements[smallerIndex].Priority >= Elements[index].Priority)
                {
                    break;
                }
                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }
        private void SiftUp(int index)
        {
            while (!IsRoot(index) && Elements[index].Priority < GetParent(index).Priority)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
        public void Add(T query,double priority)
        {
            var element = new Entity<T>(query,priority);
            Elements[Cursor] = element;
            Cursor++;
            SiftUp(Cursor - 1);
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < Cursor;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < Cursor;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Entity<T> GetLeftChild(int elementIndex) => Elements[GetLeftChildIndex(elementIndex)];
        private Entity<T> GetRightChild(int elementIndex) => Elements[GetRightChildIndex(elementIndex)];
        private Entity<T> GetParent(int elementIndex) => Elements[GetParentIndex(elementIndex)];
    }
}
