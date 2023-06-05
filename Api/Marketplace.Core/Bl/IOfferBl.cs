using System.Threading.Tasks;
using System.Collections.Generic;
using Marketplace.Core.Model;
using System;

namespace Marketplace.Core.Bl
{
    public interface IOfferBl
    {
        Task<IEnumerable<OfferDTO>> GetOffersAsync(int pageNumber, int pageSize);
        Task<OfferDTO> CreateOffer(OfferDTO offerDTO);
        Task<OfferDTO> GetOfferById(Guid id);
        Task<int> OffersQuantity();
    }
}