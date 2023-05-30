using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Model;

namespace Marketplace.Core.Dal
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<int> GetCategoryIdByName(string name);
        Task<string> GetCategoryNameById(int id);
    }
}