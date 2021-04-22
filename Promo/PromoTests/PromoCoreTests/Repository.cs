using Microsoft.EntityFrameworkCore;
using Moq;
using Promo.Core.Interfaces;
using Promo.Core.Models;
using Promo.Core.Models.APIRequests;
using Promo.Core.Models.APIResponses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PromoTests.PromoCoreTests
{
    public class Repository
    {
        protected Mock<IApplicationDbContext> dbContextMock;
        protected Mock<IRepository> repositoryMock;
        [Fact]
        public async Task CheckThatUserCanGetServices()
        {
            var services = GetPaginatedServices();
            var queryableService = services.AsQueryable();

            var mockSet = new Mock<DbSet<Service>>();
            mockSet.As<IDbAsyncEnumerable<Service>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Service>(queryableService.GetEnumerator()));

            mockSet.As<IQueryable<Service>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Service>(queryableService.Provider));

            mockSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(queryableService.Expression);
            mockSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(queryableService.ElementType);
            mockSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(queryableService.GetEnumerator());

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(c => c.Services).Returns(mockSet.Object);

            
            repositoryMock = new Mock<IRepository>();
        
            var systemUnderTest = new Promo.Infrastructure.Services.Repository(dbContextMock.Object);
            repositoryMock.Setup(x => x.GetServices(It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync(new GenericAPIResponse<PaginatedList<Service>> { ResponseCode = "00", ResponseDescription = "Request Processsed Successfully"}); //Data = services });

            var result = await systemUnderTest.GetServices(1, 1);
            Assert.True(result.ResponseCode == "00");
            Assert.True(result.Data == services);
        }

        [Fact]
        public async Task CheckThatUserCanGetServiceByName()
        {
            var service = GetServices()[0];
            var queryableService = GetServices().AsQueryable();
            var mockSet = new Mock<DbSet<Service>>();
            mockSet.As<IDbAsyncEnumerable<Service>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Service>(queryableService.GetEnumerator()));

            mockSet.As<IQueryable<Service>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Service>(queryableService.Provider));

            mockSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(queryableService.Expression);
            mockSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(queryableService.ElementType);
            mockSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(queryableService.GetEnumerator());

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(c => c.Services).Returns(mockSet.Object);

            dbContextMock = new Mock<IApplicationDbContext>();
            repositoryMock = new Mock<IRepository>();
            var systemUnderTest = new Promo.Infrastructure.Services.Repository(dbContextMock.Object);
            repositoryMock.Setup(x => x.SearchServices(It.IsAny<string>()))
               .ReturnsAsync(new GenericAPIResponse<Service> { ResponseCode = "00", ResponseDescription = "Request Processsed Successfully", Data = service });

            var result = await systemUnderTest.SearchServices("serviceName");
            Assert.True(result.ResponseCode == "00");
            //Assert.True(result.Data == service);
        }

        [Fact]
        public async Task CheckThatUserCanActivateBonus()
        {

            var mockSet = new Mock<DbSet<PromoActivation>>();

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(m => m.PromoActivations).Returns(mockSet.Object);

            repositoryMock = new Mock<IRepository>();
            var systemUnderTest = new Promo.Infrastructure.Services.Repository(dbContextMock.Object);
            repositoryMock.Setup(x => x.ActivateBonus(It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync(new GenericAPIResponse { ResponseCode = "00", ResponseDescription = "Request Processsed Successfully" });

            var result = await systemUnderTest.ActivateBonus(1, 1);
            Assert.True(result.ResponseCode == "00");
        }

        [Fact]
        public async Task CheckThatUserCanLogin()
        {

            var mockSet = new Mock<DbSet<User>>();

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(m => m.Users).Returns(mockSet.Object);


            var user = GetUser();
            repositoryMock = new Mock<IRepository>();
            var systemUnderTest = new Promo.Infrastructure.Services.Repository(dbContextMock.Object);
            repositoryMock.Setup(x => x.AuthenticateUser(It.IsAny<AuthenticateUserRequest>()))
               .ReturnsAsync(new GenericAPIResponse<AuthenticateUserResponse> { ResponseCode = "00", ResponseDescription = "Request Processsed Successfully", Data = user, Token = "xyzToken" });

            var result = await systemUnderTest.AuthenticateUser(new AuthenticateUserRequest { Username = "user1", Password = "password"});
            Assert.True(result.ResponseCode == "00");
            Assert.True(result.Data == user);
        }

        private PaginatedList<Service> GetPaginatedServices()
        {
            var dbResult = new List<Service>
            {
               new Service { Id = 1, Name = "Appvision.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 2, Name = "Analytics.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 3, Name = "Siteconstructor.io", Description = "My service description", PromoCode = "itpromocodes" }
               };
            PaginatedList<Service> services = new PaginatedList<Service>(dbResult, dbResult.Count, 1, 1);

            return services;
        }

        private List<Service> GetServices()
        {
            
            List<Service> services = new List<Service> {
               new Service { Id = 1, Name = "Appvision.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 2, Name = "Analytics.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 3, Name = "Siteconstructor.io", Description = "My service description", PromoCode = "itpromocodes" }};

            return services;
        }

        private AuthenticateUserResponse GetUser()
        {
            var user = new AuthenticateUserResponse
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            };

            return user;
        }
    }
}
