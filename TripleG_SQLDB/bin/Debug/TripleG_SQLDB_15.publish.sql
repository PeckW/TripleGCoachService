﻿/*
Deployment script for TripleG_SQLDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TripleG_SQLDB"
:setvar DefaultFilePrefix "TripleG_SQLDB"
:setvar DefaultDataPath "C:\Users\William\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\William\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Account].[AccountID] is being dropped, data loss could occur.

The column Username on table [dbo].[Account] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Account])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key 597b70de-7b31-48be-aea5-368e6ec84b6f, ad2275dd-3f4f-4c24-8f9d-6ca6519dd1f8 is skipped, element [dbo].[Account].[AccountID] (SqlSimpleColumn) will not be renamed to Password';


GO
PRINT N'Starting rebuilding table [dbo].[Account]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Account] (
    [Username] NVARCHAR (50) NOT NULL,
    [Password] VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Username] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Account])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Account] ([Username], [Password])
        SELECT   [Username],
                 [Password]
        FROM     [dbo].[Account]
        ORDER BY [Username] ASC;
    END

DROP TABLE [dbo].[Account];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Account]', N'Account';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '597b70de-7b31-48be-aea5-368e6ec84b6f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('597b70de-7b31-48be-aea5-368e6ec84b6f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ad2275dd-3f4f-4c24-8f9d-6ca6519dd1f8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ad2275dd-3f4f-4c24-8f9d-6ca6519dd1f8')

GO

GO
PRINT N'Update complete.';


GO