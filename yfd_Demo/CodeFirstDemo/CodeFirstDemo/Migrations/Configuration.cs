namespace CodeFirstDemo.Migrations
{
    using CodeFirstModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstDemo.CodeFirstModels.CourseContext>
    {
        //�����Ǩ�������ļ�
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        //���ӷ���
        protected override void Seed(CodeFirstDemo.CodeFirstModels.CourseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            ////ע��˳��
            //context.Database.ExecuteSqlCommand("delete courses");
            //context.Database.ExecuteSqlCommand("delete students");
            //context.Database.ExecuteSqlCommand("delete departments");
            ////��ʼ��ѧԺ����
            //DepartmentSeed.Seed(context);
            ////רҵ
            //CourseSeed.Seed(context); 
            ////ѧ��
            //StudentSeed.Seed(context);
        }
    }
}
