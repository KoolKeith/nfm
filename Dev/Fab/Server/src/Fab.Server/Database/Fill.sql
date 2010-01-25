
-- --------------------------------------------------
-- Date Created: 01/26/2010 00:19:35
-- Create AFK user and CASH accout records
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
SET ANSI_NULLS ON;
GO

--USE [Database]
GO

INSERT INTO [dbo].[UserSet]
           ([Id]
           ,[Login]
           ,[Password]
           ,[IsDisabled])
     VALUES
           ('6184b6dd-26d0-4d06-ba2c-95c850ccfebe'
           ,'AFK'
           ,'AFK'
           ,1)
GO

INSERT INTO [dbo].[AccountSet]
           ([Name]
           ,[User_Id])
     VALUES
           ('CASH'
           ,'6184b6dd-26d0-4d06-ba2c-95c850ccfebe')
GO