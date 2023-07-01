SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	4r13lx
-- Create date: 2016-Aug-16
-- Description:	Test GetData
-- =============================================
CREATE PROCEDURE sp_SupportLibrary_GetData
@TextParam varchar(20),
@IntParam int = 0
AS
BEGIN
SET NOCOUNT OFF;

	-- Resulset 1
	SELECT
		d.Id,
		d.Column1,
		d.Column2,
		d.Column3,
		d.Column4,
		d.Column5
	FROM	db_name.dbo.SupportLibrary_Data d (nolock)
	WHERE	d.Id = 1

	-- Resulset 2
	SELECT
		d.Id,
		d.Column1,
		d.Column2,
		d.Column3,
		d.Column4,
		d.Column5
	FROM	db_name.dbo.SupportLibrary_Data d (nolock)
	WHERE	d.Id = 2

	-- Retorno
	RETURN 1234 -- RETURN @@IDENTITY

END
GO

GRANT EXECUTE ON db_name.dbo.sp_SupportLibrary_GetData TO [dbuser]
GO
