USE [Sales]
GO
/****** Object:  StoredProcedure [marketplace].[WriteOrderProduct]    Script Date: 5/06/2023 9:46:45 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [marketplace].[WriteOrderProduct]
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
		DELETE FROM [marketPlace].[OrderProduct] WHERE ProductId = @ProductId AND OrderId = @OrderId;
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
		ON (target.ProductId = source.ProductId AND target.OrderId = source.OrderId)

		WHEN MATCHED THEN
			UPDATE SET
				target.Qty = source.Qty,
				target.UnitPrice = source.UnitPrice,
				target.TotalPrice = source.TotalPrice

		WHEN NOT MATCHED THEN
			INSERT (ProductId, OrderId, Qty, UnitPrice, TotalPrice)
			VALUES (source.ProductId, source.OrderId, source.Qty, source.UnitPrice, source.TotalPrice);
	END

END

