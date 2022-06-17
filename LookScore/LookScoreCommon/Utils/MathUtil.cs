using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreCommon.Utils
{
    public class MathUtil
    {
        private MathUtil()
        {

        }

        public static double Percentage(double value, double total)
        {
            return Math.Round((value * 100) / total);
        }
    }
}
