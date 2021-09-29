using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;
using Microsoft.Extensions.Logging;
using System;

namespace Packt.Shared
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryingCategories();
            //QueryingProducts();
        }

        private static void QueryingCategories()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Categories and how many products they have: ");

                IQueryable<Category> cats;
                //db.Categories;
                //.Include(c => c.Products);        //It enables the early/eager loading

                db.ChangeTracker.LazyLoadingEnabled = false;
                Write("Enable eager loading? (Y/N): ");
                bool eagerloading = (ReadKey().Key == ConsoleKey.Y);
                bool explicitloading = false;
                WriteLine();

                if (eagerloading)
                {
                    cats = db.Categories.Include(c => c.Products);
                }
                else
                {
                    cats = db.Categories;
                    Write("Enable explicit loading? (Y/N): ");
                    explicitloading = (ReadKey().Key == ConsoleKey.Y);
                    WriteLine();
                }

                foreach (var c in cats)
                {
                    if (explicitloading)
                    {
                        Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                        ConsoleKeyInfo key = ReadKey();
                        WriteLine();
                        if (key.Key == ConsoleKey.Y)
                        {
                            var products = db.Entry(c).Collection(c2 => c2.Products);
                            if (!products.IsLoaded)
                            {
                                products.Load();
                            }
                        }
                    }
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
    
        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Products that cost more than a price, highest at top.");
                string input;
                float price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();

                } while (!float.TryParse(input, out price));

                IQueryable<Product> prods = db.Products
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product =>  product.Cost);

                foreach (Product item in prods)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
                                item.ProductID,
                                item.ProductName,
                                item.Cost,
                                item.Stock);
                }
            }
        }
    }
}
