namespace Tisdagsuppg
{
    internal class Uppgift4
    {
        public static void Run()
        {
            var db = new SQLDatabase("Test");
            db.GetFilePath();
        }
    }
}