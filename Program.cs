using IllustratedCSharp.ch14_delegates;
using IllustratedCSharp.ch18_generics;
using IllustratedCSharp.ch21_asynchronous;
using IllustratedCSharp.ch23_exception;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Odbc;
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
            //Test泛型扩展方法();
            //DelegateDemo.TestMyDel();

            //Console.WriteLine(_testNetVersion("4.8").ToString());
            //Console.WriteLine(_newTestNetVersion("4.5").ToString());

            //TestException();
            //TestMyDownloadStringSingleThread();
            TestMyDownloadStringAsync();
            //TaskCancel.TaskCancelTest();

            //DoAsyncStuff.DoAsyncStuffTest();

            //TestFileName();

            //ConnectODBCSqlserver();

            //ConnectDMDB();
        }

        static void Test泛型扩展方法()
        {
            var intHolder = new Holder<int>(3, 5, 7);
            var stringHolder = new Holder<string>("a1", "b2", "c3");
            intHolder.Print();
            stringHolder.Print();
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

        private static void TestMyDownloadStringAsync()
        {
            MyDownloadString ds = new MyDownloadString();
            ds.DoRun();
        }

        private static void TestMyDownloadStringSingleThread()
        {
            MyDownloadStringSingleThread ds = new MyDownloadStringSingleThread();
            ds.DoRun();
        }

        private static void TestFileName()
        {
            string fileName = "锡滨政发〔2020〕2号(滨湖区2020年招商工作意见).doc";
            string fileType = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower().Trim();
            Console.WriteLine(fileType);
        }

        private static void ConnectODBCSqlserver()
        {
            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=192.168.0.36;Database=Wesoft_OA;Uid=sa;Pwd=sa;DBType=SQL";
            //定义SqlConnection对象实例
            OdbcConnection odbcCon = new OdbcConnection(connectionString);
            OdbcDataAdapter adapter = new OdbcDataAdapter();
            //string strUpdate = "Update TestOdbc Set TestOdbc=?";
            string strUpdate = "Update FrameModule Set MODULENAME=?,VERSION=?,HASFUNCTION=?,SERIALNO=?,STATE=?,UPDATETIME=? Where ID=?";
            OdbcCommand cmd = new OdbcCommand(strUpdate, odbcCon);
           
            cmd.Parameters.Add("@ModuleName", OdbcType.NVarChar, 50).Value = "系统框架";
            cmd.Parameters.Add("@Version", OdbcType.NVarChar, 50).Value = "Wesoft";
            cmd.Parameters.Add("@HasFunction", OdbcType.Int).Value = 1;
            cmd.Parameters.Add("@SerialNo", OdbcType.Int).Value = 9999;
            cmd.Parameters.Add("@State", OdbcType.Int).Value = 1;

            // 不指定 param.Scale的话，秒精度会超出数据库字段长度。
            OdbcParameter param = new OdbcParameter("UpdateTime", OdbcType.DateTime);
            param.Value = DateTime.Now;
            param.Precision = 23;
            param.Scale = 3;
            cmd.Parameters.Add(param);
            //cmd.Parameters.Add("@UpdateTime", OdbcType.DateTime, 5).Value = DateTime.Now;
            cmd.Parameters.Add("@KeyFieldValue", OdbcType.Int).Value = 1;
            
            
            //cmd.Parameters.Add("@TestOdbc", OdbcType.DateTime,7).Value = DateTime.Now;
            
            try
            {
                odbcCon.Open();
                cmd.ExecuteNonQuery();
            } catch (Exception err)
            {
                Console.WriteLine(err.StackTrace);
                throw;
            }
            finally
            {
                odbcCon.Close();
                odbcCon.Dispose();
            }
            
        }

        private static OdbcConnection ConnectDMDB()
        {
            string ConnString = "DSN=dmdb2;Driver={DM8 ODBC DRIVER};Server=192.168.0.211:5236;Uid=WESOFTOA;PWD=P@ssw0rd1";
            OdbcConnection conn = new OdbcConnection(ConnString);
            try
            {
                conn.Open();
                Console.WriteLine("连接成功");
                return conn;
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
                Console.WriteLine(err.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return null;
        }

    }
}
