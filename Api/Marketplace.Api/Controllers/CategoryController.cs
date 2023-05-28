using Microsoft.AspNetCore.Mvc;
using Marketplace.Core.Bl;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Marketplace.Core.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Marketplace.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryBl categoryBl;
        public CategoryController(ILogger<CategoryController> logger, ICategoryBl categoryBl)
        {
            this.logger = logger;
            this.categoryBl =  categoryBl;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            IEnumerable<Category> result;
            try
            {
                result = await this.categoryBl.GetAllCategoriesAsync().ConfigureAwait(false);
            }catch(Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }
            return this.Ok(result);
        }
    }
}