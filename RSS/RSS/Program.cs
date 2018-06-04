using System;
using RSS.Functionality;
using RSS.Database;

namespace RSS
{
    class Program
    {     
        static void Main(string[] args)
        {
            RssRepository RssRepository = new RssRepository();
            RssHelper helper = new RssHelper(RssRepository);
            helper.ReadXmlFile("Habr.xml");
            Console.ReadLine();
        }
    }
}
