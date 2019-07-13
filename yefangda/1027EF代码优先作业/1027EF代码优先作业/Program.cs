using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1027EF代码优先作业
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CourseDBEntities();
            Console.WriteLine("++++++++++++查询+++++++++++++");
            //查询
            var courses = context.Courses.ToList();
            foreach (var c in courses)
            {
                Console.WriteLine("{0},{1},{2}", c.Title, c.Credit, c.Dapartments.Name);
            }

            Console.WriteLine("++++++++++++增加一门课程+++++++++++++");
            //新增
            var newC = new Courses
            {
                ID = Guid.NewGuid(),
                Title = "算法与信息",
                Credit = 2,
                Dapartments = context.Dapartments.SingleOrDefault(x => x.Name == "电子学院")

            };
            //var newC2 = new Courses
            //{
            //    ID = Guid.NewGuid(),
            //    Title = "商务英语",
            //    Credit = 2,
            //    Dapartments = context.Dapartments.SingleOrDefault(x => x.Name == "电子学院")

            //};
            //context.Courses.Add(newC2);
            //context.SaveChanges();
            if (courses.SingleOrDefault(x=>x.Title== "算法与信息") == null)
            {
                context.Courses.Add(newC);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("添加错误");
            }

            Console.WriteLine("++++++++++++修改一门课程+++++++++++++");
            //修改
            var obj = context.Courses.SingleOrDefault(x => x.Title == "商务英语");
            if(obj==null)
            {
                Console.WriteLine("未找到课程");
            }
            else
            {
                obj.Title = "计算机英语";
                obj.Credit = 4;
                obj.Dapartments = context.Dapartments.SingleOrDefault(x => x.Name == "电子学院");
                context.SaveChanges();
            }

            Console.WriteLine("++++++++++++删除一门课程++++++++++++");
            //删除
            //var delobj = context.Courses.Find(Guid.Parse("05adb18d-92ae-4b2f-92b8-384ea937d6b7"));
            //context.Courses.Remove(delobj);
            //context.SaveChanges();

            Console.WriteLine("++++++++++++修改后内容+++++++++++++");
            courses = context.Courses.ToList();
            foreach(var c in courses)
            {
                Console.WriteLine("{0},{1},{2}", c.Title, c.Credit, c.Dapartments.Name);
            }
            Console.ReadKey();
        }
    }
}
