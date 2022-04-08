using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProjectWithAngularCore.Entities.Books
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int BookPrice { get; set; }
        public string PhotoPath { get; set; }

    }
}
