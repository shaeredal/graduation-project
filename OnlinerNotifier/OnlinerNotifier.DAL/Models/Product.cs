using System.Collections.Generic;

namespace OnlinerNotifier.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int OnlinerId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }

        public ICollection<ProductPriceChange> PriceChanges { get; set; }

        public Product()
        {
            UserProducts = new List<UserProduct>();
            PriceChanges = new List<ProductPriceChange>();
        }
    }
}
