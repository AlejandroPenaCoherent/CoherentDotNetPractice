using DotNetPractice.Backend.Models;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly NorthwindContext _dbContext;

        public CustomersService(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResultWithData<Customer>> Create(Customer customer)
        {
            var operationResult = new OperationResultWithData<Customer>();

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            operationResult.Succeded = true;
            operationResult.Data = customer;

            return operationResult;
        }

        public async Task<OperationResultWithData<Customer>> Read(string customerId)
        {
            var operationResult = new OperationResultWithData<Customer>();

            var customer = await _dbContext
                .Set<Customer>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer != null)
            {
                operationResult.Data = customer;
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Update(Customer customer)
        {
            var operationResult = new OperationResult();

            var existingCustomer = await _dbContext
                .Set<Customer>()
                .FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);

            if (existingCustomer is null)
            {
                operationResult.AddError($"Customer with Id {customer.CustomerId} not found.");
                return operationResult;
            }

            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.ContactName = customer.ContactName;
            existingCustomer.ContactTitle = customer.ContactTitle;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.Region = customer.Region;
            existingCustomer.PostalCode = customer.PostalCode;
            existingCustomer.Country = customer.Country;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Fax = customer.Fax;

            await _dbContext.SaveChangesAsync();
            operationResult.Succeded = true;

            return operationResult;
        }

        public async Task<OperationResult> Delete(string customerId)
        {
            var operationResult = new OperationResult();

            var existingCustomer = await _dbContext
                .Set<Customer>()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (existingCustomer is null)
            {
                operationResult.AddError($"Customer with Id {customerId} not found.");
                return operationResult;
            }

            _dbContext.Customers.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();
            operationResult.Succeded = true;

            return operationResult;
        }
    }
}
