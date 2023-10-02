
CREATE PROCEDURE [dbo].[spOrders_Get]
@OrderId INT = NULL
AS
BEGIN

	IF @OrderId is null
	BEGIN
		SELECT
		*
		FROM [dbo].Orders;
	END
	ELSE
	BEGIN
		SELECT
		*
		FROM [dbo].Orders
		WHERE OrderID = @OrderId;
	END

END
GO

CREATE PROCEDURE [dbo].[spOrders_Insert]
@CustomerID nchar(5),
@EmployeeID int,
@OrderDate datetime,
@RequiredDate datetime,
@ShippedDate datetime,
@ShipVia int,
@Freight money,
@ShipName nvarchar(40),
@ShipAddress nvarchar(60),
@ShipCity nvarchar(15),
@ShipRegion nvarchar(15),
@ShipPostalCode nvarchar(10),
@ShipCountry nvarchar(15)
AS
BEGIN

	INSERT INTO Orders (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry)
    VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry);
    SELECT CAST(SCOPE_IDENTITY() as int);

END
GO

CREATE PROCEDURE [dbo].[SpOrders_Update]
@OrderId int,
@CustomerID nchar(5),
@EmployeeID int,
@OrderDate datetime,
@RequiredDate datetime,
@ShippedDate datetime,
@ShipVia int,
@Freight money,
@ShipName nvarchar(40),
@ShipAddress nvarchar(60),
@ShipCity nvarchar(15),
@ShipRegion nvarchar(15),
@ShipPostalCode nvarchar(10),
@ShipCountry nvarchar(15)
AS
BEGIN

	UPDATE [dbo].[Orders] SET
	CustomerID = @CustomerID,
	EmployeeID = @EmployeeID,
	OrderDate = @OrderDate,
	RequiredDate = @RequiredDate,
	ShippedDate = @ShippedDate,
	ShipVia = @ShipVia,
	Freight = @Freight,
	ShipName = @ShipName,
	ShipAddress = @ShipAddress,
	ShipCity = @ShipCity,
	ShipRegion = @ShipRegion,
	ShipPostalCode = @ShipPostalCode,
	ShipCountry = @ShipCountry
	WHERE OrderID = @OrderId;

END
GO

CREATE PROCEDURE [dbo].[SpOrders_Delete]
@OrderId int
AS
BEGIN
	DELETE FROM [dbo].[Orders] WHERE OrderID = @OrderId;
END
GO

CREATE PROCEDURE [dbo].[spOrderDetails_Insert]
@OrderID int,
@ProductID int,
@UnitPrice money,
@Quantity smallint,
@Discount real
AS
BEGIN

	INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity, Discount)
    VALUES (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount);

END
GO

CREATE PROCEDURE [dbo].[spOrderDetails_Get]
@OrderId int = null,
@ProductId int = null
AS
BEGIN

	IF @OrderId IS NOT NULL AND @ProductId IS NOT NULL
	BEGIN
		SELECT * FROM [Order Details]
		WHERE OrderID = @OrderId AND ProductID = @ProductId;
	END
	ELSE
	BEGIN
		SELECT * FROM [Order Details];
	END

END
GO

CREATE PROCEDURE [dbo].[SpOrderDetails_Update]
@OrderID int,
@ProductID int,
@UnitPrice money,
@Quantity smallint,
@Discount real
AS
BEGIN

	UPDATE [dbo].[Order Details] SET
	OrderID = @OrderID,
	ProductID = @ProductID,
	UnitPrice = @UnitPrice,
	Quantity = @Quantity,
	Discount = @Discount
	WHERE OrderID = @OrderId AND ProductID = @ProductID;

END
GO

CREATE PROCEDURE [dbo].[SpOrderDetails_Delete]
@OrderId int,
@ProductId int
AS
BEGIN
	DELETE FROM [dbo].[Order Details] WHERE OrderID = @OrderId AND ProductID = @ProductId;
END
GO