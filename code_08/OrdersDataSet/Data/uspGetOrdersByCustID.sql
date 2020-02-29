CREATE PROCEDURE uspGetOrdersByCustID(
	@sCustID NCHAR(5)
)
AS
SELECT OrderID
, EmployeeID
, OrderDate
, RequiredDate
, ShippedDate
, ShipVia
FROM Orders
WHERE CustomerID = @sCustID
