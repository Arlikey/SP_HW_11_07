using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_11_07
{
    internal class AdditionalTask2
    {
        static Random random = new Random();
        static void Main()
        {
            int[] array = CreateArray(500_000);

            int taskCount = 4;
            int arrayPartSize = array.Length / taskCount;

            Task<long>[] tasks = new Task<long>[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                int start = i * arrayPartSize;
                int end = (i == taskCount - 1) ? array.Length : start + arrayPartSize;
                tasks[i] = Task.Run(() => SumArray(array, start, end));
            }

            Task.WaitAll(tasks);
            long totalSum = tasks.Sum(task => task.Result);

            Console.WriteLine($"Sum of array elements: {totalSum}");
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
        static long SumArray(int[] array, int start, int end)
        {
            long sum = 0;
            for (int i = start; i < end; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}
