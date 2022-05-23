using LookScoreAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LookScoreAdmin.Converter
{
    public class ClubJoinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            Club[] clubs = (Club[])value;
            return clubs.Select(c => c.Name + "(" + c.Name.Substring(0, 3).ToUpper() + ")").ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
