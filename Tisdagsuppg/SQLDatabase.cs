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
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.Item1, item.Item2);
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

        /// <summary>
        /// Creates a database with the provided string as Database name
        /// </summary>
        /// <param name="databaseName">Provide the name for the database name</param>
        /// <returns>Returns true if database was created successfully, or else returns false </returns>
        public bool CreateDatabase(string databaseName)
        {
            try
            {
                var sql = $"CREATE DATABASE {databaseName}";
                ExecuteSQL(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public void CreateTable(string table, string fields)
        {
            var sql = $"CREATE TABLE {table} ({fields})";
            ExecuteSQL(sql);
        }

        public void AlterTable(string table, string field)
        {
            var sql = $"ALTER TABLE {table} {field}";
            ExecuteSQL(sql);


        }


        public void RenameColumn(string table, string old_column, string new_column)
        {
            var sql = $"EXEC sp_rename '{table}.{old_column}', '{new_column}'";
            ExecuteSQL(sql);
        }

        public void DropDatabase(string databaseName)
        {
            DatabaseName = "master";
            ExecuteSQL($"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            var sql = $"DROP DATABASE [{databaseName}]";
            ExecuteSQL(sql);
        }
    }
}