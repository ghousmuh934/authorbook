using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorBooksBcs6CWebApplication1.Models
{
    public class Book
    {
        public int bid { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public int publishyear { get; set; }
        public int aid { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }

    }
}