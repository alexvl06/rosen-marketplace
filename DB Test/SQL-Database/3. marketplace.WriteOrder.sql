Use Sales
GO

CREATE OR ALTER PROCEDURE marketplace.WriteOrder
    @OrderId int = NULL OUTPUT,
    @Delete bit = 0,
    @CustomerId int = NULL,
    @OrderDate datetime = NULL,
    @Status varchar(20) = NULL
AS
BEGIN
-- Validaciones

-- CUD using MERGE statement
IF @Delete !=0
	BEGIN
		DELETE FROM [marketPlace].[Order] WHERE OrderId= @OrderId;
	END
	ELSE
	BEGIN
		MERGE [marketPlace].[Order] As target
		USING(
			SELECT @OrderId AS OrderId, 
			@CustomerId AS CustomerId,
			@OrderDate AS OrderDate,
			@Status AS Status
		) AS source
		ON (target.OrderId = source.OrderId)

		WHEN MATCHED THEN
			UPDATE SET
				target.CustomerId = source.CustomerId,
				target.OrderDate = source.OrderDate,
				target.Status = source.Status

		WHEN NOT MATCHED THEN
			INSERT (OrderId, CustomerId, OrderDate, Status)
			VALUES (source.OrderId, source.CustomerId, source.OrderDate, source.Status);
	END
END
GO

