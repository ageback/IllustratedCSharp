using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch20_LINQ
{
    class LinqJoinDemo
    {
        static Student[] students = new Student[]
        {
            new Student{StID=1,LastName="张三"},
            new Student{StID=2,LastName="李四"},
            new Student{StID=3,LastName="王五"}
        };

        static CourseStudent[] studentInCourses = new CourseStudent[]
        {
            new CourseStudent{CourseName="美术", StID=1},
            new CourseStudent{CourseName="美术", StID=2},
            new CourseStudent{CourseName="历史", StID=1},
            new CourseStudent{CourseName="历史", StID=3},
            new CourseStudent{CourseName="物理", StID=3},
        };

        public static void Test()
        {
            var query = from s in students
                        join c in studentInCourses
                        on s.StID equals c.StID
                        where c.CourseName == "历史"
                        select s.LastName;

            foreach(var q in query)
            {
                Console.WriteLine($"选择历史课的学生：{q}");
            }
            Console.WriteLine($"选择历史课的学生人数：{query.Count()}\n");
            Console.WriteLine("各课的学生分组：");

            var query2 = from s in students
                         join c in studentInCourses
                         on s.StID equals c.StID
                         //where c.CourseName == "历史"
                         group s by c.CourseName;
            foreach(var g in query2)
            {
                Console.WriteLine($"{g.Key}：");
                foreach(var s in g)
                {
                    Console.WriteLine($"|---- {s.LastName}");
                }
            }


            // 查询延续：into 子句
            var groupA = new[] { 3, 4, 5, 6 };
            var groupB = new[] { 4, 5, 6, 7 };

            var someInts = from a in groupA
                           join b in groupB
                           on a equals b
                           into groupAandB
                           from c in groupAandB
                           select c;
            foreach(var v in someInts)
            {
                Console.Write($"{v} ");
            }
        }
    }

    public class Student
    {
        public int StID;
        public string LastName;
    }

    public class CourseStudent
    {
        public string CourseName;
        public int StID;
    }
}
