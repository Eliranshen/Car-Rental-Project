USE [master]
GO
/****** Object:  Database [car_rental]    Script Date: 09/12/2021 12:38:29 ******/
CREATE DATABASE [car_rental]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'car_rental', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\car_rental.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'car_rental_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\car_rental_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [car_rental] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [car_rental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [car_rental] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [car_rental] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [car_rental] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [car_rental] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [car_rental] SET ARITHABORT OFF 
GO
ALTER DATABASE [car_rental] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [car_rental] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [car_rental] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [car_rental] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [car_rental] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [car_rental] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [car_rental] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [car_rental] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [car_rental] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [car_rental] SET  DISABLE_BROKER 
GO
ALTER DATABASE [car_rental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [car_rental] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [car_rental] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [car_rental] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [car_rental] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [car_rental] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [car_rental] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [car_rental] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [car_rental] SET  MULTI_USER 
GO
ALTER DATABASE [car_rental] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [car_rental] SET DB_CHAINING OFF 
GO
ALTER DATABASE [car_rental] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [car_rental] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [car_rental] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [car_rental] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [car_rental] SET QUERY_STORE = OFF
GO
USE [car_rental]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 09/12/2021 12:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[branch_number] [int] NOT NULL,
	[address] [nvarchar](50) NULL,
	[location] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[branch_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[car_rental_fields]    Script Date: 09/12/2021 12:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[car_rental_fields](
	[no] [int] IDENTITY(1,1) NOT NULL,
	[start_day] [date] NOT NULL,
	[return_date] [date] NOT NULL,
	[actual_return_date] [date] NULL,
	[user_id] [int] NOT NULL,
	[car_number] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[start_day] ASC,
	[return_date] ASC,
	[car_number] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarFields]    Script Date: 09/12/2021 12:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarFields](
	[id_of_car] [int] NOT NULL,
	[id_of_field] [int] NOT NULL,
	[car_type] [nvarchar](50) NOT NULL,
	[current_km] [int] NULL,
	[picture] [nvarchar](max) NULL,
	[proper_to_rent] [nvarchar](50) NULL,
	[free_for_rent] [nvarchar](50) NULL,
	[car_number] [int] NOT NULL,
	[branch_number] [int] NULL,
 CONSTRAINT [PK_CarFields] PRIMARY KEY CLUSTERED 
(
	[id_of_car] ASC,
	[id_of_field] ASC,
	[car_type] ASC,
	[car_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarTypes]    Script Date: 09/12/2021 12:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarTypes](
	[id_of_car] [int] NOT NULL,
	[manufacture_name] [nvarchar](50) NOT NULL,
	[car_model] [nvarchar](50) NOT NULL,
	[daily_cost] [int] NULL,
	[daily_delay_cost] [int] NULL,
	[year_of_production] [int] NULL,
	[gear] [nvarchar](50) NULL,
 CONSTRAINT [PK_CarTypes] PRIMARY KEY CLUSTERED 
(
	[id_of_car] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 09/12/2021 12:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[full_name] [nvarchar](50) NULL,
	[user_id] [int] NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[bdate] [date] NULL,
	[gender] [nvarchar](10) NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[picture] [nvarchar](max) NULL,
	[role] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[car_rental_fields] ON 

INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (7, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-10' AS Date), NULL, 0, 1005)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (5, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-14' AS Date), CAST(N'2021-12-14' AS Date), 0, 1003)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (1, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-15' AS Date), CAST(N'2021-12-16' AS Date), 0, 1000)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (3, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-15' AS Date), CAST(N'2021-12-15' AS Date), 0, 1002)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (8, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-24' AS Date), CAST(N'2021-12-24' AS Date), 0, 1007)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (2, CAST(N'2021-12-09' AS Date), CAST(N'2021-12-30' AS Date), CAST(N'2021-12-10' AS Date), 0, 1001)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (4, CAST(N'2021-12-09' AS Date), CAST(N'2022-01-04' AS Date), NULL, 0, 1006)
INSERT [dbo].[car_rental_fields] ([no], [start_day], [return_date], [actual_return_date], [user_id], [car_number]) VALUES (6, CAST(N'2021-12-09' AS Date), CAST(N'2022-01-06' AS Date), CAST(N'2022-01-06' AS Date), 0, 1004)
SET IDENTITY_INSERT [dbo].[car_rental_fields] OFF
GO
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (1, 1, N'private', 5000, N'https://mediacloud.carbuyer.co.uk/image/private/s--H_6Vk01l--/f_auto,t_content-image-full-desktop@2/v1579658755/carbuyer/2019/11/01_1.jpg', N'yes', N'yes', 1000, 100)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (1, 2, N'private', 100, N'https://www.topgear.com/sites/default/files/2021/09/Medium-2824-KamiqMonteCarlo.jpg?w=976&h=549', N'yes', N'yes', 1001, 200)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (2, 3, N'private', 900, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFmQzGmSflkAuEPk_ULxtO-lttLjvoAmW_9w&usqp=CAU', N'yes', N'yes', 1002, 200)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (3, 4, N'suv', 300, N'data:image/jpeg;base64,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', N'yes', N'yes', 1003, 300)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (4, 5, N'suv', 10000, N'https://www.conceptcarz.com/images/Skoda/skoda-karoq-se-drive-01.jpg', N'yes', N'yes', 1004, 300)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (4, 6, N'suv', 3000, N'https://upload.wikimedia.org/wikipedia/commons/1/13/%C5%A0KODA_Kodiaq.jpg', N'yes', N'no', 1005, 400)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (5, 7, N'private', 2500, N'https://media.autoweek.nl/m/sxeyuoib6eah.jpg', N'yes', N'no', 1006, 400)
INSERT [dbo].[CarFields] ([id_of_car], [id_of_field], [car_type], [current_km], [picture], [proper_to_rent], [free_for_rent], [car_number], [branch_number]) VALUES (5, 8, N'private', 200, N'https://i0.wp.com/techantibacterial.com/wp-content/uploads/2021/06/Screenshot-2021-06-10-at-11.26.54-PM.png?resize=640%2C406&ssl=1', N'yes', N'yes', 1007, 400)
GO
INSERT [dbo].[CarTypes] ([id_of_car], [manufacture_name], [car_model], [daily_cost], [daily_delay_cost], [year_of_production], [gear]) VALUES (1, N'skoda', N'kamiq', 350, 350, 2021, N'auto')
INSERT [dbo].[CarTypes] ([id_of_car], [manufacture_name], [car_model], [daily_cost], [daily_delay_cost], [year_of_production], [gear]) VALUES (2, N'skoda', N'kamiq', 250, 300, 2021, N'manual')
INSERT [dbo].[CarTypes] ([id_of_car], [manufacture_name], [car_model], [daily_cost], [daily_delay_cost], [year_of_production], [gear]) VALUES (3, N'skoda', N'karoq', 400, 400, 2021, N'auto')
INSERT [dbo].[CarTypes] ([id_of_car], [manufacture_name], [car_model], [daily_cost], [daily_delay_cost], [year_of_production], [gear]) VALUES (4, N'skoda', N'kodiaq', 500, 600, 2021, N'auto')
INSERT [dbo].[CarTypes] ([id_of_car], [manufacture_name], [car_model], [daily_cost], [daily_delay_cost], [year_of_production], [gear]) VALUES (5, N'skoda', N'octavia', 200, 200, 2020, N'auto')
GO
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'eliran shenhav', 1, N'eliran100', CAST(N'1984-09-08' AS Date), N'male', N'eliran100@gmail.com', N'qaz123!', NULL, N'admin')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'avi', 2, N'avi', CAST(N'1950-01-01' AS Date), N'male', N'avi@gmail.com', N'1234', N' ', N'admin')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'staff1', 3, N'einat', CAST(N'1990-01-01' AS Date), N'female', N'einat@gmail.com', N'1234@', N' ', N'worker')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'staff2', 4, N'yaniv', CAST(N'1991-01-01' AS Date), N'male', N'yaniv@gmail.com', N'1234@!', N' ', N'worker')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'user1', 5, N'dudu', CAST(N'1993-01-01' AS Date), N'male', N'dudu@gmail.com', N'1234@!#', N' ', N'user')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'user2', 6, N'dani', CAST(N'1990-01-01' AS Date), N'male', N'dani@gmail.com', N'789#$@', NULL, N'user')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'user3', 7, N'yoni', CAST(N'1990-01-01' AS Date), N'male', N'yonii@gmail.com', N'789#$@12', NULL, N'user')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'user4', 8, N'shay', CAST(N'1980-01-01' AS Date), N'female', N'shay@gmail.com', N'777@', NULL, N'user')
INSERT [dbo].[UserDetails] ([full_name], [user_id], [user_name], [bdate], [gender], [email], [password], [picture], [role]) VALUES (N'user5', 9, N'wowi', CAST(N'2000-01-01' AS Date), N'female', N'wowi@gmail.com', N'wowi123@', NULL, N'user')
GO
USE [master]
GO
ALTER DATABASE [car_rental] SET  READ_WRITE 
GO
