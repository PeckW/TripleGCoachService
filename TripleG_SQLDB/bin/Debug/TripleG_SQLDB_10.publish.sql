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
PRINT N'Altering [dbo].[Coach]...';


GO
ALTER TABLE [dbo].[Coach] ALTER COLUMN [JourneyTime] NVARCHAR (10) NULL;


GO
PRINT N'Update complete.';


GO
