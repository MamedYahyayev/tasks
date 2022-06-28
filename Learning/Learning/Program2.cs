using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{

    public class Program2
    {
        enum Columns
        {
            NAME, SURNAME, AGE, EMAIL
        }

        enum Student
        {
            NAME,
            SURNAME,
            AGE,
            EMAIL
        }

        //public static void Main(string[] args)
        //{
        //    dynamic column = new DataColumn();
            
        //    var persons = new List<Person>();
        //    using (var stream = new FileStream(@"C:\Users\User\Desktop\tasks\Learning\Learning\persons.txt", FileMode.Open))
        //    {
        //        var reader = new StreamReader(stream);
        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            string[] values = line.Split('\t');

        //            for (var i = 0; i < 1; i++)
        //            {
        //                var person = new Person
        //                {
        //                    Name = values[column[Student.NAME]],
        //                    Surname = values[column[Student.SURNAME]],
        //                    Age = Convert.ToInt32(values[column[Student.AGE]]),
        //                    Email = values[column[Student.EMAIL]]
        //                };

        //                persons.Add(person);
        //            }
        //        }
        //    }

        //    foreach (var person in persons)
        //    {
        //        Console.WriteLine(person);
        //    }

        //    Console.ReadLine();
        //}


        class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }

            public override string ToString()
            {
                return Name + " " + Surname + " " + Age + " " + Email;
            }
        }
    }
}
