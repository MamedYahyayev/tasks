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
            var isNull = reader.IsDBNull(reader.GetOrdinal(columnName));
            if (isNull) return String.Empty;

            return reader.GetString(columnName);
        }
    }
}
