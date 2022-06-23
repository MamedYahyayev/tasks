using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    AddWithParamsKeyword(1, 2, 3, 4, 5, 6, 7, 8);

        //    AsOperator("saa");

        //    IsOperator(3);

        //    IsOperatorWithDeclaredVariable(new Person() { Name = "Samir", Surname = "Samirov" });

        //    Console.WriteLine("********Hash********");
        //    Console.WriteLine(ComputeHash("salam"));

        //    Console.WriteLine("*****Comparison*****");

        //    CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

        //    Console.WriteLine(CompareStrings("SıL", "SIL"));

        //    Console.WriteLine("\ndynamic and var keyword");

        //    DynamicAndVarKeywords();

        //    Console.WriteLine("\ndynamic object");

        //    dynamic obj = new Dynamic();
        //    obj.LastName = "Samirov";
        //    Console.WriteLine(obj.LastName);

        //    obj["FirstName"] = "Samir";
        //    Console.WriteLine(obj["FirstName"]);

        //    Console.ReadLine();
        //}

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

        public static string ComputeHash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            string result = BitConverter.ToString(bytes).Replace("-", string.Empty);
            // result will be DE-68-38-25-2F-95-D3-B9-E8-03-B2-8D-F3-3B-4B-AA, it is not good we can replace - sign with empty string
            // we can hash password with this method
            return result;
        }

        public static bool CompareStrings(string first, string second)
        {
            return first.Equals(second, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Dynamic keyword will do boxing and unboxing operations behind the scenes
        /// and we don't need to explicitly box or unbox,
        /// it is the same as
        /// object obj; 
        /// but the difference is if we use object we have to convert values
        /// </summary>
        public static void DynamicAndVarKeywords()
        {
            dynamic obj = 3.4;

            obj = 2;

            object realObj = 1;

            double doubleWithDynamic = obj; // it will do cast behind the scenes
            // double doubleWithObj = realObj; // compile error, needs to cast


            var num = 1;

            num = (int)1.5;
        }
    }


    class Person
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
    }

    class Student : Person
    {
        public Student()
        {
            this.Name = "saassa";
            this.Surname = "sasda";
        }
    }

    class Dynamic : DynamicObject
    {
        Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public object this[string propertyName]
        {
            get { return _dictionary[propertyName]; }
            set { AddProperty(propertyName, value); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            AddProperty(binder.Name, value);
            return true;
        }

        public void AddProperty(string propertyName, object value)
        {
            _dictionary.Add(propertyName, value);
        }
    }
}
