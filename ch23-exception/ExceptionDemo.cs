using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch23_exception
{
    class ExceptionDemo
    {
        public void div()
        {
            try
            {
                int x = 10, y = 0;
                x /= y;
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine("消息：{0}", e.Message);
                Console.WriteLine("源：{0}", e.Source);
                Console.WriteLine("堆栈：{0}",e.StackTrace);
                
                //Console.WriteLine("处理异常 —— 保持程序运行");
            }
        }

        public void ExceptionChain()
        {
            try
            {
                A();
            }catch(DivideByZeroException)
            {
                Console.WriteLine("catch clause in ExceptionChain()");
            }
            finally
            {
                Console.WriteLine("finally clause in ExceptionChain()");
                Console.WriteLine("After try statement in ExceptionChain.");
                Console.WriteLine("             -- Keep running.");
            }
        }

        private void A()
        {
            try
            {
                B();
            }catch(NullReferenceException)
            {
                Console.WriteLine("catch clause in A()");
            }
            finally
            {
                Console.WriteLine("finally clause in A()");
            }
        }

        private void B()
        {
            int x = 10, y = 0;
            try
            {
                x /= y;
            }catch(IndexOutOfRangeException)
            {
                Console.WriteLine("catch clause in B()");
            }
            finally
            {
                Console.WriteLine("finally clause in B()");
            }
        }
    }
}
