using Promo.Core.Models;
using Promo.Core.Models.APIRequests;
using Promo.Core.Models.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promo.Core.Interfaces
{
    public interface IRepository
    {
        Task<GenericAPIResponse<Service>> SearchServices(string name);
        Task<GenericAPIResponse<PaginatedList<Service>>> GetServices(int page, int size);
        Task<GenericAPIResponse> ActivateBonus(int userId, int serviceId);
        Task<GenericAPIResponse<AuthenticateUserResponse>> AuthenticateUser(AuthenticateUserRequest request);
    }
}
