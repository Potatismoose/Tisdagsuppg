using System;
using System.Data;

namespace Tisdagsuppg
{
    internal class Uppgift3
    {
        public static void Run()
        {
            Console.Write("Ange databasens namn: ");
            var dataBase = new SQLDatabase(Console.ReadLine());
            var sql = @"SELECT * FROM People WHERE Age > @age1 AND Age < @age2";
            Console.WriteLine("Ange åldrar:");
            var age1 = Console.ReadLine();
            var age2 = Console.ReadLine();
            var dataTable = dataBase.GetDataTable(sql, ("@age1", age1), ("@age2", age2));
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.Write($"{row[column]}, ");
                }
                Console.WriteLine();
            }
        }
    }
}