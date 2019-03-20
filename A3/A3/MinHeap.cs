using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class MinHeapFwd
    {
        public Edge[] _elements;
        private int _size;

        public MinHeapFwd(int size)
        {
            _elements = new Edge[size];
            _size = 0;
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Edge GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private Edge GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private Edge GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }
        public void Update()
        {
            for (int i = 0; i < _size; i++)
            {
                ReCalculateUp(i);
            }
        }
        public Edge Peek()
        {
            if (_size == 0)
                return null;

            return _elements[0];
        }

        public Edge Pop()
        {
            if (_size == 0)
                return null;

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown(0);

            return result;
        }

        public void Add(Edge element)
        {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException();

            _elements[_size] = element;
            _size++;

            ReCalculateUp(_size - 1);
        }
        private void ReCalculateDown(int index)
        {
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).To.ForwardDist < GetLeftChild(index).To.ForwardDist)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].To.ForwardDist >= _elements[index].To.ForwardDist)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp(int index)
        {
            while (!IsRoot(index) && _elements[index].To.ForwardDist < GetParent(index).To.ForwardDist)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }

    public class MinHeapBwd
    {
        private Edge[] _elements;
        private int _size;

        public MinHeapBwd(int size)
        {
            _elements = new Edge[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Edge GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private Edge GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private Edge GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }
        public void Update()
        {
            for (int i = 0; i < _size; i++)
            {
                ReCalculateUp(i);
            }
        }
        public Edge Peek()
        {
            if (_size == 0)
                return null;

            return _elements[0];
        }

        public Edge Pop()
        {
            if (_size == 0)
                return null;

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown(0);

            return result;
        }
        public void Add(Edge element)
        {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException();

            _elements[_size] = element;
            _size++;

            ReCalculateUp(_size - 1);
        }

        private void ReCalculateDown(int index)
        {
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).To.ReverseDist < GetLeftChild(index).To.ReverseDist)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].To.ReverseDist >= _elements[index].To.ReverseDist)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp(int index)
        {
            while (!IsRoot(index) && _elements[index].To.ReverseDist < GetParent(index).To.ReverseDist)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}
