using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tender.Tendering.Data.Interface;
using Tender.Tendering.Entities;
using Tender.Tendering.Settings;

namespace Tender.Tendering.Data
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));

            SourcingContextSeed.SeedData(Auctions);
        }

        public IMongoCollection<Auction> Auctions { get; }

        public IMongoCollection<Bid> Bids { get; }
    }
}
