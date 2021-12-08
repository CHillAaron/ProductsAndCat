using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsAndCat.Models;

namespace ProductsAndCat.Context
{
    public class ProdAndCatDbContext :DbContext
    {
        public ProdAndCatDbContext(DbContextOptions options) : base(options) { }
        // the "Monsters" table name will come from the DbSet variable name
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProdAndCat> ProdsAndCats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=192.168.1.104;Username=postgres;Password=root;Database=ProdAndCat");
    }
}
