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
        
        public enum Operation
        {
            ADD, SUBTRACT, MULTIPLY, DIVIDE, PERCENTAGE, UNKNOWN
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


    }
}
