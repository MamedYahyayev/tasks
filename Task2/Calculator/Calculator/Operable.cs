using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal interface Operable
    {
        double add(double number1, double number2);
        double subtract(double number1, double number2);
        double divide(double number1, double number2);
        double multiply(double number1, double number2);
        double percentage(double number1, double number2);

    }
}
