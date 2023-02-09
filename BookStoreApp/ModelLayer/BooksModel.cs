using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class BooksModel
    {
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string Rating { get; set; } 
        public string NoOfPeopleRated { get; set; }
        public string BookImage { get; set; }
        public  int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
        public string BookDescription { get; set; }
        public int BookQuantity { get; set; }
    }
}
