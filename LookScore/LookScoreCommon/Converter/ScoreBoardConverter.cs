using LookScoreCommon.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LookScoreCommon.Converter
{
    public class ScoreBoardConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            if (!(value is GameStatistics statistics))
            {
                return "0 : 0";
            }

            return statistics.HomeClub.Goal + " : " + statistics.GuestClub.Goal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
