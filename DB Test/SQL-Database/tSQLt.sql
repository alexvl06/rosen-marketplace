USE [Sales]
GO
EXEC tSQLt.NewTestClass 'UnitTestWriteOrder';
GO
Create or alter procedure UnitTestWriteOrder.[test try to insert new order]
as
begin
	DECLARE @RC int
	DECLARE @OrderId int
	DECLARE @Delete bit
	DECLARE @CustomerId int
	DECLARE @OrderDate datetime
	DECLARE @Status varchar(20)
	DECLARE @RowCounter int
	set @RC = (SELECT TOP(1) OrderId
	FROM [Sales].[marketplace].[Order]
	order by OrderId desc);

	set @OrderId  = @RC+1;
	set @Delete  = 0;
	set @CustomerId  = 0;
	set @RowCounter = 2;
	WHILE @RowCounter >1
	BEGIN
		set @CustomerId = @CustomerId+1;
		set @RowCounter = (SELECT COUNT(*) FROM  marketplace.[Order] WHERE CustomerId = @CustomerID)

	END
	set @OrderDate = GETDATE();
	set @Status = 'CLOSE';

	EXECUTE [marketplace].[WriteOrder] 
	   @OrderId OUTPUT
	  ,@Delete
	  ,@CustomerId
	  ,@OrderDate
	  ,@Status

	set @RC = (SELECT TOP(1) OrderId
	FROM [Sales].[marketplace].[Order]
	order by OrderId desc);

		exec tSQLt.AssertEquals @OrderId, @RC;
end;
GO

exec tSQLt.Run 'UnitTestWriteOrder.[test try to insert new order]';
GO

Create or alter procedure UnitTestWriteOrder.[test try to update an order]
as
begin
	DECLARE @RC varchar(20)
	DECLARE @OrderId int
	DECLARE @Delete bit
	DECLARE @CustomerId int
	DECLARE @OrderDate datetime
	DECLARE @Status varchar(20)
	DECLARE @RowCounter int
	set @OrderId = (SELECT TOP(1) OrderId
	FROM [Sales].[marketplace].[Order]
	order by OrderId desc);
	set @Delete  = 0;
	set @CustomerId  = 0;
	set @RowCounter = 2;
	WHILE @RowCounter >1
	BEGIN
		set @CustomerId = @CustomerId+1;
		set @RowCounter = (SELECT COUNT(*) FROM  marketplace.[Order] WHERE CustomerId = @CustomerID)

	END
	set @OrderDate = GETDATE();
	set @Status = (SELECT Status FROM marketplace.[Order] WHERE CustomerId = @CustomerId);
	IF @Status ='CLOSE'
	BEGIN
		set @Status = 'OPEN'
	END
	ELSE
	BEGIN
		set @Status = 'CLOSE'
	END


	EXECUTE [marketplace].[WriteOrder] 
	   @OrderId OUTPUT
	  ,@Delete
	  ,@CustomerId
	  ,@OrderDate
	  ,@Status

	set @RC = (SELECT TOP(1) Status
	FROM [Sales].[marketplace].[Order]
	order by OrderId desc);

		exec tSQLt.AssertEquals @Status, @RC;
end;

GO

exec tSQLt.Run 'UnitTestWriteOrder.[test try to update an order]';

GO

Create or alter procedure UnitTestWriteOrder.[test try to delete an order]
as
begin
	DECLARE @RC int
	DECLARE @OrderId int
	DECLARE @Delete bit
	set @OrderId = (SELECT TOP(1) OrderId
	FROM [Sales].[marketplace].[Order]
	order by OrderId desc);
	set @Delete  = 1;


	EXECUTE [marketplace].[WriteOrder] 
	   @OrderId OUTPUT
	  ,@Delete

	set @RC = (SELECT COUNT(*)
	FROM [Sales].[marketplace].[Order]
	WHERE OrderId = @OrderId);

		exec tSQLt.AssertEquals 0, @RC;
end;

Go
exec tSQLt.Run 'UnitTestWriteOrder.[test try to delete an order]';
GO

