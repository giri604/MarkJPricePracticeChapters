using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public class Northwind : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            //global filter to remove discontinued products
            modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued);
        }
    }
}
