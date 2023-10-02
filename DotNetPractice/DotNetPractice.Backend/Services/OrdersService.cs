using DotNetPractice.Backend.Models;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataAccess;
using DotNetPractice.DataAccess.Models.Common;
using DotNetPractice.DataModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly NorthwindDapperContext _dbContext;

        public OrdersService(NorthwindDapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResultWithData<Order>> Create(Order order)
        {
            var operationResult = new OperationResultWithData<Order>();

            var parameters = new
            {
                order.CustomerId,
                order.EmployeeId,
                order.OrderDate,
                order.RequiredDate,
                order.ShippedDate,
                order.ShipVia,
                order.Freight,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipRegion,
                order.ShipPostalCode,
                order.ShipCountry
            };

            var newOrderId = (await _dbContext.GetAsync<int>(StoredProcedures.ORDERS_INSERT, parameters: parameters))
                .FirstOrDefault();

            order.OrderId = newOrderId;

            operationResult.Data = order;
            operationResult.Succeded = true;

            return operationResult;
        }

        public async Task<OperationResultWithData<Order>> Read(int orderId)
        {
            var operationResult = new OperationResultWithData<Order>();

            var order = await _dbContext.GetAsync<Order>(StoredProcedures.ORDERS_GET, parameters: new { OrderId = orderId });

            if (order != null && order.Any())
            {
                operationResult.Data = order.First();
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Update(Order order)
        {
            var operationResult = new OperationResult();

            var parameters = new
            {
                order.OrderId,
                order.CustomerId,
                order.EmployeeId,
                order.OrderDate,
                order.RequiredDate,
                order.ShippedDate,
                order.ShipVia,
                order.Freight,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipRegion,
                order.ShipPostalCode,
                order.ShipCountry
            };

            var numberOfRowsAffected = await _dbContext.ExecuteAsync(StoredProcedures.ORDERS_UPDATE, parameters);

            if (numberOfRowsAffected > 0)
            {
                operationResult.Succeded = true;
            }

            return operationResult;
        }

        public async Task<OperationResult> Delete(int orderId)
        {
            var operationResult = new OperationResult();

            var numberOfRowsAffected = await _dbContext.ExecuteAsync(StoredProcedures.ORDERS_DELETE, new { OrderId = orderId });

            if (numberOfRowsAffected > 0)
            {
                operationResult.Succeded = true;
            }

            return operationResult;
        }
    }
}
