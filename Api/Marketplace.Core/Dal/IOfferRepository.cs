using Marketplace.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Marketplace.Core.Dal
{
    public interface IOfferRepository
    {
        Task<IEnumerable<OfferDTO>> GetOffersByPageIndex(int pageIndex, int pageSize);
       Task<OfferDTO> GetOfferById(Guid id);
        Task<OfferDTO> CreateOffer(OfferDTO offerDTO);
        Task<int> OffersQuantity();
    }
}