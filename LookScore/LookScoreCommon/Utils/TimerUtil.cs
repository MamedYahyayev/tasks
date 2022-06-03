using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreCommon.Utils
{
    public class TimerUtil
    {
        private TimerUtil()
        {

        }

        private static string ConvertToTimePart(int seconds)
        {
            if(seconds.ToString().Length == 1)
            {
                return "0" + seconds.ToString();
            }

            return seconds.ToString();
        }

        public static string Convert(int seconds)
        {
            int minute = seconds / 60;
            int remainderSeconds = seconds - (minute * 60);

            string minuteStr = ConvertToTimePart(minute);
            string secondStr = ConvertToTimePart(remainderSeconds);

            return minuteStr + " : " + secondStr;
        }

    }
}
