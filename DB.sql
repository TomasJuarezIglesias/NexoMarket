USE [master]
GO
/****** Object:  Database [NexoMarket]    Script Date: 27/5/2025 21:24:30 ******/
CREATE DATABASE [NexoMarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NexoMarket', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NexoMarket.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NexoMarket_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NexoMarket_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NexoMarket] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NexoMarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NexoMarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NexoMarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NexoMarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NexoMarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NexoMarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [NexoMarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NexoMarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NexoMarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NexoMarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NexoMarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NexoMarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NexoMarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NexoMarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NexoMarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NexoMarket] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NexoMarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NexoMarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NexoMarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NexoMarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NexoMarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NexoMarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NexoMarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NexoMarket] SET RECOVERY FULL 
GO
ALTER DATABASE [NexoMarket] SET  MULTI_USER 
GO
ALTER DATABASE [NexoMarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NexoMarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NexoMarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NexoMarket] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NexoMarket] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NexoMarket] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NexoMarket', N'ON'
GO
ALTER DATABASE [NexoMarket] SET QUERY_STORE = ON
GO
ALTER DATABASE [NexoMarket] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NexoMarket]
GO
/****** Object:  Table [dbo].[BitacoraEvento]    Script Date: 27/5/2025 21:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitacoraEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[evento] [nvarchar](50) NOT NULL,
	[id_user] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_BitacoraEventos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamiliaPermiso]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamiliaPermiso](
	[IdFamilia] [int] NOT NULL,
	[Id_Permiso] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_FamiliaPermiso] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC,
	[Id_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPermiso] [int] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Url] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[Id_Permiso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Is_Familia] [bit] NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[Id_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[Id_Rol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_Permiso]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Permiso](
	[Id_Rol] [int] NOT NULL,
	[Id_Permiso] [int] NOT NULL,
 CONSTRAINT [PK_Rol_Permiso] PRIMARY KEY CLUSTERED 
(
	[Id_Rol] ASC,
	[Id_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/5/2025 21:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [char](64) NOT NULL,
	[IdRol] [int] NOT NULL,
	[IsBlocked] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BitacoraEvento] ON 

INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (4, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:48:53.867' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (5, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:51:54.890' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (6, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:51:54.890' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (7, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:52:22.700' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (8, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:54:05.393' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (9, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:55:34.543' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (10, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:55:56.773' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (11, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:57:19.877' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (12, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:57:37.933' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (13, N'Inicio de Sesion', 1, CAST(N'2025-05-22T19:59:34.133' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (14, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:10:18.557' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (15, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:12:39.520' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (16, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:24:18.760' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (17, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:24:18.760' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (18, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:24:33.560' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (19, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:30:49.250' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (20, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:32:20.560' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (21, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:35:08.760' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (22, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:37:04.837' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (23, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:38:03.173' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (24, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:41:37.637' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (25, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:44:09.043' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (26, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:46:03.973' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (27, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:47:20.540' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (28, N'Inicio de Sesion', 1, CAST(N'2025-05-22T20:50:57.760' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (29, N'Inicio de Sesion', 1, CAST(N'2025-05-22T21:03:43.013' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (30, N'Inicio de Sesion', 1, CAST(N'2025-05-27T14:54:47.807' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (31, N'Inicio de Sesion', 1, CAST(N'2025-05-27T15:25:34.480' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (32, N'Inicio de Sesion', 1, CAST(N'2025-05-27T16:24:55.870' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (33, N'Inicio de Sesion', 1, CAST(N'2025-05-27T17:20:14.807' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (34, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:20:29.523' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (35, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:21:13.227' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (36, N'Inicio de Sesion', 1, CAST(N'2025-05-27T17:21:32.253' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (37, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:21:54.577' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (38, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:22:55.963' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (39, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:23:37.030' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (40, N'Inicio de Sesion', 6, CAST(N'2025-05-27T17:29:11.477' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (41, N'Inicio de Sesion', 1, CAST(N'2025-05-27T19:21:10.087' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (42, N'Inicio de Sesion', 6, CAST(N'2025-05-27T19:21:50.057' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (43, N'Inicio de Sesion', 1, CAST(N'2025-05-27T20:10:55.000' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (44, N'Inicio de Sesion', 1, CAST(N'2025-05-27T20:34:22.200' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (45, N'Inicio de Sesion', 6, CAST(N'2025-05-27T20:45:21.417' AS DateTime))
INSERT [dbo].[BitacoraEvento] ([id], [evento], [id_user], [fecha]) VALUES (46, N'Inicio de Sesion', 7, CAST(N'2025-05-27T20:48:30.347' AS DateTime))
SET IDENTITY_INSERT [dbo].[BitacoraEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (1, 1, N'Usuarios', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (2, 2, N'Bitacora', N'BitacoraEventos.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (12, 3, N'Backup - Restore', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (13, 4, N'Productos', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (14, 5, N'Carrito', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (15, 9, N'Mis Compras', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (16, 7, N'Gestion de Productos', N'Inicio.aspx')
INSERT [dbo].[Menu] ([Id], [IdPermiso], [Title], [Url]) VALUES (17, 8, N'Historial de Ventas', N'Inicio.aspx')
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (1, N'Usuarios', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (2, N'Bitacora', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (3, N'Backup', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (4, N'Productos', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (5, N'Carrito', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (7, N'Gestión de Productos', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (8, N'Historial de Ventas', 0)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Is_Familia]) VALUES (9, N'Mis Compras', 0)
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([Id_Rol], [Nombre]) VALUES (1, N'Web Master')
INSERT [dbo].[Rol] ([Id_Rol], [Nombre]) VALUES (2, N'Cliente')
INSERT [dbo].[Rol] ([Id_Rol], [Nombre]) VALUES (3, N'Operador')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (1, 1)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (1, 2)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (1, 3)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (2, 4)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (2, 5)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (2, 9)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (3, 7)
INSERT [dbo].[Rol_Permiso] ([Id_Rol], [Id_Permiso]) VALUES (3, 8)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Username], [Password], [IdRol], [IsBlocked]) VALUES (1, N'admin', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1, 0)
INSERT [dbo].[Usuarios] ([Id], [Username], [Password], [IdRol], [IsBlocked]) VALUES (6, N'cliente', N'a60b85d409a01d46023f90741e01b79543a3cb1ba048eaefbe5d7a63638043bf', 2, 0)
INSERT [dbo].[Usuarios] ([Id], [Username], [Password], [IdRol], [IsBlocked]) VALUES (7, N'operador', N'a60b85d409a01d46023f90741e01b79543a3cb1ba048eaefbe5d7a63638043bf', 3, 0)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[BitacoraEvento] ADD  CONSTRAINT [DF_BitacoraEvento_fecha]  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[BitacoraEvento]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraEvento_Usuarios] FOREIGN KEY([id_user])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[BitacoraEvento] CHECK CONSTRAINT [FK_BitacoraEvento_Usuarios]
GO
ALTER TABLE [dbo].[FamiliaPermiso]  WITH CHECK ADD  CONSTRAINT [FK_FamiliaPermiso_Permiso] FOREIGN KEY([Id_Permiso])
REFERENCES [dbo].[Permiso] ([Id_Permiso])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FamiliaPermiso] CHECK CONSTRAINT [FK_FamiliaPermiso_Permiso]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Permiso] FOREIGN KEY([IdPermiso])
REFERENCES [dbo].[Permiso] ([Id_Permiso])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Permiso]
GO
ALTER TABLE [dbo].[Rol_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Permiso_Permiso] FOREIGN KEY([Id_Permiso])
REFERENCES [dbo].[Permiso] ([Id_Permiso])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rol_Permiso] CHECK CONSTRAINT [FK_Rol_Permiso_Permiso]
GO
ALTER TABLE [dbo].[Rol_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Permiso_Rol] FOREIGN KEY([Id_Rol])
REFERENCES [dbo].[Rol] ([Id_Rol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rol_Permiso] CHECK CONSTRAINT [FK_Rol_Permiso_Rol]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([Id_Rol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Rol]
GO
USE [master]
GO
ALTER DATABASE [NexoMarket] SET  READ_WRITE 
GO
