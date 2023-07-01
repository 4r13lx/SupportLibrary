USE [Web]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SupportLibrary_Data]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Column1] [varchar](10) NOT NULL,
	[Column2] [varchar](10) NOT NULL,
	[Column3] [varchar](10) NOT NULL,
	[Column4] [varchar](10) NOT NULL,
	[Column5] [varchar](10) NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--TRUNCATE TABLE [Web].[dbo].[SupportLibrary_Data]
--GO

INSERT INTO db_name.dbo.SupportLibrary_Data (Column1, Column2, Column3, Column4, Column5) VALUES ('This', 'is', 'a', 'test', 'string.')
INSERT INTO db_name.dbo.SupportLibrary_Data (Column1, Column2, Column3, Column4, Column5) VALUES ('This', 'is', 'another', 'test', 'string.')
GO
