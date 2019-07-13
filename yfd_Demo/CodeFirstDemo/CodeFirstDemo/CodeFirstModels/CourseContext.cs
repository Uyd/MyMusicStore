using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo.CodeFirstModels
{
    /// <summary>
    /// 数据上下文 映射到DB
    /// </summary>
    public class CourseContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        //被创建 数据初始化
    //        Database.SetInitializer<CourseContext>();
    //    }
    //}

    ////每一次运行时都重新生成新数据库
    //public class CourseInitializer : DropCreateDatabaseIfModelChanges<CourseContext>
    //{

    }
}
