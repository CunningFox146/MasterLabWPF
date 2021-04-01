using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MasterLabWPF
{
    [Serializable]
    public class Product
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
        public int Price { get; set; }
        public string Imagesrc { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }

        public int DiscountedPrice
        {
            get => Price-(int)(Price * (float)Discount / 100);
        }
        //public Image image { get; set; }

        public Product()
        {

        }

        public Product(string shortName, string fullName, string category, double rating, int price, string imagesrc, int discount, int count)
        {
            this.ShortName = shortName;
            this.FullName = fullName;
            this.Category = category;
            this.Rating = rating;
            this.Price = price;
            this.Imagesrc = imagesrc;
            this.Discount = discount;
            this.Amount = count;

        }


      


    }
}
