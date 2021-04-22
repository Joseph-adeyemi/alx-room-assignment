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
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly ITokenService _tokenService;
        public AuthenticationController(IRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request)
        {
            var response = await _repository.AuthenticateUser(request);
            
            response.Token = response.Data != null ? _tokenService.GenerateToken(request.Username) : null;                                                                                                                                                                                                                                                                                                                                                                                                                                    
            return Ok(response);
        }

        
    }
}
