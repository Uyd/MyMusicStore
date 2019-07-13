using CodeFirstDemo.CodeFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo.Migrations
{
    public class StudentSeed
    {
        public static void Seed(CourseContext context)
        {
            var s1 = new Student()
            {
                StudentCode = "20170310079",
                Name = "叶仿达",
                Sex = true,
                Birthday = Convert.ToDateTime("2017-03-19"),
                Address = "陆川",
                Phone = "15077577075",
                Department = context.Departments.SingleOrDefault(x => x.Name == "电子信息工程学院")
            };
            context.Students.Add(s1);
            context.SaveChanges();
        }
    }
}
