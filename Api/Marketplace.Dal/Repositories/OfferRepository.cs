using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Dal.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly MarketplaceDb _context;
        public OfferRepository()
        {
            _context = new MarketplaceDb();
        }

        public Task<bool> CreateOffer(Offer offer)
        {
            return _context.CreateNewOffer(offer);
        }

        public async Task<IEnumerable<Offer>> GetOffersByPageIndex(int pageIndex, int pageSize)
        {
            return await _context.GetOffersByPageIndex(pageIndex,pageSize);
        }
    }
}