namespace OnlinerNotifier.BLL.Models
{
    public class UserProductViewModel
    {
        public int Id { get; set; }

        public ProductViewModel Product { get; set; }

        public bool IsTracked { get; set; }
    }
}
