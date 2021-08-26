using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApplication.DTOs;

namespace WebApplication.Reposities
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IApplicationReadDbConnection _readDbConnection;
        private readonly IApplicationWriteDbConnection _writeDbConnection;
        
        public EmployeeRepository(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }
        
        public async Task<int> CreateEmployee(EmployeeDto employeeDto)
        {
            _dbContext.Connection.Open();
            using (var transaction = _dbContext.Connection.BeginTransaction())
            {
                try
                {
                    _dbContext.Database.UseTransaction(transaction as DbTransaction);
                    //Check if Department Exists (By Name)
                    bool DepartmentExists = await _dbContext.Departments.AnyAsync(a => a.Name == employeeDto.Department.Name);
                    if (DepartmentExists)
                    {
                        throw new Exception("Department Already Exists");
                    }
                    //Add Department
                    var addDepartmentQuery = $"INSERT INTO Departments(Name,Description) VALUES('{employeeDto.Department.Name}','{employeeDto.Department.Description}');SELECT CAST(SCOPE_IDENTITY() as int)";
                    var departmentId = await _writeDbConnection.QuerySingleAsync<int>(addDepartmentQuery, transaction: transaction);
                    //Check if Department Id is not Zero.
                    if (departmentId == 0)
                    {
                        throw new Exception("Department Id");
                    }
                    //Add Employee
                    var employee = new Employee
                    {
                        DepartmentId = departmentId,
                        Name = employeeDto.Name,
                        Email = employeeDto.Email
                    };
                    await _dbContext.Employees.AddAsync(employee);
                    await _dbContext.SaveChangesAsync(default);
                    //Commmit
                    transaction.Commit();
                    //Return EmployeeId
                    return employee.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbContext.Connection.Close();
                }
            }
        }

        public async Task<List<Employee>> GetAllEmployeesById(int id)
        {
            var employees = await _dbContext.Employees.Include(a => a.Department).Where(a => a.Id == id).ToListAsync();
            return employees;
        }

        public async Task<IReadOnlyList<Employee>> GetAllEmployees()
        {
            var query = $"Select * from \"Employees\"";
            var employees = await _readDbConnection.QueryAsync<Employee>(query);
            return employees;
        }
    }

    public interface IEmployeeRepository
    {
        Task<int> CreateEmployee(EmployeeDto employeeDto);
        Task<List<Employee>> GetAllEmployeesById(int id);
        Task<IReadOnlyList<Employee>> GetAllEmployees();
    }
}