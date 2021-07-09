CREATE TABLE [dbo].[Customer]
(
    [FirstName]		NVARCHAR(50)		  NULL, 
    [LastName]		NVARCHAR(50)		  NULL, 
    [EmailAddress]  NVARCHAR(50)		  NULL, 
    [PhoneNumber]   NVARCHAR(11)		  NULL, 
    [CustomerID]    INT IDENTITY(1,1) NOT NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerID]), 
)
