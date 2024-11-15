USE [master]
GO
/****** Object:  Database [candle]    Script Date: 29/10/2024 20:31:16 ******/
CREATE DATABASE [candle]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'candle', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\candle.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'candle_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\candle_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [candle] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [candle].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [candle] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [candle] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [candle] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [candle] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [candle] SET ARITHABORT OFF 
GO
ALTER DATABASE [candle] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [candle] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [candle] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [candle] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [candle] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [candle] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [candle] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [candle] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [candle] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [candle] SET  ENABLE_BROKER 
GO
ALTER DATABASE [candle] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [candle] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [candle] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [candle] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [candle] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [candle] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [candle] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [candle] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [candle] SET  MULTI_USER 
GO
ALTER DATABASE [candle] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [candle] SET DB_CHAINING OFF 
GO
ALTER DATABASE [candle] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [candle] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [candle] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [candle] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [candle] SET QUERY_STORE = OFF
GO
USE [candle]
GO
/****** Object:  Table [dbo].[addresses]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[addresses](
	[address_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[address_line] [nvarchar](255) NOT NULL,
	[city] [nvarchar](255) NOT NULL,
	[postal_code] [nvarchar](20) NULL,
	[country] [nvarchar](255) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[candles]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[candles](
	[candle_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[stock_quantity] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[category_id] [int] NULL,
	[imgURL] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[candle_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[candles_img]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[candles_img](
	[candle_img_id] [int] NOT NULL,
	[candle_id] [int] NULL,
	[imgURL] [text] NULL,
 CONSTRAINT [PK_candles_img] PRIMARY KEY CLUSTERED 
(
	[candle_img_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[action] [nvarchar](max) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_items]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_items](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[candle_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[total_price] [decimal](10, 2) NOT NULL,
	[status] [nvarchar](20) NULL,
	[created_at] [datetime] NULL,
	[address] [nvarchar](250) NULL,
	[phone] [int] NULL,
	[note] [nvarchar](250) NULL,
	[isPay] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payments]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payments](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[transaction_id] [nvarchar](255) NOT NULL,
	[amount] [decimal](10, 2) NOT NULL,
	[status] [nvarchar](50) NOT NULL,
	[payment_method] [nvarchar](50) NOT NULL,
	[payment_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reviews]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reviews](
	[review_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[candle_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[comment] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[review_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[password_hash] [nvarchar](255) NOT NULL,
	[role_id] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zalopay]    Script Date: 29/10/2024 20:31:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zalopay](
	[appid] [int] NULL,
	[appuser] [nvarchar](255) NULL,
	[apptime] [bigint] NULL,
	[amount] [bigint] NULL,
	[apptransid] [nvarchar](255) NULL,
	[embeddata] [nvarchar](max) NULL,
	[mac] [nvarchar](255) NULL,
	[paymentcode] [nvarchar](255) NULL,
	[bankcode] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[returnurl] [nvarchar](255) NULL,
	[order_id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[candles] ON 

INSERT [dbo].[candles] ([candle_id], [name], [description], [price], [stock_quantity], [created_at], [updated_at], [category_id], [imgURL]) VALUES (2, N'cccc', N'ccc', CAST(123.00 AS Decimal(10, 2)), 123, NULL, NULL, NULL, N'https://res.cloudinary.com/dpbscvwv3/image/upload/v1730123988/240096506_108317671587618_8567008174005835042_n_nhk2rw.jpg')
INSERT [dbo].[candles] ([candle_id], [name], [description], [price], [stock_quantity], [created_at], [updated_at], [category_id], [imgURL]) VALUES (3, N'chinese', N'nen trung', CAST(10000.00 AS Decimal(10, 2)), 12, CAST(N'2022-12-12T00:00:00.000' AS DateTime), NULL, 2, NULL)
INSERT [dbo].[candles] ([candle_id], [name], [description], [price], [stock_quantity], [created_at], [updated_at], [category_id], [imgURL]) VALUES (5, N'clami', N'clam clam', CAST(123.00 AS Decimal(10, 2)), 123, NULL, NULL, 2, N'https://res.cloudinary.com/dpbscvwv3/image/upload/v1730125077/7799536_ih7azw.png')
INSERT [dbo].[candles] ([candle_id], [name], [description], [price], [stock_quantity], [created_at], [updated_at], [category_id], [imgURL]) VALUES (6, N'nam', N'cc', CAST(123.00 AS Decimal(10, 2)), 123, NULL, NULL, 2, N'nam')
SET IDENTITY_INSERT [dbo].[candles] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([category_id], [name]) VALUES (1, N'outside candle')
INSERT [dbo].[category] ([category_id], [name]) VALUES (2, N'inside candle')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[order_items] ON 

INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (4, 1, 3, 21, CAST(300.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (5, 3, 2, 6, CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (6, 3, 3, 6, CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (7, 4, 3, 18, CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (9, 4, 2, 18, CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (10, 5, 2, 10, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (11, 5, 2, 10, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (12, 5, 3, 10, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (13, 7, 2, 2, CAST(20.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (14, 7, 3, 2, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (15, 7, 5, 1, CAST(50.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (16, 7, 6, 3, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (17, 7, 2, 2, CAST(20.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (18, 7, 3, 2, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (19, 7, 5, 1, CAST(50.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [candle_id], [quantity], [price]) VALUES (20, 7, 6, 3, CAST(10.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[order_items] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (1, 2, CAST(1290300.00 AS Decimal(10, 2)), N'false', CAST(N'2024-10-22T03:48:52.097' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (2, 3, CAST(2600.00 AS Decimal(10, 2)), N'false', CAST(N'2024-10-22T03:54:20.207' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (3, 1, CAST(0.00 AS Decimal(10, 2)), N'false', CAST(N'2024-10-22T04:21:13.590' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (4, 4, CAST(7200.00 AS Decimal(10, 2)), N'false', CAST(N'2024-10-22T04:42:37.650' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (5, 2, CAST(2400.00 AS Decimal(10, 2)), N'Pending', CAST(N'2024-10-28T22:03:03.897' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[orders] ([order_id], [user_id], [total_price], [status], [created_at], [address], [phone], [note], [isPay]) VALUES (7, 3, CAST(140.00 AS Decimal(10, 2)), N'Pending', CAST(N'2024-10-29T09:56:36.003' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[reviews] ON 

INSERT [dbo].[reviews] ([review_id], [user_id], [candle_id], [rating], [comment], [created_at]) VALUES (1, 1, 2, 10, N'nice 1', CAST(N'2024-10-14T13:10:01.877' AS DateTime))
SET IDENTITY_INSERT [dbo].[reviews] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [name]) VALUES (1, N'admin')
INSERT [dbo].[role] ([role_id], [name]) VALUES (2, N'staff')
INSERT [dbo].[role] ([role_id], [name]) VALUES (3, N'customer')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [username], [email], [password_hash], [role_id], [created_at]) VALUES (1, N'admin', N'admin@gmail', N'123', 1, CAST(N'2022-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[user] ([user_id], [username], [email], [password_hash], [role_id], [created_at]) VALUES (2, N'customer', N'customer', N'123', 3, CAST(N'2022-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[user] ([user_id], [username], [email], [password_hash], [role_id], [created_at]) VALUES (3, N'staff', N'staff@gmail', N'123', 2, CAST(N'2022-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[user] ([user_id], [username], [email], [password_hash], [role_id], [created_at]) VALUES (4, N'cac', N'cac', N'123', 2, CAST(N'2024-10-03T22:32:58.903' AS DateTime))
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[addresses]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[candles]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([category_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[candles_img]  WITH CHECK ADD  CONSTRAINT [FK_candles_img_candles] FOREIGN KEY([candle_id])
REFERENCES [dbo].[candles] ([candle_id])
GO
ALTER TABLE [dbo].[candles_img] CHECK CONSTRAINT [FK_candles_img_candles]
GO
ALTER TABLE [dbo].[logs]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([candle_id])
REFERENCES [dbo].[candles] ([candle_id])
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[payments]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[reviews]  WITH CHECK ADD FOREIGN KEY([candle_id])
REFERENCES [dbo].[candles] ([candle_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[reviews]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[zalopay]  WITH CHECK ADD  CONSTRAINT [FK_zalopay_orders1] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[zalopay] CHECK CONSTRAINT [FK_zalopay_orders1]
GO
USE [master]
GO
ALTER DATABASE [candle] SET  READ_WRITE 
GO
