using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Add(1, 2, 3, 4, 5, 6, 7, 8);
        }

        public static void Add(params int[] nums)
        {
            foreach (int num in nums)
            {
                Console.WriteLine(num);
            }
        }
    }
}
