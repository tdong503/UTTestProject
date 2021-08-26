using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using WebApplication.DTOs;
using WebApplication.Reposities;

namespace WebApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> CreateEmployee(EmployeeDto employeeDto)
        {
            return await _employeeRepository.CreateEmployee(employeeDto);
        }

        public async Task<List<Employee>> GetAllEmployeesById(int id)
        {
            return await _employeeRepository.GetAllEmployeesById(id);
        }

        public async Task<IReadOnlyList<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }
    }

    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeDto employeeDto);
        Task<List<Employee>> GetAllEmployeesById(int id);
        Task<IReadOnlyList<Employee>> GetAllEmployees();
    }
}