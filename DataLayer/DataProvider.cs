using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DataProvider
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True";

        public DataTable ExecuteQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public void ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public object MyExecuteScalar(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}