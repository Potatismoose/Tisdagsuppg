namespace Tisdagsuppg
{
    internal class Uppgift7
    {
        public static void Run()
        {
            var db = new SQLDatabase();
            var sql = "CREATE DATABASE FamilyTree";
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

        public static void RunCreateTable()
        {

            var db = new SQLDatabase("Animals");
            var sql = "Id int PRIMARY KEY IDENTITY (1,1), Name nvarchar(50), Barksound nvarchar(50)";
            db.CreateTable("Dogs", sql);
        }

        public static void ChangeColumnName()
        {

            var db = new SQLDatabase("Animals");
            var sql = "Id int PRIMARY KEY IDENTITY (1,1), Name nvarchar(50), Barksound nvarchar(50)";
            db.RenameColumn("Dogs", "Barksound", "Voffsound");
        }

        public static void DropDatabase()
        {

            var db = new SQLDatabase();
            db.DropDatabase("Animals");
        }


    }
}