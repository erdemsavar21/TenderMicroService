using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tender.Tendering.Entities;

namespace Tender.Tendering.Data.Interface
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
