CREATE TABLE [dbo].[Account]
(
	[Username] NVARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL, 
    [AccountID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 

    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [EmailAddress] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(11) NULL, 

)
