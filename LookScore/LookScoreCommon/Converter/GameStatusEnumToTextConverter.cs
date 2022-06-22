using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using LookScoreCommon.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LookScoreCommon.Converter
{
    public class GameStatusEnumToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Game game)) return null;

            DateTime now = DateTime.Now;

            if (game.GameStatus == GameStatus.UNSTARTED)
            {
                return game.GameStartDate;
            }

            if (game.GameStatus == GameStatus.STARTED)
            {
                return DateUtil.DifferenceInMinutes(game.GameStartDate, now);
            }

            if (game.GameStatus == GameStatus.STOPPED)
            {
                return "STOPPED";
            }

            if (game.GameStatus == GameStatus.HALF_TIME)
            {
                return "HF";
            }

            if (game.GameStatus == GameStatus.FULL_TIME)
            {
                return "FT";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
