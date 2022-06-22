using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreCommon.Utils
{
    public class DateUtil
    {
        private DateUtil()
        {

        }

        public static double DifferenceInMinutes(DateTime start, DateTime end) 
        {
            return Math.Floor((end - start).TotalMinutes);
        }

        public static double DifferenceInSeconds(DateTime start, DateTime end)
        {
            return (end - start).TotalSeconds;
        }
    }
}
