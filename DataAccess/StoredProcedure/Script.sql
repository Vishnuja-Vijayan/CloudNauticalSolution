IF OBJECT_ID('dbo.sp_GetLatestOrder', 'P') IS NOT NULL  
    DROP PROCEDURE dbo.sp_GetLatestOrder;  
GO  

CREATE PROCEDURE dbo.sp_GetLatestOrder
    @CustomerId Varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        C.FirstName, 
        C.LastName, 
        C.Email,
        O.OrderID, 
        O.OrderDate, 
        O.DeliveryExpected, 
        O.ContainsGift,
        CONCAT(C.HouseNo, ' ', C.Street, ', ', C.Town, ', ', C.PostCode) AS DeliveryAddress,
        CASE 
            WHEN O.ContainsGift = 1 THEN 'Gift' 
            ELSE P.ProductName 
        END AS ProductName,
        OI.Quantity, 
        OI.PriceEach
    FROM Customers C
    LEFT JOIN Orders O ON C.CustomerId = O.CustomerId
    LEFT JOIN OrderItems OI ON O.OrderID = OI.OrderID
    LEFT JOIN Products P ON OI.ProductID = P.ProductID
    WHERE C.CustomerId = @CustomerId
    ORDER BY O.OrderDate DESC;
END;
GO