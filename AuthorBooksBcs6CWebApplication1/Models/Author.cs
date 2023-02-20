using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorBooksBcs6CWebApplication1.Models
{
    public class Author
    {
        
            public int aid { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string Gender { get; set; }
            public bool Married { get; set; }
        
    }
}