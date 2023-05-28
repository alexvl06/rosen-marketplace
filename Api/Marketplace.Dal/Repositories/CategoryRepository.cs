using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Dal.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MarketplaceDb _context;
        public CategoryRepository()
        {
            _context = new MarketplaceDb();
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.GetAllCategories();
        }
    }
}