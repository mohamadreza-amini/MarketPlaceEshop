using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Model.Entities.Addresses;
using Model.Entities.Categories;
using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ChangeInterceptors;
using Model.Entities.Reports;


namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFeature> Categoryfeatures { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeatureValue> ProductFeatureValues { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ViewLog> ViewLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarketPlace1;TrustServerCertificate=True;Integrated Security=SSPI;");
            optionsBuilder.AddInterceptors(new ChangeInterceptor());

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }

        /* public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
         {
             PrepareEntity();
             return base.SaveChangesAsync(cancellationToken);
         }
         public override int SaveChanges()
         {
             PrepareEntity();
             return base.SaveChanges();
         }

         private void PrepareEntity()
         {
             foreach (var entity in ChangeTracker.Entries())
             {
                 if (entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
                 {
                     try
                     {
                         entity.Property("UpdateDatetime").CurrentValue = DateTime.Now;
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine($"Error while setting properties: {ex.Message}");
                     }
                 }
                 if (entity.State == EntityState.Added)
                 {
                     try
                     {
                         entity.Property("UpdateDatetime").CurrentValue = DateTime.Now;
                         entity.Property("CreateDatetime").CurrentValue = DateTime.Now;
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine($"Error while setting properties: {ex.Message}");
                     }
                 }
             }
         }*/
    }
}
