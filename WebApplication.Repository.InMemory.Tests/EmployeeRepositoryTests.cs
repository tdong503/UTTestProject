using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using WebApplication.Reposities;
using Xunit;

namespace WebApplication.Repository.InMemory.Tests
{
    public class EmployeeRepositoryTests
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRepositoryTests(ApplicationTestFixture fixture)
        {
            _applicationDbContext = fixture.GetDbContext();
            //_employeeRepository = new EmployeeRepository();
        }
        
        [Fact]
        public async Task GetAllEmployees()
        {
            var employee = new Employee
            {
                Name = "test name",
                Email = "test@test.com"
            };
            _applicationDbContext.Employees.Add(employee);
            
            var allEmployees = await _employeeRepository.GetAllEmployees();
            
            Assert.Equal(1, allEmployees.Count);
        }

        // private Task<ApplicationDbContext> GetDbContext()
        // {
            // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                // .UseInMemoryDatabase();
        // }
    }
}