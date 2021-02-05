using System;
using System.Collections.Generic;
using System.Data;

namespace Tisdagsuppg
{
    internal class Uppgift6
    {
        public static void Run()
        {
            var db = new SQLDatabase();

            var list = new List<string>();
            var sql = "SELECT name FROM sys.databases";

            var dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["name"].ToString());
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}