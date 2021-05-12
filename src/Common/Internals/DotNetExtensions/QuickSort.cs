using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetExtensions
{
    internal class QuickSort<T>
    {
        private readonly Func<T, T, bool> _isNotDescending;

        public QuickSort(IComparer<T> comparer)
        {
            _isNotDescending = (x, y) => comparer.Compare(x, y) <= 0;
        }
        public QuickSort(Func<T, T, int> comparer)
        {
            _isNotDescending = (x, y) => comparer(x, y) <= 0;
        }

        public T[] Sort(T[] array, bool usePassedArray = false)
        {
            if (!usePassedArray)
                array = (T[])array.Clone();
            Sort(array, 0, array.Length - 1);
            return array;
        }

        public async Task<T[]> SortAsync(T[] array, bool usePassedArray = false)
        {
            if (!usePassedArray)
                array = (T[])array.Clone();
            await SortAsync(array, 0, array.Length - 1);
            return array;
        }

        private void Sort(T[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);
                Sort(array, low, partitionIndex - 1);
                Sort(array, partitionIndex + 1, high);
            }
        }

        private async Task SortAsync(T[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);
                await Task.WhenAll(
                    SortAsync(array, low, partitionIndex - 1),
                    SortAsync(array, partitionIndex + 1, high));
            }
        }

        private int Partition(T[] array, int low, int high)
        {
            //1. Select a pivot point.
            T pivot = array[high];

            int lowIndex = low - 1;

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                T current = array[j];
                if (_isNotDescending(current, pivot))
                {
                    lowIndex++;
                    array[j] = array[lowIndex];
                    array[lowIndex] = current;
                }
            }

            T temp = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp;

            return lowIndex + 1;
        }
    }

    internal static class QuickSort
    {
        public static QuickSort<T> ForComparer<T>(IComparer<T> comparer) =>
            new QuickSort<T>(comparer);

        public static QuickSort<T> ForComparer<T>(Func<T, T, int> comparer) =>
            new QuickSort<T>(comparer);

        public static QuickSort<T> ForComparable<T>() where T : IComparable<T> =>
            new QuickSort<T>((x, y) => x.CompareTo(y));

        public static QuickSort<T> ForComparable<T, TValue>(Func<T, TValue> getValue) where TValue : IComparable<TValue> =>
            new QuickSort<T>((x, y) => getValue(x).CompareTo(getValue(y)));
    }
}
