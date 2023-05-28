using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Model;
using Marketplace.Core.Dal;

namespace Marketplace.Bl
{
    public class CategoryBl: ICategoryBl
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryBl(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllCategories().ConfigureAwait(false);
        }
    }
}