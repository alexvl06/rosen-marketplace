Use Sales
GO

CREATE OR ALTER PROCEDURE marketplace.WriteOrderProduct
    @OrderId int,
    @ProductId int = NULL,
    @Delete bit = 0,
    @Qty int = NULL,
    @UnitPrice money = NULL,
    @TotalPrice money = NULL
AS

BEGIN
-- Validaciones
	DECLARE @Status NVARCHAR(20);
	SET @Status = (SELECT Status FROM [marketplace].[Order] WHERE OrderId=@OrderId);
	IF @Status='CLOSE'
	BEGIN
		RAISERROR( 'Status is close',0, 403);
		RETURN;
	END
	IF @ProductId<=0 or @Qty<=0 or @UnitPrice<=0 or @TotalPrice<=0  
	BEGIN
		RAISERROR( 'ProductId, Qty, UnitPrice and TotalPrice must be greater than zero',
		1, 400);
		RETURN;
	END

-- CUD using MERGE statement
	IF @Delete !=0
	BEGIN
		DELETE FROM [marketPlace].[OrderProduct] WHERE ProductId = @ProductId;
	END
	ELSE
	BEGIN
		MERGE [marketPlace].[OrderProduct] As target
		USING(
			SELECT @OrderId AS OrderId, 
			@ProductId AS ProductId,
			@Qty AS Qty,
			@UnitPrice AS UnitPrice,
			@TotalPrice AS TotalPrice
		) AS source
		ON (target.ProductId = source.ProductId)

		WHEN MATCHED THEN
			UPDATE SET
				target.OrderId = source.OrderId,
				target.Qty = source.Qty,
				target.UnitPrice = source.UnitPrice,
				target.TotalPrice = source.TotalPrice

		WHEN NOT MATCHED THEN
			INSERT (ProductId, OrderId, Qty, UnitPrice, TotalPrice)
			VALUES (source.ProductId, source.OrderId, source.Qty, source.UnitPrice, source.TotalPrice);
	END

END

GO
