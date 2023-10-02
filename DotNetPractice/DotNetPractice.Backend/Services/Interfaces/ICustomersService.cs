using DotNetPractice.Backend.Models;
using DotNetPractice.DataModel;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services.Interfaces
{
    public interface ICustomersService
    {
        public Task<OperationResultWithData<Customer>> Create(Customer customer);
        public Task<OperationResultWithData<Customer>> Read(string customerId);
        public Task<OperationResult> Update(Customer customer);
        public Task<OperationResult> Delete(string customerId);
    }
}
