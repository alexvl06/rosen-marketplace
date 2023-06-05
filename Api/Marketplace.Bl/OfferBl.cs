
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Model;
using Marketplace.Core.Dal;
using System;

namespace Marketplace.Bl
{
    public class OfferBl : IOfferBl
    {
        private readonly IOfferRepository offerRepository;

        public OfferBl(IOfferRepository offerRepository){
            this.offerRepository = offerRepository;
        }

        public Task<OfferDTO> CreateOffer(OfferDTO offer)
        {
            return offerRepository.CreateOffer(offer);
        }

        public async Task<OfferDTO> GetOfferById(Guid id)
        {
           return await offerRepository.GetOfferById(id);
        }

        public async Task<IEnumerable<OfferDTO>> GetOffersAsync(int pageNumber, int pageSize)
        {
            return await offerRepository.GetOffersByPageIndex(pageNumber, pageSize).ConfigureAwait(false);
        }

        public async Task<int> OffersQuantity()
        {
           return await offerRepository.OffersQuantity();
        }
    }
}