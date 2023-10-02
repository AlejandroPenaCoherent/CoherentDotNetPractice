using DotNetPractice.Backend.Models;
using DotNetPractice.DataModel;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services.Interfaces
{
    public interface IOrderDetailsService
    {
        public Task<OperationResultWithData<OrderDetail>> Create(OrderDetail orderDetail);
        public Task<OperationResultWithData<OrderDetail>> Read(int orderId, int productId);
        public Task<OperationResult> Update(OrderDetail orderDetail);
        public Task<OperationResult> Delete(int orderId, int productId);
    }
}
