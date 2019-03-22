using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Entity
    {
        public Vertex Item { get; set; }
        public double Priority { get; set; }
        public Entity(Vertex item, double priority)
        {
            Item = item;
            Priority = priority;
        }
    }
    public class PriorityQueue
    {
        public Entity[] Elements { get; set; }
        public int Cursor { get; set; }
        public bool IsForward { get; set; }

        public PriorityQueue(bool isFwd, int size = 1000)
        {
            IsForward = isFwd;
            Elements = new Entity[size];
            Cursor = 0;
        }
        private void Swap(int firstIndex, int secondIndex)
        {
            if (IsForward)
            {
                Elements[firstIndex].Item.FwdIndex = secondIndex;
                Elements[secondIndex].Item.FwdIndex = firstIndex;
            }
            else
            {
                Elements[firstIndex].Item.BwdIndex = secondIndex;
                Elements[secondIndex].Item.BwdIndex = firstIndex;
            }
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
        public void ChangePriority(Vertex query,int newPriority)
        {
            var index = -1;
            if (IsForward)
                index = query.FwdIndex;
            else
                index = query.BwdIndex;
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
        public int FindIndex(Vertex query)
        {
            for (int i = 0; i < Cursor; i++)
                if (Elements[i].Item == query)
                    return i;
            return -1;
        }
        public Vertex ExtraxtMin()
        {
            if (Cursor == 0)
                return null;
            if (IsForward)
                Elements[0].Item.FwdIndex = -1;
            else
                Elements[0].Item.BwdIndex = -1;
            var result = Elements[0];
            Cursor--;
            Elements[0] = Elements[Cursor];
            Elements[Cursor] = null;
            if (Elements[0] != null)
                if (IsForward)
                    Elements[0].Item.FwdIndex = -1;
                else
                    Elements[0].Item.BwdIndex = -1;
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
        public void Add(Vertex query,double priority)
        {
            if (IsForward)
                query.FwdIndex = Cursor;
            else
                query.BwdIndex = Cursor;
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
