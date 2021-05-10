using System;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;
using Ordering.Domain.Entities;
using System.IO;
using Newtonsoft.Json;

namespace Tender.Order.Test
{
    public class OrderTestContext : OrderContext
    {
        public OrderTestContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            seedData<Ordering.Domain.Entities.Order>(modelBuilder, "/Users/erdemsavar/Desktop/MicroserviceTutorial/TenderMicroService/Tender.Order.Test/Data/orders.json");
        }

        private void seedData<T>(ModelBuilder modelBuilder, string file) where T : class
        {
            using (StreamReader reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T[]>(json);
                modelBuilder.Entity<T>().HasData(data);
            }

        }
    }
}
