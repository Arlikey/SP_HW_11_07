using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_11_07
{
    internal class AdditionalTask1
    {
        static Random random = new Random();
        static void Main()
        {
            int arraySize = 20;
            int[] array = new int[arraySize];

            FillArray(array);

            Console.WriteLine("Initial array: ");
            PrintArray(array);

            Task<int[]> removeDuplicatesTask = Task.Run(() => RemoveDuplicates(array));

            Task<int[]> sortTask = removeDuplicatesTask.ContinueWith(previousTask =>
            {
                int[] arrayWithoutDuplicates = previousTask.Result;
                return SortArray(arrayWithoutDuplicates);
            });

            int searchValue = random.Next(10);
            Task<int> binarySearchTask = sortTask.ContinueWith(previousTask =>
            {
                int[] sortedArray = previousTask.Result;
                return BinarySearch(sortedArray, searchValue);
            });

            binarySearchTask.ContinueWith(previousTask =>
            {
                int searchResult = previousTask.Result;
                int[] sortedArray = sortTask.Result;

                Console.WriteLine("\nSorted array:");
                PrintArray(sortedArray);

                if (searchResult >= 0)
                {
                    Console.WriteLine($"\nValue {searchValue} was found in array on index: {searchResult}");
                }
                else
                {
                    Console.WriteLine($"\nValue {searchValue} wasn't found.");
                }
            });

            Console.ReadLine();
        }

        static int[] RemoveDuplicates(int[] array)
        {
            Console.WriteLine("\nRemoving duplicates...");
            Thread.Sleep(500);
            return array.Distinct().ToArray();
        }

        static int[] SortArray(int[] array)
        {
            Console.WriteLine("\nSorting array...");
            Thread.Sleep(500);
            Array.Sort(array);
            return array;
        }

        static int BinarySearch(int[] array, int value)
        {
            Console.WriteLine($"\nSearching for {value} in array...");
            Thread.Sleep(500);
            return Array.BinarySearch(array, value);
        }

        static void PrintArray(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }

        static void FillArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(10);
            }
        }
    }
}
