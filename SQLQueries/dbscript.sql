USE [master]
GO
/****** Object:  Database [ArtGalleryDb]    Script Date: 9/9/2024 9:55:11 AM ******/
CREATE DATABASE [ArtGalleryDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ArtGalleryDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ArtGalleryDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ArtGalleryDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ArtGalleryDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ArtGalleryDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ArtGalleryDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ArtGalleryDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ArtGalleryDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ArtGalleryDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ArtGalleryDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ArtGalleryDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET RECOVERY FULL 
GO
ALTER DATABASE [ArtGalleryDb] SET  MULTI_USER 
GO
ALTER DATABASE [ArtGalleryDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ArtGalleryDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ArtGalleryDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ArtGalleryDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ArtGalleryDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ArtGalleryDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ArtGalleryDb', N'ON'
GO
ALTER DATABASE [ArtGalleryDb] SET QUERY_STORE = OFF
GO
USE [ArtGalleryDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [uniqueidentifier] NOT NULL,
	[AppUserId] [nvarchar](450) NULL,
	[AddressLine] [nvarchar](500) NOT NULL,
	[PinCode] [nvarchar](12) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Landmark] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NOT NULL,
	[CountryCode] [nvarchar](6) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppOrder]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppOrder](
	[OrderId] [uniqueidentifier] NOT NULL,
	[AppUserId] [nvarchar](450) NULL,
	[PaymentId] [uniqueidentifier] NULL,
	[AddressId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Status] [nvarchar](30) NOT NULL,
	[CountryCode] [nvarchar](6) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[LastLoginAt] [datetime2](7) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [uniqueidentifier] NOT NULL,
	[AppUserId] [nvarchar](450) NULL,
	[ProductId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[ProductId] [uniqueidentifier] NULL,
	[Status] [nvarchar](30) NOT NULL,
	[ProductCost] [decimal](13, 3) NOT NULL,
	[TaxCost] [decimal](13, 3) NOT NULL,
	[ShippingCost] [decimal](13, 3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](13, 3) NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[CardNumber] [nvarchar](100) NOT NULL,
	[CardHolderName] [nvarchar](100) NOT NULL,
	[ExpiryDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Price] [decimal](13, 3) NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishList]    Script Date: 9/9/2024 9:55:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishList](
	[WishListId] [uniqueidentifier] NOT NULL,
	[AppUserId] [nvarchar](450) NULL,
	[ProductId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WishListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240827052059_AuthDB', N'8.0.8')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f', N'Reader', N'READER', N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c91a3a78-4cee-4fba-8f81-46bb5280a56b', N'Writer', N'WRITER', N'c91a3a78-4cee-4fba-8f81-46bb5280a56b')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0f362a5a-d778-4735-a5de-7f7a62a8fe41', N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4f3cd3ce-b56e-46b4-85c1-9d6598915c81', N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'61591961-4579-4d0a-a758-cf01782172b2', N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8bb95c21-6534-41fb-abfe-fae08ddd7a30', N'5cfe7d19-a05b-4b48-a4e4-eebd9f40529f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4f3cd3ce-b56e-46b4-85c1-9d6598915c81', N'c91a3a78-4cee-4fba-8f81-46bb5280a56b')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Status], [CountryCode], [CreatedAt], [ModifiedAt], [ModifiedBy], [LastLoginAt], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0f362a5a-d778-4735-a5de-7f7a62a8fe41', N'User', N'', N'Active', N'', CAST(N'2024-09-09T04:22:22.2087025' AS DateTime2), NULL, NULL, NULL, N'usernew@example.com', N'USERNEW@EXAMPLE.COM', N'usernew@example.com', N'USERNEW@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEI3wyfD3vpkdgVOoZwn4tNl6RyJk+FgKeqwPng5QSc6QtCBv+vXJqHmpvHopZ2eYfg==', N'XRDFEJQWUO5M47VELAU3TJ76UGC5UO6L', N'2835719e-eca9-4dbb-b0b8-c9b1ae8a1872', N'', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Status], [CountryCode], [CreatedAt], [ModifiedAt], [ModifiedBy], [LastLoginAt], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4f3cd3ce-b56e-46b4-85c1-9d6598915c81', N'admin', NULL, N'Active', NULL, CAST(N'2024-08-27T05:20:57.4051437' AS DateTime2), NULL, NULL, NULL, N'admin@galleria.com', N'ADMIN@GALLERIA.COM', N'admin@galleria.com', N'ADMIN@GALLERIA.COM', 0, N'AQAAAAIAAYagAAAAEDu8Q4GP0GuqaUblyHIavpJzOVcQUJOB8ClklKmKZtrljDyl3MuS8LSfhGSR7JwQ9w==', N'b54d2953-1390-47db-acc2-d21277aa3f03', N'7366758d-7d01-42fd-b5b1-9cc599bb3f23', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Status], [CountryCode], [CreatedAt], [ModifiedAt], [ModifiedBy], [LastLoginAt], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'61591961-4579-4d0a-a758-cf01782172b2', N'Bobby', N'', N'Active', N'', CAST(N'2024-09-09T04:22:53.6489010' AS DateTime2), NULL, NULL, NULL, N'Bob@example.com', N'BOB@EXAMPLE.COM', N'Bob@example.com', N'BOB@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEL0ozCm89EMHyj+fxnvriZp836h4n6bmlR8C9B8eKKsQlfKKnvsiA6ZjaDnjXjP1fg==', N'4BISDKKITZOJROWZ5RPRJYZR4OJ5RIYD', N'90b6a031-b657-4fc0-a2c3-964ceb9c2cef', N'', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Status], [CountryCode], [CreatedAt], [ModifiedAt], [ModifiedBy], [LastLoginAt], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8bb95c21-6534-41fb-abfe-fae08ddd7a30', N'Alice', N'', N'Active', N'', CAST(N'2024-09-09T04:22:37.3596987' AS DateTime2), NULL, NULL, NULL, N'alice@example.com', N'ALICE@EXAMPLE.COM', N'alice@example.com', N'ALICE@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEDaf+b45pnom3Xak8pzrAIPVIgC4EWaW/TCeMNNXysmq3nVeA5O1H6nFM2KSDr9Jig==', N'QAIELWQ6VV2MMODPEFEHLF4Y2P3JOKXA', N'e11c1146-aa95-45e4-a907-8764ee79ef04', N'', 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'687cefd1-bb59-4781-cf23-08dcd085e6c7', N'Doodling', N'Doodle Art', CAST(N'2024-09-09T04:14:30.0553494' AS DateTime2), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'ad9e6d54-4e8e-4b29-cf24-08dcd085e6c7', N'Pastels', N'Art with pastels', CAST(N'2024-09-09T04:14:45.1753203' AS DateTime2), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'bc356fd1-a672-4d9e-cf25-08dcd085e6c7', N'Black and White', N'Art with pencil', CAST(N'2024-09-09T04:15:08.9046191' AS DateTime2), NULL, NULL)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'519d033e-01e5-4120-cf26-08dcd085e6c7', N'Sculpture', N'sculptures', CAST(N'2024-09-09T04:15:21.7378224' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[Product] ([ProductId], [Name], [Description], [ImageUrl], [Price], [Status], [CategoryId], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'0fc555d1-833e-466b-0404-08dcd086a513', N'Black and White Art', N'Leaves of a Plant', N'https://images.pexels.com/photos/15838443/pexels-photo-15838443.jpeg?cs=srgb&dl=pexels-juairiaa-15838443.jpg&fm=jpg', CAST(900.000 AS Decimal(13, 3)), N'In Stock', N'bc356fd1-a672-4d9e-cf25-08dcd085e6c7', CAST(N'2024-09-09T04:19:49.3326378' AS DateTime2), NULL, NULL)
