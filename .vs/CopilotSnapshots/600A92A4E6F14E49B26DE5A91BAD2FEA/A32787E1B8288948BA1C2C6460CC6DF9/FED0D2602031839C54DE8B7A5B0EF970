
using Microsoft.Data.SqlClient;

namespace ReportMLCI.Helper
{
    public static class HandleNull
    {
        public static string nlString(SqlDataReader reader, string fieldName)
        {
            return reader[fieldName] != DBNull.Value
                ? reader[fieldName].ToString()
                : "";
        }

        public static bool nlBool(SqlDataReader reader, string fieldName)
        {
            return reader[fieldName] != DBNull.Value
                ? Convert.ToBoolean(reader[fieldName])
                : false;
        }
    }
}

