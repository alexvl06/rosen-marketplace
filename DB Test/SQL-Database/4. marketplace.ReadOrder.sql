Use Sales
GO

CREATE OR ALTER PROCEDURE marketplace.ReadOrder
    @FilterXml xml,
    @DataTablesXml xml,
    @Language varchar(2) = 'EN'
AS

-- Validaciones

-- Lectura parametros
DECLARE @customerId int
DECLARE @orderId int
DECLARE @status NVARCHAR(20)
DECLARE @TableData TABLE(TableName VARCHAR(50));

SELECT @customerId = @FilterXml.value('(/ROOT/Filter/@CustomerId)[1]', 'int');
SELECT @orderId = @FilterXml.value('(/ROOT/Filter/@OrderId)[1]', 'int');
SELECT @status = @FilterXml.value('(/ROOT/Filter/@Status)[1]', 'NVARCHAR(20)');
INSERT INTO @TableData (TableName)
SELECT DataTable.value('@TableName', 'VARCHAR(50)') AS TableName 
FROM @DataTablesXml.nodes('/ROOT/DataTable') AS T(DataTable);

-- 'Order-Header'
IF EXISTS ( SELECT 1
            FROM   @TableData
            WHERE  TableName = 'Order-Header' )
BEGIN
    SELECT TableName = 'Order-Header';

    SELECT
               O.OrderId,
               O.CustomerId,
               C.CustomerName,
               O.OrderDate,
               O.Status
    FROM		[marketplace].[Order] AS O Join [marketplace].[Customer] AS C
		ON O.CustomerId = C.CustomerId
	WHERE		(O.CustomerId = @customerId OR @customerId IS NULL) AND
				(O.OrderId = @orderId OR @orderId IS NULL)  AND
				(O.Status = @status OR @status IS NULL);
END;

-- 'Order-Detail'
IF EXISTS ( SELECT 1
            FROM   @TableData
            WHERE  TableName = 'Order-Detail' )
BEGIN
    SELECT TableName = 'Order-Detail';
    SELECT
               OP.OrderId,
               OP.ProductId,
               OP.Qty,
               OP.UnitPrice,
               OP.TotalPrice
    FROM	 [marketplace].[OrderProduct] AS OP Join [marketplace].[Order] AS O
			ON O.OrderId = OP.OrderId
	WHERE		(O.CustomerId = @customerId OR @customerId IS NULL) AND
				(O.OrderId = @orderId OR @orderId IS NULL)  AND
				(O.Status = @status OR @status IS NULL);
END;

-- 'Order-History-Json'
IF EXISTS ( SELECT 1
            FROM   @TableData
            WHERE  TableName = 'Order-History-Json' )
BEGIN
    SELECT TableName = 'Order-History-Json';

	SELECT
               O.OrderId AS _id,
               O.OrderDate,
               O.CustomerId,
               C.CustomerName,
               L.LocationName AS CustomerLocationCity,
			   (
				SELECT  CONCAT(L3.LocationName, ' | ',L2,' | ',L1)
				FROM
				(
					SELECT L2.LocationName AS L2, L1, L2.ParentLocationId
					FROM
					(
						SELECT L1.LocationName AS L1, L1.ParentLocationId 
						FROM [marketplace].Location AS L1 WHERE L1.LocationId = L.LocationId
					) 
					AS L1 JOIN [marketplace].Location AS L2 ON L1.ParentLocationId = L2.LocationId
				) AS L2 JOIN [marketplace].Location AS L3 ON L2.ParentLocationId = L3.LocationId
			   ) AS CustomerLocationHierarchy,
			   C.Address AS CustomerAddress,
			   (
				SELECT OP.ProductId, OP.Qty, OP.UnitPrice, OP.TotalPrice
				FROM [marketplace].OrderProduct OP
				WHERE O.OrderId = OP.OrderId
				FOR JSON PATH
			   ) AS Products,
			   (SELECT COUNT(*) FROM [marketplace].OrderProduct  WHERE OrderId = O.OrderId) AS ProductsCount,
			   (SELECT SUM(OP.TotalPrice) FROM [marketplace].OrderProduct AS OP WHERE OrderId = O.OrderId) AS TotalOrder
    FROM	[marketplace].[Order] AS O Join [marketplace].[Customer] AS C
			ON O.CustomerId = C.CustomerId Join [marketplace].[Location] AS L
			ON C.LocationId = L.LocationId
	WHERE		(O.CustomerId = @customerId OR @customerId IS NULL) AND
				(O.OrderId = @orderId OR @orderId IS NULL)  AND
				(O.Status = @status OR @status IS NULL)
	FOR JSON PATH;
END;
GO

/*
-------------------- By OrderId
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

-------------------- By CustomerId and Order in OPEN Status
DECLARE
    @FilterXml xml = '
	<ROOT>
		<Filter CustomerId="1" Status="OPEN"/>
	</ROOT>',
    @DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-Header"/>
		<DataTable TableName="Order-Detail"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml;
*/

-------------------- Order-History by CustomerId 

/*
DECLARE
    @FilterXml xml = '
	<ROOT>
		<!-- <Filter CustomerId="1"/> -->
		<Filter CustomerId="-1" />
	</ROOT>',
    @DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-History-Json"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml,
    @Language = 'DE';

	*/