INSERT [dbo].[Product] ([ProductId], [Name], [Description], [ImageUrl], [Price], [Status], [CategoryId], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'f0a9cff9-7619-45d0-0405-08dcd086a513', N'Oil Pastel', N'Pastel Art', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTfuaJaNfAcGAfJa9h2Rrpbsy5SgYKawLycag&s', CAST(870.000 AS Decimal(13, 3)), N'In Stock', N'ad9e6d54-4e8e-4b29-cf24-08dcd085e6c7', CAST(N'2024-09-09T04:20:53.9250404' AS DateTime2), NULL, NULL)
INSERT [dbo].[Product] ([ProductId], [Name], [Description], [ImageUrl], [Price], [Status], [CategoryId], [CreatedAt], [ModifiedAt], [ModifiedBy]) VALUES (N'feffe337-31cc-45d6-0406-08dcd086a513', N'Apollo', N'Apollo-The Greek God', N'https://cdn.thestonestudio.in/wp-content/uploads/2020/09/apollo-sculpture.png', CAST(1200.000 AS Decimal(13, 3)), N'In Stock', N'519d033e-01e5-4120-cf26-08dcd085e6c7', CAST(N'2024-09-09T04:21:44.4594218' AS DateTime2), NULL, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/9/2024 9:55:12 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Category__737584F69B0B1514]    Script Date: 9/9/2024 9:55:12 AM ******/
ALTER TABLE [dbo].[Category] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT (newid()) FOR [AddressId]
GO
ALTER TABLE [dbo].[AppOrder] ADD  DEFAULT (newid()) FOR [OrderId]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT (newid()) FOR [CartId]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (newsequentialid()) FOR [CategoryId]
GO
ALTER TABLE [dbo].[OrderItem] ADD  DEFAULT (newid()) FOR [OrderItemId]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (newid()) FOR [PaymentId]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (newsequentialid()) FOR [ProductId]
GO
ALTER TABLE [dbo].[WishList] ADD  DEFAULT (newid()) FOR [WishListId]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppOrder]  WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[AppOrder]  WITH CHECK ADD FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[AppOrder]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([PaymentId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[AppOrder] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[WishList]  WITH CHECK ADD FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishList]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [ArtGalleryDb] SET  READ_WRITE 
GO
