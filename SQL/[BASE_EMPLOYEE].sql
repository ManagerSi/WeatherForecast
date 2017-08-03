USE [ManagerSi]
GO

/****** Object:  Table [dbo].[BASE_EMPLOYEE]    Script Date: 2017/4/22 22:47:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BASE_EMPLOYEE](
	[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[EmpType] [decimal](18, 0) NULL,
	[Name] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[State] [char](1) NULL,
	[CreateTime] [datetime] NULL,
	[Mobile] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Sex] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateID] [decimal](18, 0) NULL,
 CONSTRAINT [PK_BASE_EMPLOYEE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


