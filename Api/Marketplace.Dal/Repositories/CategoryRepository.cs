using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Marketplace.Dal.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MarketplaceContext _context;
        public CategoryRepository(MarketplaceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<int> GetCategoryIdByName(string name)
        {
            Category category = await _context.categories.Where(c=>c.Name == name).FirstOrDefaultAsync();
            if(category != null){
                return category.Id;
            } else{
                return 0;
            }
        }

        public async Task<string> GetCategoryNameById(int id)
        {
            Category category = await _context.categories.Where(c=>c.Id == id).FirstOrDefaultAsync();
            return category.Name;
        }
    }
}