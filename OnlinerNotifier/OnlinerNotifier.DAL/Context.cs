using System.Data.Entity;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(x => x.PriceChanges)
                .WithRequired(x => x.Product)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPriceChange> PriceChanges { get; set; }

        public DbSet<UserProduct> UserProducts { get; set; }
    }
}
