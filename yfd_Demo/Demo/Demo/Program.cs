using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用数据上下文进行数据操作，using表示上下文代码的范围，执行完后内存会自动释放
            using (var context = new CourseContext())
            {
                //.where .orderby .tolist() 注意调用的顺序
                var departments = context.Dapartments.Where(n => n.Name.Contains("电子")).OrderBy(n => n.SortCode).ToList();

                foreach (var d in departments)
                    Console.WriteLine("编号{0},{1},{2}", d.SortCode, d.Name, d.Dscn);

                Console.WriteLine("添加一条新的部门记录");
                Console.WriteLine("=================================");

                //添加一条记录

                var newDepartment = new Dapartments
                {
                    ID = Guid.NewGuid(),
                    Name = "商务学院",
                    Dscn = "",
                    SortCode = "004"
                };
                var newDept = context.Dapartments.Where(n => n.Name.Contains(newDepartment.Name));
                if (newDept == null)
                {
                    //把新的对象添加到上下文中
                    context.Dapartments.Add(newDepartment);
                    //更新上下文，把新的实体保存到数据库
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("已存在，无法新建数据");
                }

                Console.WriteLine("删除记录");
                Console.WriteLine("========================================");
                //find--用主键查询实体
                var delDept = context.Dapartments.Find(Guid.Parse("36dd31e0-cfb0-432f-9c04-6eb71f34e961"));
                //var id = Guid.Parse("e6a729b7-7638-40b1-a8e7-ce9e0e666b70");
                //var delDept = context.Departments.SingleOrDefault(x => x.ID == id);
                if (delDept != null)
                {
                    context.Dapartments.Remove(delDept);
                    context.SaveChanges();
                }else
                {
                    Console.WriteLine("不存在");
                }

                //修改记录
                Console.WriteLine("修改记录");
                Console.WriteLine("=================================");
                Console.ReadKey();
                var editDepartment = context.Dapartments.SingleOrDefault(n => n.Name == "环境与食品检测学院");
                if (editDepartment != null)
                {
                    editDepartment.Name = "环境食品检测学院";
                    editDepartment.SortCode = "005";
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("未找到该记录");
                    //var newDepartment2 = new Dapartments
                    //{
                    //    ID = Guid.NewGuid(),
                    //    Name = editDepartment.Name,
                    //    Dscn = "",
                    //    SortCode = editDepartment.SortCode
                    //};
                    //context.Dapartments.Add(newDepartment2);
                    //context.SaveChanges();
                }


                //显示新的记录
                var departments2 = context.Dapartments.OrderBy(n => n.SortCode).ToList();

                foreach (var d in departments2)
                    Console.WriteLine("编号{0},{1},{2}", d.SortCode, d.Name, d.Dscn);

                Console.ReadKey();
            }
        }
    }
}