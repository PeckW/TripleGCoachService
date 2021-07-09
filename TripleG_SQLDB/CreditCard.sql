CREATE TABLE [dbo].[CreditCard]
(
	[CreditCardHolderName]		NVARCHAR(50)		  NULL , 
    [CreditCardNumber]			NVARCHAR(20)		  NULL, 
    [SecurityNumber]			NVARCHAR(3)			  NULL, 
    [ExpiryDate]				NVARCHAR(11)		  NULL,
    [CreditCardID]				INT IDENTITY(1,1) NOT NULL,
	[AccountID]					INT NULL 
    CONSTRAINT [PK_CreditCard] PRIMARY KEY ([CreditCardID]), 
    CONSTRAINT [FK_CreditCard_Customer] FOREIGN KEY ([AccountID]) REFERENCES [Account]([AccountID])
)
