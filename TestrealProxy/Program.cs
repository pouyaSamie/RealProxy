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
            var x= Factory.GetInstance<TestService>();
            x.Add(10);
            x.Delete();
            Console.ReadKey();
        }
    }
}
