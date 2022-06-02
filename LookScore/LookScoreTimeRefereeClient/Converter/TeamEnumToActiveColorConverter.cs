using LookScoreCommon.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LookScoreTimeRefereeClient.Converter
{
    public class TeamEnumToActiveColorConverter : IValueConverter
    {
        private const string ACTIVE_COLOR = "#F1EEE9";
        private const string NON_ACTIVE_COLOR = "#73777B";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Team))
                return false;


            Team ballOwnerTeam = (Team)value;
            Team team = (Team)parameter;

            if (ballOwnerTeam == team)
            {
                return ACTIVE_COLOR;
            }

            return NON_ACTIVE_COLOR;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
