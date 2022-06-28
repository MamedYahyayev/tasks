using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class ColumnParser
    {
        private readonly string[] _columns;

        public ColumnParser(string[] columns)
        {
            if (columns == null) throw new ArgumentNullException(nameof(columns));

            _columns = columns;
        }

        private void Parse(Enum header, out string value)
        {
            value = null;

            if (_columns == null || _columns.Length == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_columns[Convert.ToInt32(header)]))
            {
                value = _columns[Convert.ToInt32(header)];
            }
        }

        public void ParseToString(Enum header, out string value)
        {
            Parse(header, out value);
        }

        public void ParseToInt(Enum header, out int value)
        {
            Parse(header, out string strValue);

            value = Convert.ToInt32(strValue);
        }

    }
}
