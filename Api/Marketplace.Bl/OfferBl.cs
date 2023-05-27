
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Model;
using Marketplace.Core.Dal;

namespace Marketplace.Bl
{
    public class OfferBl : IOfferBl
    {
        private readonly IOfferRepository offerRepository;

        public OfferBl(IOfferRepository offerRepository){
            this.offerRepository = offerRepository;
        }
        public async Task<IEnumerable<Offer>> GetOffersAsync(int pageNumber, int pageSize)
        {
            return await offerRepository.GetOffersByPageIndex(pageNumber, pageSize).ConfigureAwait(false);
        }
    }
}