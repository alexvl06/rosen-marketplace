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
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> logger;
        private readonly IOfferBl offerBl;

        public OfferController(ILogger<OfferController> logger, IOfferBl offerBl)
        {
            this.logger = logger;
            this.offerBl = offerBl;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> Get(
            [FromQuery] int index,
            [FromQuery] int size
        )
        {
            IEnumerable<Offer> result;
            int totalOffers = 0;
            try
            {
                result = await this.offerBl.GetOffersAsync(index, size).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error trying to get offers.");
            }
            try
            {
                totalOffers = await this.offerBl.OffersQuantity();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error trying to count offers.");
            }
            Response.Headers.Add("total-offers", totalOffers.ToString());
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(
            [FromBody] Offer offer
        )
        {
            bool result;
            try
            {
                result = await this.offerBl.CreateOffer(offer).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }
    }
}
