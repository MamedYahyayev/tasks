using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using LookScoreCommon.Model;
using LookScoreCommon.Enums;

namespace LookScoreCommon.Converter
{
    public class SecondsToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GameStatistics gameStatistics = (GameStatistics)value;

            if (gameStatistics == null) return null;

            double result = CalculatePercentage(gameStatistics);

            Team team = (Team)parameter;
            if (team == Team.GUEST)
            {
                return (100 - result) + "%";
            }

            return result + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private double CalculatePercentage(GameStatistics gameStatistics)
        {
            double homeTeamPossessionTime = gameStatistics.HomeClub.BallPossessionTime;
            double guestTeamPossessionTime = gameStatistics.GuestClub.BallPossessionTime;

            return Math.Round((homeTeamPossessionTime / (homeTeamPossessionTime + guestTeamPossessionTime)) * 100);
        }
    }
}
