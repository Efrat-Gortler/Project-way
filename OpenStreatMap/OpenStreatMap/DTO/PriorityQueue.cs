using System;
using System.Collections.Generic;
using OpenStreatMap.Manager;
namespace OpenStreatMap.DTO
    
{
   
        public class PriorityQueue<T> where T : IComparable<T>
        {
            private List<T> heap = new List<T>();

            public int Count => heap.Count;

            public PriorityQueue(List<T> elements)
            {
                foreach (var element in elements)
                {
                    Insert(element);
                }
            }

            public void Insert(T element)
            {
                heap.Add(element);
                HeapifyUp(heap.Count - 1);
            }

            public T ExtractMin()
            {
                if (heap.Count == 0)
                {
                    throw new InvalidOperationException("Priority queue is empty");
                }

                T min = heap[0];
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);
                HeapifyDown(0);

                return min;
            }

            public void UpdatePriority(T element)
            {
                int index = heap.IndexOf(element);
                HeapifyUp(index);
                HeapifyDown(index);
            }

            private void HeapifyUp(int index)
            {
                while (index > 0)
                {
                    int parentIndex = (index - 1) / 2;

                    if (heap[index].CompareTo(heap[parentIndex]) < 0)
                    {
                        Swap(index, parentIndex);
                        index = parentIndex;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            private void HeapifyDown(int index)
            {
                while (true)
                {
                    int leftChildIndex = 2 * index + 1;
                    int rightChildIndex = 2 * index + 2;

                    int smallestChildIndex = index;

                    if (leftChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[smallestChildIndex]) < 0)
                    {
                        smallestChildIndex = leftChildIndex;
                    }

                    if (rightChildIndex < heap.Count && heap[rightChildIndex].CompareTo(heap[smallestChildIndex]) < 0)
                    {
                        smallestChildIndex = rightChildIndex;
                    }

                    if (smallestChildIndex != index)
                    {
                        Swap(index, smallestChildIndex);
                        index = smallestChildIndex;
                    }

                //if (heap[index].CompareTo(heap[parentIndex]) < 0)
                //{
                //    Swap(index, parentIndex);
                //    index = parentIndex;
                //}
                else
                    {
                        break;
                    }
                }
            }

            private void Swap(int index1, int index2)
            {
                T temp = heap[index1];
                heap[index1] = heap[index2];
                heap[index2] = temp;
            }
        }
    }

