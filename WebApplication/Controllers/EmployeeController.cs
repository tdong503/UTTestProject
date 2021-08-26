using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllEmployeesById(int id)
        {
            return Ok(await _employeeService.GetAllEmployeesById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeWithDepartment(EmployeeDto employeeDto)
        {
            return Created("", await _employeeService.CreateEmployee(employeeDto));
        }
    }
}