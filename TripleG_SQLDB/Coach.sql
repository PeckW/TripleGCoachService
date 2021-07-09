CREATE TABLE [dbo].[Coach]
(
	[CoachJourneyID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [LeavingFrom] NVARCHAR(50) NULL, 
    [ArrivalDestination] NVARCHAR(50) NULL, 
    [JourneyTime] NVARCHAR(10) NULL, 
    [JourneyDate] NVARCHAR(50) NULL, 
    [Price] NVARCHAR(50) NULL
)
