using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestrealProxy
{
    public class TestService:MarshalByRefObject
    {
        public void Add(int age)
        {
            Console.WriteLine("Adding");
        }

        public void Delete()
        {
            Console.WriteLine("Deleting");
        }
    }
}
