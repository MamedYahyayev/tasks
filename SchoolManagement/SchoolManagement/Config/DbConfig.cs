using SchoolManagement.Constant;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Config
{
    public class DbConfig
    {
        public static SqlConnection Connect() => new SqlConnection(SqlServerConnection());

        private static string SqlServerConnection()
        {
            string connection = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                SqlServerDbConfig.DATA_SOURCE, SqlServerDbConfig.DATABASE, SqlServerDbConfig.USER, SqlServerDbConfig.PASSWORD);

            return connection;
        }
    }
}
