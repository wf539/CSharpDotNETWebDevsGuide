/****** Object:  Database shopDb    Script Date: 9/30/2001 10:54:42 AM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'shopDb')
	DROP DATABASE [shopDb]
GO

CREATE DATABASE [shopDb]  ON (NAME = N'shopDb_dat', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL\data\shopDb.mdf' , SIZE = 2, FILEGROWTH = 10%) LOG ON (NAME = N'shopDb_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL\data\shopDb.ldf' , SIZE = 2, FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'shopDb', N'autoclose', N'false'
GO

exec sp_dboption N'shopDb', N'bulkcopy', N'true'
GO

exec sp_dboption N'shopDb', N'trunc. log', N'true'
GO

exec sp_dboption N'shopDb', N'torn page detection', N'true'
GO

exec sp_dboption N'shopDb', N'read only', N'false'
GO

exec sp_dboption N'shopDb', N'dbo use', N'false'
GO

exec sp_dboption N'shopDb', N'single', N'false'
GO

exec sp_dboption N'shopDb', N'autoshrink', N'false'
GO

exec sp_dboption N'shopDb', N'ANSI null default', N'false'
GO

exec sp_dboption N'shopDb', N'recursive triggers', N'false'
GO

exec sp_dboption N'shopDb', N'ANSI nulls', N'false'
GO

exec sp_dboption N'shopDb', N'concat null yields null', N'false'
GO

exec sp_dboption N'shopDb', N'cursor close on commit', N'false'
GO

exec sp_dboption N'shopDb', N'default to local cursor', N'false'
GO

exec sp_dboption N'shopDb', N'quoted identifier', N'false'
GO

exec sp_dboption N'shopDb', N'ANSI warnings', N'false'
GO

exec sp_dboption N'shopDb', N'auto create statistics', N'true'
GO

exec sp_dboption N'shopDb', N'auto update statistics', N'true'
GO

use [shopDb]
GO

/****** Object:  Stored Procedure dbo.GetAllBooks    Script Date: 9/30/2001 10:54:45 AM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetAllBooks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetAllBooks]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 9/30/2001 10:54:45 AM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Books]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Books]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 9/30/2001 10:54:46 AM ******/
CREATE TABLE [dbo].[Books] (
	[BK_ISBN] [char] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BK_Author] [char] (80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[BK_Price] [money] NULL ,
	[BK_Title] [char] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[BK_Description] [char] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[BK_ImagePath] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.GetAllBooks    Script Date: 9/30/2001 10:54:48 AM ******/


CREATE   PROCEDURE GetAllBooks

AS

SELECT BK_ISBN isbn, BK_ImagePath imgSrc, BK_author author, BK_Price price, BK_Title title, BK_Description "description"
FROM Books book 

ORDER BY "title"





GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

