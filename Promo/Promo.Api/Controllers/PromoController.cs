using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Promo.Core.Interfaces;
using Promo.Core.Models.APIRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promo.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PromoController : ControllerBase
    {

        private readonly IRepository _repository;
        public PromoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("SearchServices")]
        public async Task<IActionResult> SearchServices([FromQuery] string name)
        {
            var response = await _repository.SearchServices(name);
            return Ok(response);
        }

        [HttpPost]
        [Route("ActivateBonus")]
        public async Task<IActionResult> ActivateBonus([FromBody] ActivateBonusRequest request)
        {
            var response = await _repository.ActivateBonus(request.UserId, request.ServiceId);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetServices")]
        public async Task<IActionResult> GetServices([FromQuery] int page, int size)
        {
            var response = await _repository.GetServices(page, size);
            return Ok(response);
        }
    }
}
