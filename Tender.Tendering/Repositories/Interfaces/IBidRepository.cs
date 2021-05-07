using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tender.Tendering.Entities;

namespace Tender.Tendering.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsByAuctionId(string id);
        Task<List<Bid>> GetAllBidsByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
