using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Computing : Operable
    {
        private double number1;
        private double number2;
        private string activeOperation;

        public static class Operation
        {
            public const string ADD = "ADD";
            public const string SUBTRACT = "SUBTRACT";
            public const string MULTIPLY = "MULTIPLY";
            public const string DIVIDE = "DIVIDE";
            public const string PERCENTAGE = "PERCENTAGE";
        }

        public double add(double number1, double number2)
        {
            return number1 + number2;
        }

        public double divide(double number1, double number2)
        {
            if (number2 == 0)
            {
                throw new DivideByZeroException();
            }

            return number1 / number2;
        }

        public double multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        public double subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        public double percentage(double number1, double number2)
        {
            return number1 / 100 * number2;
        }

        public double Number1
        {
            get { return number1; }
            set { number1 = value; }
        }

        public double Number2
        {
            get { return number2; }
            set { number2 = value; }
        }

        public string ActiveOperation
        {
            get { return activeOperation; }
            set { activeOperation = value; }
        }


    }
}
