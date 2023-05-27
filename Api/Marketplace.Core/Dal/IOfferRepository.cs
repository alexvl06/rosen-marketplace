using Marketplace.Core.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Marketplace.Core.Dal
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetOffersByPageIndex(int pageIndex, int pageSize);
    }
}