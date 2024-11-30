using System;
using Mango.Services.ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace mango.services.ShoppingCart.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders{get;set;}

      
    }
}