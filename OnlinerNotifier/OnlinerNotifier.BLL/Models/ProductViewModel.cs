namespace OnlinerNotifier.BLL.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public int OnlinerId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }
    }
}
