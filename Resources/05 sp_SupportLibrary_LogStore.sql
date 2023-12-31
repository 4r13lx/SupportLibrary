USE [Web]
GO
/****** Object:  StoredProcedure [dbo].[sp_ChargeTransfer_Log_Store]    Script Date: 16/08/2016 5:11:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	4r13lx
-- Create date: 2016-Aug-16
-- Description:	Log information from application use
-- =============================================
CREATE PROCEDURE [dbo].[sp_SupportLibrary_LogStore]
	@EntityName nvarchar(255),
	@EntityID int,
	@User nvarchar(255),
	@Description nvarchar(MAX)
AS
BEGIN

	INSERT INTO SupportLibrary_Log
	(
		[EntityName],
		[EntityID],
		[User],
		[LogTime],
		[Description]
	)
	VALUES
	(
		@EntityName,
		@EntityID,
		@User,
		GETDATE(),
		@Description
	)

END
GO

GRANT EXECUTE ON db_name.dbo.sp_SupportLibrary_LogStore TO [dbuser]
GO
