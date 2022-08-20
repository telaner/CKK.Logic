USE [StructuredProjectDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 3/24/2022 7:38:49 PM 
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
[CustomerId] [int] NOT NULL,
[Address] [nvarchar](max) NULL,
[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/24/2022 7:38:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
[OrderId] [int] IDENTITY(1,1) NOT NULL,
[OrderNumber] [varchar](50) NOT NULL,
[ShoppingCartId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/24/2022 7:38:49 PM 
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Price] [decimal](18, 2) NOT NULL,
[Quantity] [int] NOT NULL,
[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShopingCarts]    Script Date: 3/24/2022 7:38:49 PM 
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
[ShoppingCartId] [int] IDENTITY(1,1) NOT NULL,
[CustomerId] [int] NOT NULL,
[ProductId] [int] NULL,
[Quantity] [int] NULL,
 CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY CLUSTERED 
(
[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_ShoppingCartId]    Script Date: 3/24/2022 7:38:49
PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_ShoppingCartId] ON [dbo].[Orders]
(
[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, 
DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON 
[PRIMARY]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT 
[FK_Customers_ShoppingCarts_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[ShoppingCarts] ([ShoppingCartId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT 
[FK_Customers_ShoppingCarts_CustomerId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT 
[FK_Orders_ShoppingCarts_ShoppingCartId] FOREIGN KEY([ShoppingCartId])
REFERENCES [dbo].[ShoppingCarts] ([ShoppingCartId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT 
[FK_Orders_ShoppingCarts_ShoppingCartId]
GO
USE [master]
GO
ALTER DATABASE [StructuredProjectDB] SET  READ_WRITE 
GO