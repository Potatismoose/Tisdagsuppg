using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Tisdagsuppg
{
    internal class SQLDatabase
    {
        public string ConnectionString { get; set; } = @"Data Source = .\SQLExpress; Integrated Security = true; database = {0}";
        public string DatabaseName { get; set; }

        public SQLDatabase(string databaseName = "master")
        {
            DatabaseName = databaseName;
        }

        public int ExecuteSQL(string sql, params (string, string)[] parameters)
        {
            var connectionString = string.Format(ConnectionString, DatabaseName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(string sql, params (string, string)[] parameters)
        {
            var dataTable = new DataTable();
            var connectionString = string.Format(ConnectionString, DatabaseName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public void GetFilePath()
        {
            var db = new SQLDatabase();
            var sql = "SELECT physical_name, size FROM sys.database_files";
            var dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["physical_name"]}, ");
            }
        }

        public List<string> GetDatabases()
        {
            var list = new List<string>();
            var dt = GetDataTable("SELECT name FROM sys.databases");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["name"].ToString());
            }

            return list;
        }

        public void CreateDatabase(string databaseName)
        {

            var sql = $"CREATE DATABASE {databaseName}";
            ExecuteSQL(sql);
        }
    }
}