using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Entity
    {
        public Point Item { get; set; }
        public double Priority { get; set; }
        public Entity(Point item, double priority)
        {
            Item = item;
            Priority = priority;
        }
    }
    public class PriorityQueue
    {
        public Entity[] Elements { get; set; }
        public int Cursor { get; set; }

        public PriorityQueue(int size = 1000)
        {
            Elements = new Entity[size];
            Cursor = 0;
        }
        private void Swap(int firstIndex, int secondIndex)
        {
            
            Elements[firstIndex].Item.Index = secondIndex;
            Elements[secondIndex].Item.Index = firstIndex;
            
            var temp = Elements[firstIndex];
            Elements[firstIndex] = Elements[secondIndex];
            Elements[secondIndex] = temp;
        }
        public bool IsEmpty()
            => Cursor == 0;

        public Entity Peek()
        {
            if (Cursor == 0)
                return null;

            return Elements[0];
        }
        public void ChangePriority(Point query,double newPriority)
        {
            var index = query.Index;
            if (index >= 0)
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
        public int FindIndex(Point query)
        {
            for (int i = 0; i < Cursor; i++)
                if (Elements[i].Item == query)
                    return i;
            return -1;
        }
        public Point ExtraxtMin()
        {
            if (Cursor == 0)
                return null;
            Elements[0].Item.Index = -1;
            var result = Elements[0];
            Cursor--;
            Elements[0] = Elements[Cursor];
            Elements[Cursor] = null;
            if (Elements[0] != null)
                Elements[0].Item.Index = -1;
            SiftDown(0);
            return result.Item;
        }
        private void SiftDown(int index)
        {
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).Priority < GetLeftChild(index).Priority)
                    smallerIndex = GetRightChildIndex(index);
                if (Elements[smallerIndex].Priority >= Elements[index].Priority)
                    break;
                Swap(smallerIndex, index);
                index = smallerIndex;
            }
            return;
        }
        private void SiftUp(int index)
        {
            while (!IsRoot(index) && Elements[index].Priority < GetParent(index).Priority)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
            return;
        }
        public void Add(Point query,double priority)
        {
            query.Index = Cursor;
            var element = new Entity(query,priority);
            Elements[Cursor] = element;
            SiftUp(Cursor);
            Cursor++;
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < Cursor;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < Cursor;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Entity GetLeftChild(int elementIndex) => Elements[GetLeftChildIndex(elementIndex)];
        private Entity GetRightChild(int elementIndex) => Elements[GetRightChildIndex(elementIndex)];
        private Entity GetParent(int elementIndex) => Elements[GetParentIndex(elementIndex)];
    }
}
