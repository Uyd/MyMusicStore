namespace MVCdemo.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCdemo.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MVCdemo.Models.MovieDBContext context)
        {
            context.Database.ExecuteSqlCommand("delete movies");
            var m1 = new Movie
            {
                Title = "\"毒液\"",
                ReleaseDate = DateTime.Parse("2019-11-2"),
                Genre = "3D 动作 好莱坞",
                Price = 50.00M
            };
            var m2 = new Movie
            {
                Title = "\"复仇者联盟IV\"",
                ReleaseDate = DateTime.Parse("2019-9-9"),
                Genre = "3D 动作 好莱坞",
                Price = 30.00M
            };
            var m3 = new Movie
            {
                Title = "\"极限特工4\"",
                ReleaseDate = DateTime.Parse("2018-10-12"),
                Genre = "3D 动作 好莱坞",
                Price = 20.00M
            };
            context.Movies.Add(m1);
            context.Movies.Add(m2);
            context.Movies.Add(m3);
            context.SaveChanges();
        }
    }
}
