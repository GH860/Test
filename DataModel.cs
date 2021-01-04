using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class DataModel
    {
        public String link { get; set; }
        public String title { get; set; }

        public DataModel(string link, string title)
        {
            this.link = link;
            this.title = title;
        }

        public DataModel()
        {
        }
    }
}
