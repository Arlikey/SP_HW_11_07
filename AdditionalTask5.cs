using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_11_07
{
    internal class AdditionalTask5
    {
        static Random random = new Random();

        static void Main()
        {
            const int arraySize = 50000000;


            var array1 = CreateArray(arraySize);
            var array2 = CreateArray(arraySize);
            var array3 = CreateArray(arraySize);

            Console.WriteLine("Sort without using tasks:");

            var stopwatch = Stopwatch.StartNew();
            SortArray(array1);
            SortArray(array2);
            SortArray(array3);
            stopwatch.Stop();
            Console.WriteLine($"Execute time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("\nSort using tasks:");

            stopwatch.Restart();

            var sortTask1 = Task.Run(() => SortArray(array1));
            var sortTask2 = Task.Run(() => SortArray(array2));
            var sortTask3 = Task.Run(() => SortArray(array3));

            Task.WaitAll(sortTask1, sortTask2, sortTask3);
            stopwatch.Stop();
            Console.WriteLine($"Execute time: {stopwatch.ElapsedMilliseconds} ms");
        }

        static int[] CreateArray(int size)
        {
            var array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(100_000);
            }
            return array;
        }

        static void SortArray(int[] array)
        {
            Array.Sort(array);
        }


    }
}
