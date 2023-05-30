using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Marketplace.Dal.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly MarketplaceContext _context;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        public OfferRepository(MarketplaceContext context, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
            _context = context;
        }

        public async Task<bool> CreateOffer(Offer offer)
        {
            bool state = false;
            int categoryId = await categoryRepository.GetCategoryIdByName(offer.CategoryName);
            int userId = await userRepository.GetUserIdByName(offer.Username);
            if(userId ==0){
                userId = await userRepository.CreateNewUser(offer.Username);
            }
            offer.CategoryId = categoryId;
            offer.UserId = userId;
            _context.offers.Add(offer);
            await _context.SaveChangesAsync();
            state = true;
            return state;
        }

        public async Task<IEnumerable<Offer>> GetOffersByPageIndex(int pageIndex, int pageSize)
        {
            IEnumerable<Offer> offers = await _context.offers.OrderByDescending(o=>o.UserId).Skip((pageIndex-1)*pageSize).Take(pageSize)
            .ToListAsync();

            foreach(Offer offer in offers){
                string categoryName = await categoryRepository.GetCategoryNameById(offer.CategoryId);
                string username = await userRepository.GetUserNameById(offer.UserId);
                if(categoryName!=null){
                    offer.CategoryName = categoryName;
                }
                if(username!=null){
                    offer.Username = username;
                }
            }
            return offers;
        }

        public async Task<int> OffersQuantity()
        {
            return await _context.offers.CountAsync();
        }
    }
}