using Glutena.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Glutena.Data
{
    public class GlutenaDbContext : IdentityDbContext<IdentityUser>
    {
        public GlutenaDbContext(DbContextOptions<GlutenaDbContext> options)
            : base(options)
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
        public DbSet<BlacklistWord> BlacklistWords { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<ContentPage> ContentPages { get; set; }
        public DbSet<ContentSection> ContentSections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

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
            // Migration komutları (Add-Migration / Update-Database) bağlantıyı
            // ayar dosyalarından okur, kodda sabit adres yazmaz.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GlutenaDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new GlutenaDbContext(optionsBuilder.Options);
        }
    }
}