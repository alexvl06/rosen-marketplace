using System.Threading.Tasks;
using System.Collections.Generic;
using Marketplace.Core.Model;

namespace Marketplace.Core.Bl
{
    public interface IOfferBl
    {
        Task<IEnumerable<Offer>> GetOffersAsync(int pageNumber, int pageSize);
        Task<bool> CreateOffer(Offer offer);
        Task<int> OffersQuantity();
    }
}