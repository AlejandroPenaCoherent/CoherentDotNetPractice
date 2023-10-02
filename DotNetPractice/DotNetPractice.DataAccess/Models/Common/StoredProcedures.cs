namespace DotNetPractice.DataAccess.Models.Common
{
    public static class StoredProcedures
    {
        // orders procedures
        public const string ORDERS_GET = "[dbo].[spOrders_Get]";
        public const string ORDERS_INSERT = "[dbo].[spOrders_Insert]";
        public const string ORDERS_UPDATE = "[dbo].[SpOrders_Update]";
        public const string ORDERS_DELETE = "[dbo].[SpOrders_Delete]";

        // order details procedures
        public const string ORDER_DETAILS_GET = "[dbo].[spOrderDetails_Get]";
        public const string ORDER_DETAILS_INSERT = "[dbo].[spOrderDetails_Insert]";
        public const string ORDER_DETAILS_UPDATE = "[dbo].[spOrderDetails_Update]";
        public const string ORDER_DETAILS_DELETE = "[dbo].[spOrderDetails_Delete]";
    }
}
