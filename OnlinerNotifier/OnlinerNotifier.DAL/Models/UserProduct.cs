namespace OnlinerNotifier.DAL.Models
{
    public class UserProduct
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public bool IsTracked { get; set; }
    }
}
