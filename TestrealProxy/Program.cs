using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestrealProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            // you should Always use Factory
            var service= Factory.GetInstance<TestService>();

            service.Add(10);
            service.Delete();
            Console.ReadKey();
        }
    }
}
