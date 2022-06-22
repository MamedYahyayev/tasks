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
            AddWithParamsKeyword(1, 2, 3, 4, 5, 6, 7, 8);

            AsOperator("saa");

            IsOperator(3);

            IsOperatorWithDeclaredVariable(new Person() { Name = "Samir", Surname = "Samirov" });

            Console.ReadLine();
        }

        /// <summary>
        /// params keyword allow us to enter a large amount of
        /// item and seperate with comma
        /// </summary>
        /// <param name="nums"></param>
        public static void AddWithParamsKeyword(params int[] nums)
        {
            foreach (int num in nums)
            {
                Console.WriteLine(num);
            }
        }

        /// <summary>
        /// it will cast value to suitable type
        /// if value is different from null, then cast happened
        /// otherwise it will be null
        /// </summary>
        /// <param name="value"></param>
        public static void AsOperator(object value)
        {
            string text = value as string;

            if (text != null)
            {
                Console.WriteLine(text + " casted");
            }
            else
            {
                Console.WriteLine("cast doesn't happened");
            }
        }

        public static void AsOperatorWithPrimitives(object value)
        {
            // int num = value as int; // compile time error
            // as operator can't be used for primitive types 
        }

        /// <summary>
        /// if value is suitable type it will return true,
        /// otherwise false
        /// </summary>
        /// <param name="value"></param>
        public static void IsOperator(object value)
        {
            Console.WriteLine(value is bool);
            Console.WriteLine(value is string);
            Console.WriteLine(value is int);
            Console.WriteLine(value is double);
        }

        public static void IsOperatorWithDeclaredVariable(object value)
        {
            // it will check the value is a Person or not, and it can create a object of Person
            if (value is Person person)
            {
                Console.WriteLine(person.Name + " " + person.Surname);
            }

            // Console.WriteLine(person.ToString()); // compile error, person isn't declared outside of if block
        }
    }


    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
