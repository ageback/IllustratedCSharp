using IllustratedCSharp.ch14_delegates;
using IllustratedCSharp.ch18_generics;
using IllustratedCSharp.ch21_asynchronous;
using IllustratedCSharp.ch23_exception;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGenericMethod();
            //TestGenericClass();
            //DelegateDemo.TestMyDel();

            //Console.WriteLine(_testNetVersion("4.8").ToString());
            //Console.WriteLine(_newTestNetVersion("4.5").ToString());

            //TestException();

            TestMyDownloadString();
        }

        static void TestGenericClass()
        {
            MyStack<int> StackInt = new MyStack<int>();
            MyStack<string> StackString = new MyStack<string>();

            StackInt.Push(3);
            StackInt.Push(5);
            StackInt.Push(7);
            StackInt.Push(9);
            StackInt.Print();

            StackString.Push("This is fun");
            StackString.Push("Hi there!  ");
            StackString.Print();
        }

        static void TestGenericMethod()
        {
            var intArray = new int[] { 3, 5, 7, 9, 11 };
            var stringArray = new string[] { "first", "second", "third" };
            var doubleArray = new double[] { 3.567, 8.891, 2.345 };

            Simple.ReverseAndPrint<int>(intArray);
            Simple.ReverseAndPrint(intArray);

            Simple.ReverseAndPrint<string>(stringArray);
            Simple.ReverseAndPrint(stringArray);

            Simple.ReverseAndPrint<double>(doubleArray);
            Simple.ReverseAndPrint(doubleArray);
        }

        private static bool _newTestNetVersion(string version)
        {
            try
            {
                var opt = new Dictionary<string, string>();
                // 能不能不设置 CompilerVersion ？ 让编译器自动检测。
                opt.Add("CompilerVersion", "v" + version);

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp", opt);
                CompilerParameters cp = new CompilerParameters();
                cp.IncludeDebugInformation = true;
                cp.GenerateExecutable = false;     // 不生成可执行文件   
                cp.GenerateInMemory = true;
                cp.ReferencedAssemblies.Add("System.dll");
                CompilerResults cr = provider.CompileAssemblyFromSource(cp, "using System;namespace WCPTest{public class Test{public void a(){var x = 12345;}}}");
                return !cr.Errors.HasErrors;
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(".Net " + version + " 版本检测：" + ex.Message);
                Console.WriteLine(".Net " + version + " 版本检测：" + ex.StackTrace);
                return false;
            }
        }

        private static bool _testNetVersion(string version)
        {
            try
            {
                var opt = new Dictionary<string, string>();
                // 能不能不设置 CompilerVersion ？ 让编译器自动检测。
                opt.Add("CompilerVersion", "v" + version);

                ICodeCompiler comp = new CSharpCodeProvider(opt).CreateCompiler();
                //编译器的传入参数   
                CompilerParameters cp = new CompilerParameters();
                cp.IncludeDebugInformation = true;
                cp.GenerateExecutable = false;     // 不生成可执行文件   
                cp.GenerateInMemory = true;
                cp.ReferencedAssemblies.Add("System.dll");

                CompilerResults cr = comp.CompileAssemblyFromSource(cp, "using System;namespace WCPTest{public class Test{public void a(){var x = 12345;}}}");
                return !cr.Errors.HasErrors;
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(".Net " + version + " 版本检测：" + ex.StackTrace);
                return false;
            }
        }

        private static void TestException()
        {
            ExceptionDemo demo = new ExceptionDemo();
            //demo.div();
            demo.ExceptionChain();
        }

        private static void TestMyDownloadString()
        {
            MyDownloadString ds = new MyDownloadString();
            ds.DoRun();
        }

    }
}
