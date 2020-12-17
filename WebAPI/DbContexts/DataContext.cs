using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.DbContexts
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    Id = int.Parse("1"),
                    Name = "New York",
                    Country ="United State"
                },
                new City()
                {
                    Id = int.Parse("2"),
                    Name = "Los Angeles",
                    Country = "United State"
                },
                new City()
                {
                    Id = int.Parse("3"),
                    Name = "Atlanta",
                    Country = "United State"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
