using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCdemo.Models
{
    public class Movie
    {
        public Guid ID { get; set; }
        public string Title{get; set;}
        public DateTime ReleaseDate { get; set; }//上映时间
        public String Genre { get; set; }
        public decimal Price { get; set; }
        public Movie()
        {
            ID = Guid.NewGuid();
            ReleaseDate = DateTime.Now;
        }
    }
}