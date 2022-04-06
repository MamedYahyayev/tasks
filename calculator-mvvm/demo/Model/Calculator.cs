using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Model
{
    public class Calculator
    {
        public double Number { get; set; }
        public CalcOperation Operation { get; set; } = CalcOperation.UNSET;

        private double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        private double Subtract(double n1, double n2)
        {
            return n1 - n2;
        }

        private double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        private double Divide(double n1, double n2)
        {
            if (n2 == 0) throw new DivideByZeroException("You cannot divide by zero");

            return n1 / n2;
        }

        private double Percentage(double n1, double n2)
        {
            return n1 / 100 * n2;
        }

        public double CalculateResult(double n1, double n2, CalcOperation operation)
        {
            switch (operation)
            {
                case CalcOperation.ADD:
                    return Add(n1, n2);

                case CalcOperation.SUBTRACT:
                    return Subtract(n1, n2);

                case CalcOperation.MULTIPLY:
                    return Multiply(n1, n2);

                case CalcOperation.DIVIDE:
                    return Divide(n1, n2);

                case CalcOperation.PERCENTAGE:
                    return Percentage(n1, n2);

                default: return 0;

            }
        }

    }
}
