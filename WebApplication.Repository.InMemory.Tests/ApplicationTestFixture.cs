using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Persistence.Contexts;
using WebApplication.Reposities;
using WebApplication.Services;

namespace WebApplication.Repository.InMemory.Tests
{
    public class ApplicationTestFixture
    {
        public ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        
            return new ApplicationDbContext(options);
        }
        
        private readonly TestServer _testServer;
        
        public ApplicationTestFixture()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .ConfigureLogging((logging) =>
                {
                    logging.AddConsole();
                })
                .ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("postgres");
                    });
                })
                .UseStartup<Startup>();

            _testServer = new TestServer(webHostBuilder);
            ServerServices = _testServer.Host.Services;
            ProvisionData(ServerServices.GetRequiredService<ApplicationDbContext>());
            ConfigureClientServices(_testServer.CreateClient());
        }

        public IServiceProvider ClientServices { get; private set; }
        public IServiceProvider ServerServices { get; private set; }

        private void ConfigureClientServices(HttpClient httpClient)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(builder => builder.AddConsole());
            services.AddSingleton(httpClient);
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            ClientServices = services.BuildServiceProvider();
        }

        private void ProvisionData(ApplicationDbContext dbContext)
        {
            //dbContext.Add();            
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
    }
}