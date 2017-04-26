using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace TestrealProxy
{
    public class DynamicProxy<T> : RealProxy where T : MarshalByRefObject
    {
        private readonly T decorated;

        public DynamicProxy(T decorated)
        : base(typeof(T))
        {
            this.decorated = decorated;
        }
        
        public event EventHandler<IMethodCallMessage> AfterExecute;

        public event EventHandler<IMethodCallMessage> BeforeExecute;

        public event EventHandler<IMethodCallMessage> ErrorExecute;

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            OnBeforeExecute(methodCall);
            try
            {
                var result = methodInfo.Invoke(decorated, methodCall.InArgs);
                OnAfterExecute(methodCall);
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                OnErrorExecute(methodCall);
                return new ReturnMessage(ex, methodCall);
            }
        }

        private void OnAfterExecute(IMethodCallMessage methodCall)
        {
            Console.WriteLine($"End of Execution {DateTime.Now.ToString("hh.mm.ss.fff")}");
            if (AfterExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                AfterExecute(this, methodCall);
            }
        }

        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            Console.WriteLine($"Start of Execution {DateTime.Now.ToString("hh.mm.ss.fff")}");
            if (BeforeExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;

                BeforeExecute(this, methodCall);
            }
        }

        private void OnErrorExecute(IMethodCallMessage methodCall)
        {
            Console.WriteLine("OnErrorExecute");
            if (ErrorExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                ErrorExecute(this, methodCall);
            }
        }

    }
}
