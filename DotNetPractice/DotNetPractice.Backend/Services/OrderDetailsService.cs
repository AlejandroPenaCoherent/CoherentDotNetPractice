using DotNetPractice.Backend.Models;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataAccess.Models.Common;
using DotNetPractice.DataAccess;
using DotNetPractice.DataModel;
using System.Threading.Tasks;
using System.Linq;

namespace DotNetPractice.Backend.Services
{
    public class OrderDetailsService: IOrderDetailsService
    {
        private readonly NorthwindDapperContext _dbContext;

        public OrderDetailsService(NorthwindDapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResultWithData<OrderDetail>> Create(OrderDetail orderDetail)
        {
            var operationResult = new OperationResultWithData<OrderDetail>();

            var parameters = new
            {
                orderDetail.OrderId,
                orderDetail.ProductId,
                orderDetail.UnitPrice,
                orderDetail.Quantity,
                orderDetail.Discount
            };

            var numberOfRowsAffected = await _dbContext.ExecuteAsync(StoredProcedures.ORDER_DETAILS_INSERT, parameters: parameters);

            if (numberOfRowsAffected > 0)
            {
                operationResult.Data = orderDetail;
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResultWithData<OrderDetail>> Read(int orderId, int productId)
        {
            var operationResult = new OperationResultWithData<OrderDetail>();

            var orderDetail = await _dbContext.GetAsync<OrderDetail>(StoredProcedures.ORDER_DETAILS_GET, parameters: new { OrderId = orderId, ProductId = productId });

            if (orderDetail != null && orderDetail.Any())
            {
                operationResult.Data = orderDetail.First();
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Update(OrderDetail orderDetail)
        {
            var operationResult = new OperationResult();

            var parameters = new
            {
                orderDetail.OrderId,
                orderDetail.ProductId,
                orderDetail.UnitPrice,
                orderDetail.Quantity,
                orderDetail.Discount
            };

            var numberOfRowsAffected = await _dbContext.ExecuteAsync(StoredProcedures.ORDER_DETAILS_UPDATE, parameters);

            if (numberOfRowsAffected > 0)
            {
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Delete(int orderId, int productId)
        {
            var operationResult = new OperationResult();

            var numberOfRowsAffected = await _dbContext.ExecuteAsync(StoredProcedures.ORDER_DETAILS_DELETE, new { OrderId = orderId, ProductId = productId });

            if (numberOfRowsAffected > 0)
            {
                operationResult.Succeded = true;
            }

            return operationResult;
        }
    }
}
