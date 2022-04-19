using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
    public static class SqlReaderExtension
    {
        public static string GetStringValueOrDefault(this SqlDataReader reader, string columnName)
        {
            var isNull = IsNull(reader, columnName);
            if (isNull) return string.Empty;

            return reader.GetString(columnName);
        }

        public static object GetIntValueOrDefault(this SqlDataReader reader, string columnName)
        {
            var isNull = IsNull(reader, columnName);
            if (isNull) return null;

            return reader.GetInt32(columnName);
        }

        private static bool IsNull(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)); ;
        }
    }
}
