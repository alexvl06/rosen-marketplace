using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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

        public async Task<OfferDTO> CreateOffer(OfferDTO offerDTO)
        {
            int categoryId = await categoryRepository.GetCategoryIdByName(offerDTO.CategoryName);
            int userId = await userRepository.GetUserIdByName(offerDTO.Username);
            if (userId == 0)
            {
                userId = await userRepository.CreateNewUser(offerDTO.Username);
            }

            _context.offers.Add(DTOToEntityMapper(offerDTO, categoryId, userId));
            await _context.SaveChangesAsync();
            return offerDTO;
        }

        public async Task<OfferDTO> GetOfferById(Guid id)
        {
            return EntityToDTOMapper(
                await _context.offers
                    .Include(o => o.User)
                    .Include(o => o.Category)
                    .FirstOrDefaultAsync(o => o.Id == id)
            );
        }

        public async Task<IEnumerable<OfferDTO>> GetOffersByPageIndex(int pageIndex, int pageSize)
        {
            IEnumerable<Offer> offers = await _context.offers
                .Include(o => o.Category)
                .Include(o => o.User)
                .OrderByDescending(o => o.UserId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<OfferDTO> offersDTO = new List<OfferDTO>();
            foreach (Offer offer in offers)
            {
                offersDTO.Add(EntityToDTOMapper(offer));
            }
            return offersDTO;
        }

        public async Task<int> OffersQuantity()
        {
            return await _context.offers.CountAsync();
        }

        private OfferDTO EntityToDTOMapper(Offer offer)
        {
            OfferDTO offerDTO = new OfferDTO();
            offerDTO.Description = offer.Description;
            offerDTO.Location = offer.Location;
            offerDTO.PictureUrl = offer.PictureUrl;
            offerDTO.PublishedOn = offer.PublishedOn;
            offerDTO.Title = offer.Title;
            if (offer.User != null)
            {
                offerDTO.Username = offer.User.Username;
            }

            if (offer.Category != null)
            {
                offerDTO.CategoryName = offer.Category.Name;
            }
            return offerDTO;
        }

        private Offer DTOToEntityMapper(OfferDTO offerDTO, int categoryId, int userId)
        {
            Offer offer = new Offer();
            offer.CategoryId = categoryId;
            offer.UserId = userId;
            offer.Description = offerDTO.Description;
            offer.Location = offerDTO.Location;
            offer.PictureUrl = offerDTO.PictureUrl;
            offer.PublishedOn = offerDTO.PublishedOn;
            offer.Title = offerDTO.Title;
            return offer;
        }
    }
}