EXEC tSQLt.NewTestClass 'UnitTestWriteOrderProduct';
GO
Create or alter procedure UnitTestWriteOrderProduct.[test try to insert new product]
as
begin
	DECLARE @RC int
	DECLARE @OrderId int
	DECLARE @ProductId int
	DECLARE @Delete bit
	DECLARE @Qty int
	DECLARE @UnitPrice money
	DECLARE @TotalPrice money
	DECLARE @RowCounter int
	DECLARE @Status varchar(20) 
	set @RC = (SELECT TOP(1) ProductId
	FROM [Sales].[marketplace].[OrderProduct]
	order by ProductId desc);
	set @Status = 'CLOSE'
	set @ProductId  = @RC+1;
	set @Delete  = 0;
	set @OrderId  = 0;
	set @RowCounter = 0;
	WHILE @Status='CLOSE'
	BEGIN
		set @OrderId = @OrderId+1;
		set @RowCounter = (SELECT COUNT(*) FROM  marketplace.[Order] WHERE OrderId = @OrderID)
		IF @RowCounter>0
		BEGIN
			SET @Status=(SELECT Status FROM  marketplace.[Order] WHERE OrderId = @OrderID)
		END
	END
	set @Qty = 2;
	set @UnitPrice = 10;
	set @TotalPrice = @Qty*@UnitPrice;

	EXECUTE @RC = [marketplace].[WriteOrderProduct] 
	   @OrderId
	  ,@ProductId
	  ,@Delete
	  ,@Qty
	  ,@UnitPrice
	  ,@TotalPrice

	set @RC = (SELECT TOP(1) ProductId
	FROM [Sales].[marketplace].[OrderProduct]
	order by ProductId desc);

		exec tSQLt.AssertEquals @ProductId, @RC;
end;
GO

exec tSQLt.Run 'UnitTestWriteOrderProduct.[test try to insert new product]';
GO

Create or alter procedure UnitTestWriteOrderProduct.[test try to update a product]
as
begin
	DECLARE @RC int
	DECLARE @OrderId int
	DECLARE @ProductId int
	DECLARE @Delete bit
	DECLARE @Qty int
	DECLARE @UnitPrice money
	DECLARE @TotalPrice money
	DECLARE @Status varchar(20)
	DECLARE @RowCounter int

	set @ProductId = (SELECT TOP(1) ProductId
	FROM [Sales].[marketplace].[OrderProduct]
	order by OrderId desc);
	set @Delete  = 0;
	set @OrderId  = 0;
	set @STATUS = 'CLOSE';
	WHILE @Status = 'CLOSE'
	BEGIN
		set @OrderId = @OrderId+1;
		set @RowCounter = (SELECT COUNT(*) FROM  marketplace.[Order] WHERE OrderId = @OrderId)
		IF @RowCounter>0
		BEGIN
			set @Status = (SELECT Status FROM  marketplace.[Order] WHERE OrderId = @OrderId)
		END
	END
	set @UnitPrice = 22;
	set @Qty = 3
	Set @TotalPrice = @Qty*@UnitPrice;


	EXECUTE [marketplace].[WriteOrderProduct] 
	   @OrderId
	  ,@ProductId
	  ,@Delete
	  ,@Qty
	  ,@UnitPrice
	  ,@TotalPrice

	set @RC = (SELECT TOP(1) TotalPrice
	FROM [Sales].[marketplace].[OrderProduct]
	Where OrderId =@OrderId and ProductId = @ProductId);

		exec tSQLt.AssertEquals @TotalPrice, @RC;
end;

GO

exec tSQLt.Run 'UnitTestWriteOrderProduct.[test try to update a product]';

GO

Create or alter procedure UnitTestWriteOrderProduct.[test try to delete a product]
as
begin
	DECLARE @RC int
	DECLARE @OrderId int
	DECLARE @ProductId int
	DECLARE @Delete bit
	set @OrderId = (SELECT TOP(1) OrderId
	FROM [Sales].[marketplace].[OrderProduct]
	order by OrderId desc);
	set @ProductId = (SELECT TOP(1) ProductId
	FROM [Sales].[marketplace].[OrderProduct]
	order by OrderId desc);
	set @Delete  = 1;


	EXECUTE [marketplace].[WriteOrderProduct] 
	   @OrderId,
	   @ProductId
	  ,@Delete

	set @RC = (SELECT COUNT(*)
	FROM [Sales].[marketplace].[OrderProduct]
	WHERE OrderId = @OrderId AND ProductId = @ProductId);

		exec tSQLt.AssertEquals 0, @RC;
end;

Go
exec tSQLt.Run 'UnitTestWriteOrderProduct.[test try to delete a product]';
exec tSQLt.RunAll

GO

DECLARE @RC int
DECLARE @FilterXml xml
DECLARE @DataTablesXml xml
DECLARE @Language varchar(2)

-- TODO: Set parameter values here.

set @FilterXml = '
	<ROOT>
		<Filter CustomerId="1" OrderId="1" Status="Close"/>
	</ROOT>
'
set @DataTablesXml = '
	<ROOT>
		<DataTable TableName = "Order-Header"/>
		<DataTable TableName = "Order-Detail"/>
		<DataTable TableName = "Order-History-Json"/>
	</ROOT>
'


EXECUTE [marketplace].[ReadOrder] 
   @FilterXml
  ,@DataTablesXml
  ,@Language
GO

/*
DECLARE
    @FilterXml xml = '
	<ROOT>
		<Filter OrderId="1"/>
	</ROOT>',
	@DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-Header"/>
		<DataTable TableName="Order-Detail"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml;
GO
*/
