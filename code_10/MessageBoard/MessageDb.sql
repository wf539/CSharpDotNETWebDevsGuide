/****** Object:  Database MessageDb    Script Date: 9/30/2001 7:49:20 AM ******/
CREATE DATABASE [MessageDb]  ON (NAME = N'MessageDb_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\MessageDb_Data.MDF' , SIZE = 2, FILEGROWTH = 10%) LOG ON (NAME = N'MessageDb_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\MessageDb_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
GO

exec sp_dboption N'MessageDb', N'autoclose', N'false'
GO

exec sp_dboption N'MessageDb', N'bulkcopy', N'false'
GO

exec sp_dboption N'MessageDb', N'trunc. log', N'false'
GO

exec sp_dboption N'MessageDb', N'torn page detection', N'true'
GO

exec sp_dboption N'MessageDb', N'read only', N'false'
GO

exec sp_dboption N'MessageDb', N'dbo use', N'false'
GO

exec sp_dboption N'MessageDb', N'single', N'false'
GO

exec sp_dboption N'MessageDb', N'autoshrink', N'false'
GO

exec sp_dboption N'MessageDb', N'ANSI null default', N'false'
GO

exec sp_dboption N'MessageDb', N'recursive triggers', N'false'
GO

exec sp_dboption N'MessageDb', N'ANSI nulls', N'false'
GO

exec sp_dboption N'MessageDb', N'concat null yields null', N'false'
GO

exec sp_dboption N'MessageDb', N'cursor close on commit', N'false'
GO

exec sp_dboption N'MessageDb', N'default to local cursor', N'false'
GO

exec sp_dboption N'MessageDb', N'quoted identifier', N'false'
GO

exec sp_dboption N'MessageDb', N'ANSI warnings', N'false'
GO

exec sp_dboption N'MessageDb', N'auto create statistics', N'true'
GO

exec sp_dboption N'MessageDb', N'auto update statistics', N'true'
GO

use [MessageDb]
GO

/****** Object:  Table [dbo].[Group]    Script Date: 9/30/2001 7:49:31 AM ******/
CREATE TABLE [dbo].[Group] (
	[GpID] [int] IDENTITY (1, 1) NOT NULL ,
	[GpTitle] [nvarchar] (50) NULL ,
	[GpTopic] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Moderator]    Script Date: 9/30/2001 7:49:32 AM ******/
CREATE TABLE [dbo].[Moderator] (
	[MdID] [int] IDENTITY (1, 1) NOT NULL ,
	[MdEmail] [nvarchar] (50) NULL ,
	[MdPassword] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Response]    Script Date: 9/30/2001 7:49:32 AM ******/
CREATE TABLE [dbo].[Response] (
	[RsID] [int] IDENTITY (1, 1) NOT NULL ,
	[RsName] [nvarchar] (50) NULL ,
	[RsEmail] [nvarchar] (50) NULL ,
	[RsMessage] [text] NULL ,
	[RsDate] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 9/30/2001 7:49:33 AM ******/
CREATE TABLE [dbo].[Message] (
	[MsID] [int] IDENTITY (1, 1) NOT NULL ,
	[MsName] [nvarchar] (50) NULL ,
	[MsMessage] [text] NULL ,
	[MsSubject] [nvarchar] (50) NULL ,
	[MsEmail] [nvarchar] (50) NULL ,
	[GpID] [int] NULL ,
	[Msdate] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ModeratortoGroup]    Script Date: 9/30/2001 7:49:33 AM ******/
CREATE TABLE [dbo].[ModeratortoGroup] (
	[GpID] [int] NOT NULL ,
	[MdID] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[MessagetoResponse]    Script Date: 9/30/2001 7:49:34 AM ******/
CREATE TABLE [dbo].[MessagetoResponse] (
	[RsID] [int] NOT NULL ,
	[MsID] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Group] WITH NOCHECK ADD 
	CONSTRAINT [PK_Group] PRIMARY KEY  CLUSTERED 
	(
		[GpID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Moderator] WITH NOCHECK ADD 
	CONSTRAINT [PK_Moderator] PRIMARY KEY  CLUSTERED 
	(
		[MdID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Response] WITH NOCHECK ADD 
	CONSTRAINT [PK_Response] PRIMARY KEY  CLUSTERED 
	(
		[RsID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Message] WITH NOCHECK ADD 
	CONSTRAINT [PK_Message] PRIMARY KEY  CLUSTERED 
	(
		[MsID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ModeratortoGroup] WITH NOCHECK ADD 
	CONSTRAINT [PK_ModeratortoGroup] PRIMARY KEY  CLUSTERED 
	(
		[GpID],
		[MdID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MessagetoResponse] WITH NOCHECK ADD 
	CONSTRAINT [PK_MessagetoResponse] PRIMARY KEY  CLUSTERED 
	(
		[RsID],
		[MsID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Message] ADD 
	CONSTRAINT [FK_Message_Group] FOREIGN KEY 
	(
		[GpID]
	) REFERENCES [dbo].[Group] (
		[GpID]
	)
GO

ALTER TABLE [dbo].[ModeratortoGroup] ADD 
	CONSTRAINT [FK_ModeratortoGroup_Group] FOREIGN KEY 
	(
		[GpID]
	) REFERENCES [dbo].[Group] (
		[GpID]
	),
	CONSTRAINT [FK_ModeratortoGroup_Moderator] FOREIGN KEY 
	(
		[MdID]
	) REFERENCES [dbo].[Moderator] (
		[MdID]
	)
GO

ALTER TABLE [dbo].[MessagetoResponse] ADD 
	CONSTRAINT [FK_MessagetoResponse_Message] FOREIGN KEY 
	(
		[MsID]
	) REFERENCES [dbo].[Message] (
		[MsID]
	),
	CONSTRAINT [FK_MessagetoResponse_Response] FOREIGN KEY 
	(
		[RsID]
	) REFERENCES [dbo].[Response] (
		[RsID]
	)
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.loginModerator    Script Date: 9/30/2001 7:49:34 AM ******/
create proc loginModerator
	@MdEmail nvarchar(50),
	@MdPassword nvarchar(50)

as

SELECT [MdID]
FROM Moderator
WHERE MdEmail = @MdEmail And MdPassword = @MdPassword




GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.newModerator    Script Date: 9/30/2001 7:49:34 AM ******/

CREATE  PROCEDURE [newModerator]
	(@MdEmail 	[nvarchar](50),
	 @MdPassword 	[nvarchar](50),
	 @MdID  [int] OUTPUT)

AS INSERT INTO [MessageDb].[dbo].[Moderator] 
	 ( [MdEmail],
	 [MdPassword]) 
 
VALUES 
	( @MdEmail,
	 @MdPassword)

SET @MdID = @@IDENTITY



GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.addGroup    Script Date: 9/30/2001 7:49:34 AM ******/


CREATE   PROCEDURE [addGroup]
	(@GpTitle 	[nvarchar](50),
	 @GpTopic 	[nvarchar](50),
	 @MdID	[int] )
AS


DECLARE @GpID [int]


BEGIN TRAN addGroupMod


INSERT INTO [MessageDb].[dbo].[Group] 
	 ([GpTitle],
	 [GpTopic]) 
 
VALUES 
	( @GpTitle,
	 @GpTopic)

SET @GpID = @@IDENTITY

INSERT INTO [MessageDb].[dbo].[ModeratortoGroup] 

	([GpID],
	 [MdID])

VALUES

	(@GpID,
	 @MdID)


COMMIT TRAN addGroupMod


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.addMessage    Script Date: 9/30/2001 7:49:34 AM ******/




CREATE     PROCEDURE [addMessage]
	(@MsName 	[nvarchar](50),
	 @MsMessage 	[text],
	 @MsSubject 	[nvarchar](50),
	 @MsEmail 	[nvarchar](50),
	 @GpID 	[int]  )
AS
BEGIN TRANSACTION addMess


	DECLARE @MsDate [datetime] 
	SET @MsDate=GETDATE()

 INSERT INTO [MessageDb].[dbo].[Message] 
	 ( [MsName],
	 [MsMessage],
	 [MsSubject],
	 [MsEmail],
	 [GpID],
	[MsDate]) 
 
VALUES 
	( @MsName,
	 @MsMessage,
	 @MsSubject,
	 @MsEmail,
	 @GpID,
	 @MsDate)
IF @@ERROR <> 0
	BEGIN 
		ROLLBACK TRAN addMess
		RETURN
	END
COMMIT TRANSACTION addMess


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.allGroups    Script Date: 9/30/2001 7:49:34 AM ******/

CREATE  PROC allGroups

AS

SELECT g.GpID groupid, g.GpTitle title, g.GpTopic topic, m.MdID moderatorid 
FROM [Group]  g
JOIN moderatortogroup m2g ON g.GpID = m2g.GpId
JOIN moderator m ON m2g.MdID = m.MdID 


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.addResponse    Script Date: 9/30/2001 7:49:34 AM ******/




CREATE     PROCEDURE [addResponse]
	(@MsID		[int],
	 @RsName 	[nvarchar](50),
	 @RsEmail 	[nvarchar](50),
	 @RsMessage 	[text])
AS
BEGIN TRAN addResponse
DECLARE @RsID [int]
DECLARE @RsDate [datetime]
SET @RsDate = GETDATE()





INSERT INTO [MessageDb].[dbo].[Response] 
	 ( [RsName],
	 [RsEmail],
	 [RsMessage],
	 [RsDate]) 
 
VALUES 
	( @RsName,
	 @RsEmail,
	 @RsMessage,
	 @RsDate)
	
SET @RsID = @@identity

INSERT INTO [MessageDb].[dbo].[MessagetoResponse] 
	([MsID],
	 [RsID])

VALUES

	(@MsID,
	 @RsID)
IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRAN addResponse
		RETURN
	END


COMMIT TRAN addResponse




GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.delGroup    Script Date: 9/30/2001 7:49:34 AM ******/



CREATE    PROC delGroup

	@GpID [int]

AS

	BEGIN TRAN delAllGroup	

		
		DELETE MessagetoResponse
		FROM Message m JOIN MessagetoResponse m2r ON m.MsID = m2r.MsID
		WHERE EXISTS(SELECT MsID FROM Message m WHERE m.GpID = @GpID)
				
		DELETE Response
		WHERE NOT EXISTS 
		(SELECT RsID FROM MessagetoResponse m2r
		JOIN Message m ON m2r.MsID = m.MsID
		JOIN  [Group] g ON m.GpID = @GpID)
			
		DELETE Message
		WHERE GpID = @GpID
	
		DELETE ModeratortoGroup
		FROM [Group] g JOIN ModeratortoGroup m2g ON g.GpID = m2g.GpID
		WHERE m2g.GpID = @GpID

				
		DELETE [Group]
		WHERE GpID = @GpID

		IF @@ERROR <> 0
			BEGIN
				ROLLBACK TRAN delAllGroup
				RETURN
			END

COMMIT TRAN delAllGroup			





GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.delMessage    Script Date: 9/30/2001 7:49:34 AM ******/
CREATE       PROC delMessage

	@MsID [int]

AS
BEGIN TRANSACTION delMess

	
		DELETE MessagetoResponse
		FROM Message m JOIN MessagetoResponse m2r ON m.MsID = m2r.MsID
		WHERE m2r.MsID = @MsID
				
		DELETE Response
		WHERE NOT EXISTS (SELECT RsID FROM MessagetoResponse
		WHERE MsID = @MsID)
		

		DELETE Message
		WHERE MsID = @MsID
IF @@ERROR <> 0
			BEGIN
				ROLLBACK TRAN delMess
				RETURN
			END

	
COMMIT TRANSACTION delMess











GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.delResponse    Script Date: 9/30/2001 7:49:34 AM ******/

CREATE  PROCEDURE [delResponse]
	(@RsID 	[int])

AS 
BEGIN TRANSACTION delResponse

		DELETE MessagetoResponse
		FROM Response r JOIN MessagetoResponse m2r ON r.RsID = m2r.RsID
		WHERE r.RsID = @RsID
				
		DELETE Response
		WHERE RsID = @RsID
		
IF @@ERROR <> 0
			BEGIN
				ROLLBACK TRAN delResponse
				RETURN
			END

	
COMMIT TRANSACTION delResponse


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.returnAllMess    Script Date: 9/30/2001 7:49:34 AM ******/




CREATE              PROC returnAllMess

	@GpID [int]


AS

SELECT m.MsID msgid, m.MsName owner, m.MsMessage message, m.MsSubject subject, CAST(m.MsDate AS CHAR(20)) msgdate, m.MsEmail msgemail,
	r.RsID rsgid, r.RsName rsgname,r.RsEmail rsgemail, r.RsMessage response, CAST(r.RsDate AS CHAR(20)) rsgdate
FROM Message m
LEFT JOIN MessagetoResponse m2r ON m.MsID = m2r.MsID
LEFT JOIN Response r ON m2r.RsID = r.RsID

WHERE m.GpID = @GpID 
Order BY m.MsID
FOR XML AUTO











GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

