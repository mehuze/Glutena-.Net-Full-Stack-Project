using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Glutena.Data
{
    public class GlutenaDbContext : DbContext
    {//EF Core’un kalbidir.Veritabanı ile uygulama arasındaki köprüdür.
        public GlutenaDbContext(DbContextOptions<GlutenaDbContext> options)
            : base(options)//Program.cs’te verdiğimiz connection string buraya gelir.
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }

    public class GlutenaDbContextFactory
       : IDesignTimeDbContextFactory<GlutenaDbContext>
    {
        public GlutenaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GlutenaDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=GlutenaDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new GlutenaDbContext(optionsBuilder.Options);
        }

    }

}

