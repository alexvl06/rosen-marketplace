using Marketplace.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Marketplace.Core.Dal
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetOffersByPageIndex(int pageIndex, int pageSize);
        Task<bool> CreateOffer(Offer offer);
        Task<int> OffersQuantity();
    }
}