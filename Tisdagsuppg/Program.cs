using System;

namespace Tisdagsuppg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Uppgift4.Run();

            //Uppgift6.Run();
            //Uppgift7.Run();
            //Uppgift7.Run2();
            //Uppgift7.Run3();
            //Uppgift7.Run4();
            var db = new SQLDatabase();
            db.CreateDatabase("'OR DROP TABLE ANIMAL'");
            Console.ReadKey();
        }
    }
}