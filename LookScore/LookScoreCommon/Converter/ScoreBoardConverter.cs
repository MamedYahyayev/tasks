using LookScoreCommon.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LookScoreCommon.Converter
{
    public class ScoreBoardConverter : IValueConverter
    {
        private const string TIE = "0 : 0";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            if (!(value is GameStatistics statistics))
            {
                return TIE;
            }

            return statistics.HomeClub.Goal + " : " + statistics.GuestClub.Goal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
