using LookScoreCommon.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LookScoreCommon.Converter
{
    public class SecondsToMinuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int seconds = (int)value;

            return TimerUtil.Convert(seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
