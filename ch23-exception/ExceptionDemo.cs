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
    }
}
