using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SchoolManagement.Converter
{
    public class TeacherJoinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var teachers = (List<Teacher>)value;
            var teacherNames = teachers.Select(x => x.Name + " " + x.Surname).ToList();
            var concat = string.Join(", ", teacherNames);

            return concat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
