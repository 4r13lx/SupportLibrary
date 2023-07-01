SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4r13lx
-- Create date: 2016-Aug-18
-- Description:	Test LogData
-- =============================================
CREATE PROCEDURE [dbo].[sp_SupportLibrary_GetLogs]
@NewerThan datetime,
@Count int
AS
BEGIN
SET NOCOUNT OFF;

	-- results
	SELECT
		base.EventID,
		base.EntityName,
		base.EntityID,
		base.[User],
		base.LogTime,
		base.[Description]
	FROM
	(	SELECT TOP (@Count) logs.*
		FROM Web.dbo.SupportLibrary_Log logs (nolock)
		WHERE logs.LogTime > @NewerThan
		ORDER BY logs.EventID DESC -- tomar los últimos registros
	) as base
	ORDER BY base.EventID

	-- clean up
	TRUNCATE TABLE db_name.dbo.SupportLibrary_Log

END
GO

GRANT EXECUTE ON db_name.dbo.sp_SupportLibrary_GetLogs TO [dbuser]
GO
