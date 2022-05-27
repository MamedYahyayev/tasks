using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LookScoreManageStatisticsClient.Converter
{
    public class ScoreBoardConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            GameStatistics statistics = value as GameStatistics;

            if (statistics == null)
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
