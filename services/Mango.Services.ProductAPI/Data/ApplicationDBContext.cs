using System;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mango.services.ProductAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Samosa",
                    Price = 15.00,
                    Description = "Samosa with spicy chutney",
                    CategoryName = "Appetizer",
                    ImageUrl = "https://www.google.com",
                   
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Paneer Tikka",
                    Price = 13.99,
                    Description = "Paneer Tikka with green chutney",
                    CategoryName = "Appetizer",
                    ImageUrl = "https://www.google.com",
                
                }
            );
        }
    }
}