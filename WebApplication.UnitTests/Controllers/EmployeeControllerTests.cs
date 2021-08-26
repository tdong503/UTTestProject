using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication.Controllers;
using WebApplication.DTOs;
using WebApplication.Services;
using Xunit;

namespace WebApplication.UnitTests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IEmployeeService> _employeeServiceMock;

        private readonly EmployeeDto _validEmployeeRequest = new()
        {
            Name = "Test Name",
            Email = "test@test.com",
            Department = new DepartmentDto()
        };

        private readonly Employee _validEmployee = new()
        {
            Name = "Test Name",
            Email = "test@test.com",
            Department = new Department()
        };

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_employeeServiceMock.Object);
        }

        [Fact]
        public async Task GivenValidAEmployee_WhenCreateEmployee_ThenShouldReturnCreatedId()
        {
            //Assign
            var employeeDto = _validEmployeeRequest;
            _employeeServiceMock.Setup(x => x.CreateEmployee(employeeDto))
                .ReturnsAsync(1);

            //Act
            var result = await _controller.AddNewEmployeeWithDepartment(employeeDto);

            //Assert
            var createdObjectResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal((int) HttpStatusCode.Created, createdObjectResult.StatusCode.Value);
            var returnValue = Assert.IsType<int>(createdObjectResult.Value);
            Assert.Equal(1, returnValue);
        }

        [Fact]
        public async Task GivenEmployeeId_WhenGetAllEmployeesById_ThenShouldReturnExpectedEmployees()
        {
            //Assign
            var employees = new List<Employee> {_validEmployee};

            _employeeServiceMock.Setup(x => x.GetAllEmployeesById(It.IsAny<int>()))
                .ReturnsAsync(employees);
            //Act
            var result = await _controller.GetAllEmployeesById(1);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int) HttpStatusCode.OK, okObjectResult.StatusCode.Value);
            var returnValue = Assert.IsType<List<Employee>>(okObjectResult.Value);
            Assert.Single(returnValue);
        }
    }
}