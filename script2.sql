USE [master]
GO
/****** Object:  Database [Capital Market]    Script Date: 8/15/2018 5:35:55 PM ******/
CREATE DATABASE [Capital Market]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Capital Market', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Capital Market.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Capital Market_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Capital Market_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Capital Market] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Capital Market].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Capital Market] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Capital Market] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Capital Market] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Capital Market] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Capital Market] SET ARITHABORT OFF 
GO
ALTER DATABASE [Capital Market] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Capital Market] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Capital Market] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Capital Market] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Capital Market] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Capital Market] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Capital Market] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Capital Market] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Capital Market] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Capital Market] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Capital Market] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Capital Market] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Capital Market] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Capital Market] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Capital Market] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Capital Market] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Capital Market] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Capital Market] SET RECOVERY FULL 
GO
ALTER DATABASE [Capital Market] SET  MULTI_USER 
GO
ALTER DATABASE [Capital Market] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Capital Market] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Capital Market] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Capital Market] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Capital Market] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Capital Market', N'ON'
GO
ALTER DATABASE [Capital Market] SET QUERY_STORE = OFF
GO
USE [Capital Market]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Capital Market]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[MobileNo] [varchar](500) NOT NULL,
	[Email] [varchar](500) NOT NULL,
	[LastName] [varchar](500) NOT NULL,
	[UserName] [varchar](500) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[Answer] [varchar](6500) NOT NULL,
	[IsLocked] [int] NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[UserTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ClientID] [int] NOT NULL,
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderQuantity] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderType] [int] NOT NULL,
	[OrderPrice] [float] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResetPasswordRequest]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResetPasswordRequest](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RequestDate] [date] NOT NULL,
	[Flag] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_ResetPasswordRequest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[CompanyName] [varchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [varchar](50) NULL,
	[CompanyNameAr] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Order_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[ResetPasswordRequest] ADD  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Question] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserTypes] ([ID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Question]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Question1] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([ID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Question1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Client]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Stock]
GO
/****** Object:  StoredProcedure [dbo].[GetClient]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetClient] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


SELECT
      [dbo].[Client].[name], b.Ordq , b.totalvalue 

from
      [dbo].[Client]inner join(Select [ClientID] ,count([OrderQuantity]) as Ordq, sum([OrderCost]) as totalvalue from [dbo].[Orders] group by [ClientID] ) b

	  on [dbo].[Client].[ID]= b.ClientID
where
     
	  b.Ordq>5 and b.totalvalue>5000
    
	  


END
GO
/****** Object:  StoredProcedure [dbo].[GetMostFrequentCompanyByOrders]    Script Date: 8/15/2018 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Procedure [dbo].[GetMostFrequentCompanyByOrders]
as
SELECT top 1 [CompanyOfStock], count([OrderQuantity]) as Quantity
       
	   From 
	       [dbo].[Orders]

     group by [CompanyOfStock] Order by Quantity Desc


	 
GO
USE [master]
GO
ALTER DATABASE [Capital Market] SET  READ_WRITE 
GO
