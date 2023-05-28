using System.Threading.Tasks;
using System.Collections.Generic;
using Marketplace.Core.Model;

namespace Marketplace.Core.Bl
{
    public interface ICategoryBl
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}