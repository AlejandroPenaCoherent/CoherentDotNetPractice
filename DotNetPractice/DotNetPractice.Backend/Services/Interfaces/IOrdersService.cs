using DotNetPractice.Backend.Models;
using DotNetPractice.DataModel;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services.Interfaces
{
    public interface IOrdersService
    {
        public Task<OperationResultWithData<Order>> Create(Order Order);
        public Task<OperationResultWithData<Order>> Read(int orderId);
        public Task<OperationResult> Update(Order order);
        public Task<OperationResult> Delete(int orderId);
    }
}
