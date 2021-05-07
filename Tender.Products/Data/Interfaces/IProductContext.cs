using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tender.Products.Entities;

namespace Tender.Products.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get;  }
    }
}
