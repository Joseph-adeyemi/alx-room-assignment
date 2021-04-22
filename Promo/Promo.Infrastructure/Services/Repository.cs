using Promo.Core.Context;
using Promo.Core.Interfaces;
using Promo.Core.Models;
using Promo.Core.Models.APIRequests;
using Promo.Core.Models.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promo.Infrastructure.Services
{
    public class Repository : IRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public Repository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GenericAPIResponse> ActivateBonus(int userId, int serviceId)
        {
            GenericAPIResponse response = new GenericAPIResponse();
            try
            {
                await _dbContext.PromoActivations.AddAsync(new PromoActivation { UserId = userId, ServiceId = serviceId });
                await _dbContext.SaveChanges();

                response.ResponseCode = "00";
                response.ResponseDescription = "Request processed successfully";
            }
            catch (Exception ex)
            {

                response.ResponseCode = "99";
                response.ResponseDescription = "Request failed";
            }

            return response;
        }

        public async Task<GenericAPIResponse<AuthenticateUserResponse>> AuthenticateUser(AuthenticateUserRequest request)
        {
            GenericAPIResponse<AuthenticateUserResponse> response = new GenericAPIResponse<AuthenticateUserResponse>();
            try
            {
                var user = await _dbContext.Users.(u => u.Username.ToLower() == request.Username.ToLower() && u.Password == request.Password);
                //var user = await Task.Run(() => _dbContext.Users.Where(u => u.Username.ToLower() == request.Username.ToLower() && u.Password == request.Password).FirstOrDefault());
                if (user != null)
                {
                    response.ResponseCode = "00";
                    response.ResponseDescription = "Request processed successfully";
                    response.Data = new AuthenticateUserResponse { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName};
                }
                else
                {
                    response.ResponseCode = "99";
                    response.ResponseDescription = "Username or password is incorrect";
                }

            }
            catch (Exception ex)
            {

                response.ResponseCode = "99";
                response.ResponseDescription = "Request failed";
            }

            return response;
        }

        public async Task<GenericAPIResponse<PaginatedList<Service>>> GetServices(int page, int size)
        {
            GenericAPIResponse<PaginatedList<Service>> response = new GenericAPIResponse<PaginatedList<Service>>();
            try
            {
                var query = _dbContext.Services.AsQueryable();
                var servies = await PaginatedList<Service>.CreateAsync(query, page, size);

                response.ResponseCode = "00";
                response.ResponseDescription = "Request processed successfully";
                response.Data = servies;
            }
            catch (Exception ex)
            {

                response.ResponseCode = "99";
                response.ResponseDescription = "Request failed";
            }

            return response;
        }

        public async Task<GenericAPIResponse<Service>> SearchServices(string name)
        {
            GenericAPIResponse<Service> response = new GenericAPIResponse<Service>();
            try
            {
                var service = await Task.Run(() => _dbContext.Services.Where(s => s.Name.ToLower() == name.ToLower()).FirstOrDefault());

                response.ResponseCode = "00";
                response.ResponseDescription = "Request processed successfully";
                response.Data = service;
            }
            catch (Exception ex)
            {

                response.ResponseCode = "99";
                response.ResponseDescription = "Request failed";
            }

            return response;
        }
    }
}
