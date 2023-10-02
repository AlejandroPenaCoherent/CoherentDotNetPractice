using DotNetPractice.Backend.Models;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly NorthwindContext _dbContext;

        public EmployeesService(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResultWithData<Employee>> Create(Employee employee)
        {
            var operationResult = new OperationResultWithData<Employee>();

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            operationResult.Succeded = true;
            operationResult.Data = employee;

            return operationResult;
        }
        
        public async Task<OperationResultWithData<Employee>> Read(int employeeId)
        {
            var operationResult = new OperationResultWithData<Employee>();

            var employee = await _dbContext
                .Set<Employee>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (employee != null)
            {
                operationResult.Data = employee;
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Update(Employee employee)
        {
            var operationResult = new OperationResult();

            var existingEmployee = await _dbContext
                .Set<Employee>()
                .FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);

            if (existingEmployee is null)
            {
                operationResult.AddError($"Employee with Id {employee.EmployeeId} not found.");
                return operationResult;
            }

            existingEmployee.LastName = employee.LastName;
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.Title = employee.Title;
            existingEmployee.TitleOfCourtesy = employee.TitleOfCourtesy;
            existingEmployee.BirthDate = employee.BirthDate;
            existingEmployee.Address = employee.Address;
            existingEmployee.City = employee.City;
            existingEmployee.Region = employee.Region;
            existingEmployee.PostalCode = employee.PostalCode;
            existingEmployee.Country = employee.Country;
            existingEmployee.HomePhone = employee.HomePhone;
            existingEmployee.Extension = employee.Extension;
            existingEmployee.Notes = employee.Notes;

            await _dbContext.SaveChangesAsync();
            operationResult.Succeded = true;

            return operationResult;
        }

        public async Task<OperationResult> Delete(int employeeId)
        {
            var operationResult = new OperationResult();

            var existingEmployee = await _dbContext
                .Set<Employee>()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (existingEmployee is null)
            {
                operationResult.AddError($"Employee with Id {employeeId} not found.");
                return operationResult;
            }

            _dbContext.Employees.Remove(existingEmployee);
            await _dbContext.SaveChangesAsync();
            operationResult.Succeded = true;

            return operationResult;
        }
    }
}
