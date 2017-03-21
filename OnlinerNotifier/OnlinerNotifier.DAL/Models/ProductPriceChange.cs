using System;

namespace OnlinerNotifier.DAL.Models
{
    public class ProductPriceChange
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public decimal OldMinPrice { get; set; }

        public decimal OldMaxPrice { get; set; }

        public decimal NewMinPrice { get; set; }

        public decimal NewMaxPrice { get; set; }

        public DateTime CheckTime { get; set; }
    }
}
