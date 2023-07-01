USE [Web]
GO

/****** Object:  Table [dbo].[ChargeTransfer_Log]    Script Date: 16/08/2016 5:11:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SupportLibrary_Log]
(
	[EventID] [int] IDENTITY(1,1) NOT NULL,
	[EntityName] [nvarchar](255) NOT NULL,
	[EntityID] [int] NOT NULL,
	[User] [nvarchar](255) NOT NULL,
	[LogTime] [datetime] NOT NULL,
	[Description] [nvarchar](MAX) NOT NULL,
	CONSTRAINT [PK_SupportLibrary_Log] PRIMARY KEY CLUSTERED ([EventID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

GRANT ALTER ON db_name.dbo.SupportLibrary_Log TO [dbuser]
GO
