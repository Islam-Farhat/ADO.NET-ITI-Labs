using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab2
{
    static class DBLayer
    {
        static SqlConnection connection;

        static DBLayer()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["itiEntites"].ConnectionString);
        }

        public static DataTable select(string tableName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"select * from {tableName}", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
        public static int DMLCommands(string command)
        {
            SqlCommand Sc = new SqlCommand(command, connection);
            connection.Open();
            int result = Sc.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
