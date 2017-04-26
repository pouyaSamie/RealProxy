using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestrealProxy
{
    public class Factory
    {
        public static T GetInstance<T>() where T : MarshalByRefObject
        {

            T targetService = Activator.CreateInstance<T>();
            var proxy = new DynamicProxy<T>(targetService);
            proxy.AfterExecute += Proxy_AfterExecute;
            proxy.BeforeExecute += Proxy_BeforeExecute;
            proxy.ErrorExecute += Proxy_ErrorExecute;
            return (T)(proxy.GetTransparentProxy());
        }

        private static void Proxy_ErrorExecute(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e)
        {
            Console.WriteLine("Log Error");
        }

        private static void Proxy_BeforeExecute(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e)
        {
            var args = GetArgs(e);
            Console.WriteLine("Log befor Execute {0}" , args);
        }

        private static void Proxy_AfterExecute(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e)
        {
            Console.WriteLine("Log after Execute {0}", string.Join(",", e.Args.ToList()));
          
        }

        private static string GetArgs(System.Runtime.Remoting.Messaging.IMethodCallMessage e)
        {
            var args = string.Empty;
            int counter = 0;
            foreach (var item in e.Args)
            {
                args +=$"[{e.GetArgName(counter++)}:{item.ToString()}]";
            }
            return args;

        }
    }

}
