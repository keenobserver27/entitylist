using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityListLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetEntity entity = new GetEntity("C:\\configuration.json", "U");
            var list = entity.EntityList(entity.getData());
            if(list != null)
            { 
                foreach(var x in list)
                {
                    Console.WriteLine(x.Id.ToString()+ ", " + x.Created + ", " + x.Type + ", " + x.Content);
                }
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
