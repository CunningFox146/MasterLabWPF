using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MasterLabWPF
{
    class Product
    {
        public Product()
        {

        }

        public Product(string shortName, string fullName, string description, string category, int rating, int price, string imagesrc)
        {
            this.shortName = shortName;
            this.fullName = fullName;
            this.description = description;
            this.category = category;
            this.rating = rating;
            this.price = price;
            this.imagesrc = imagesrc;
        }
        
        public string shortName { get; set; }
        public string fullName { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public int rating { get; set; }
        public int price { get; set; }
        public string imagesrc { get; set; }
        //public Image image { get; set; }


    }
}
