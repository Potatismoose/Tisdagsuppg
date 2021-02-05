namespace Tisdagsuppg
{
    internal class Uppgift7
    {
        public static void Run()
        {
            var db = new SQLDatabase();
            var sql = "CREATE DATABASE Humans";
            db.ExecuteSQL(sql);
        }

        public static void Run2()
        {
            var db = new SQLDatabase("Humans");
            var sql = @"CREATE TABLE People (
                        ID int PRIMARY KEY IDENTITY (1,1),
                        firstName nvarchar(50),
                        lastName nvarchar(50),
                        address nvarchar(50),
                        city nvarchar(50),
                        shoeSize int
                        )";
            db.ExecuteSQL(sql);
        }

        public static void Run3()
        {
            var sql = "ALTER TABLE people ADD age int";
            var db = new SQLDatabase("Humans");
            db.ExecuteSQL(sql);
        }

        public static void Run4()
        {
            var db = new SQLDatabase("Humans");
            var sql = "ALTER TABLE People DROP COLUMN ShoeSize";
            db.ExecuteSQL(sql);
        }
    }
}