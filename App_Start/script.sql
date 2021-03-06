USE [master]
GO
/****** Object:  Database [AfpEAT]    Script Date: 13/03/2020 09:47:15 ******/
CREATE DATABASE [AfpEAT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AfpEAT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AfpEAT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AfpEAT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AfpEAT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AfpEAT] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AfpEAT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AfpEAT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AfpEAT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AfpEAT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AfpEAT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AfpEAT] SET ARITHABORT OFF 
GO
ALTER DATABASE [AfpEAT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AfpEAT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AfpEAT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AfpEAT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AfpEAT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AfpEAT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AfpEAT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AfpEAT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AfpEAT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AfpEAT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AfpEAT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AfpEAT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AfpEAT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AfpEAT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AfpEAT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AfpEAT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AfpEAT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AfpEAT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AfpEAT] SET  MULTI_USER 
GO
ALTER DATABASE [AfpEAT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AfpEAT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AfpEAT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AfpEAT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AfpEAT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AfpEAT] SET QUERY_STORE = OFF
GO
USE [AfpEAT]
GO
/****** Object:  Table [dbo].[Categorie]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorie](
	[IdCategorie] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Statut] [bit] NOT NULL,
 CONSTRAINT [PK_Categorie] PRIMARY KEY CLUSTERED 
(
	[IdCategorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commande]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commande](
	[IdCommande] [int] IDENTITY(1,1) NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
	[IdRestaurant] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Prix] [decimal](10, 2) NOT NULL,
	[IdEtatCommande] [int] NOT NULL,
 CONSTRAINT [PK_Commande] PRIMARY KEY CLUSTERED 
(
	[IdCommande] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommandeProduit]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommandeProduit](
	[IdCommandeProduit] [int] IDENTITY(1,1) NOT NULL,
	[IdCommande] [int] NOT NULL,
	[IdProduit] [int] NOT NULL,
	[Prix] [decimal](10, 2) NOT NULL,
	[Quantite] [int] NOT NULL,
 CONSTRAINT [PK_CommandeProduit_1] PRIMARY KEY CLUSTERED 
(
	[IdCommandeProduit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommandeProduitMenu]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommandeProduitMenu](
	[IdCommandeProduit] [int] NOT NULL,
	[IdMenu] [int] NOT NULL,
 CONSTRAINT [PK_CommandeProduitMenu] PRIMARY KEY CLUSTERED 
(
	[IdCommandeProduit] ASC,
	[IdMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EtatCommande]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EtatCommande](
	[IdEtatCommande] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EtatCommande] PRIMARY KEY CLUSTERED 
(
	[IdEtatCommande] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[IdMenu] [int] IDENTITY(1,1) NOT NULL,
	[IdRestaurant] [int] NOT NULL,
	[IdPhoto] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Statut] [bit] NOT NULL,
	[Prix] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[IdMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuCategorie]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategorie](
	[IdMenu] [int] NOT NULL,
	[IdCategorie] [int] NOT NULL,
 CONSTRAINT [PK_MenuCategorie] PRIMARY KEY CLUSTERED 
(
	[IdMenu] ASC,
	[IdCategorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[IdOperation] [int] IDENTITY(1,1) NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
	[Montant] [decimal](10, 2) NOT NULL,
	[IdTypeVersement] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[IdOperation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[IdPhoto] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[IdPhoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit](
	[IdProduit] [int] IDENTITY(1,1) NOT NULL,
	[IdPhoto] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prix] [decimal](10, 2) NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[Quantite] [int] NOT NULL,
	[Statut] [bit] NOT NULL,
 CONSTRAINT [PK_Produit] PRIMARY KEY CLUSTERED 
(
	[IdProduit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProduitCategorie]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProduitCategorie](
	[IdProduit] [int] NOT NULL,
	[IdCategorie] [int] NOT NULL,
	[IdRestaurant] [int] NOT NULL,
 CONSTRAINT [PK_ProduitCategorie] PRIMARY KEY CLUSTERED 
(
	[IdProduit] ASC,
	[IdCategorie] ASC,
	[IdRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProduitPhoto]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProduitPhoto](
	[IdProduit] [int] NOT NULL,
	[IdPhoto] [int] NOT NULL,
 CONSTRAINT [PK_ProduitPhoto] PRIMARY KEY CLUSTERED 
(
	[IdProduit] ASC,
	[IdPhoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurant](
	[IdRestaurant] [int] IDENTITY(1,1) NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
	[IdTypeCuisine] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[Adresse] [varchar](100) NOT NULL,
	[Tel] [varchar](12) NOT NULL,
	[Mobile] [varchar](12) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CodePostal] [varchar](5) NOT NULL,
	[Ville] [varchar](50) NOT NULL,
	[Statut] [bit] NOT NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[IdRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantPhoto]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantPhoto](
	[IdRestaurant] [int] NOT NULL,
	[IdPhoto] [int] NOT NULL,
 CONSTRAINT [PK_RestaurantPhoto] PRIMARY KEY CLUSTERED 
(
	[IdRestaurant] ASC,
	[IdPhoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionUtilisateur]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionUtilisateur](
	[IdSession] [varchar](50) NOT NULL,
	[DateSession] [datetime] NOT NULL,
 CONSTRAINT [PK_SessionUtilisateur] PRIMARY KEY CLUSTERED 
(
	[IdSession] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeCuisine]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeCuisine](
	[IdTypeCuisine] [int] IDENTITY(1,1) NOT NULL,
	[IdPhoto] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeCuisine] PRIMARY KEY CLUSTERED 
(
	[IdTypeCuisine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeVersement]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeVersement](
	[IdTypeVersement] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Statut] [bit] NOT NULL,
 CONSTRAINT [PK_TypeVersement] PRIMARY KEY CLUSTERED 
(
	[IdTypeVersement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateur](
	[IdUtilisateur] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](100) NOT NULL,
	[Prenom] [varchar](100) NOT NULL,
	[Matricule] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Statut] [bit] NOT NULL,
	[Solde] [decimal](10, 2) NOT NULL,
	[IdRole] [int] NULL,
	[IdSession] [varchar](50) NULL,
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED 
(
	[IdUtilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UtilisateurFavoris]    Script Date: 13/03/2020 09:47:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UtilisateurFavoris](
	[IdUtilisateur] [int] NOT NULL,
	[IdRestaurant] [int] NOT NULL,
 CONSTRAINT [PK_UtilisateurFavoris] PRIMARY KEY CLUSTERED 
(
	[IdUtilisateur] ASC,
	[IdRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorie] ON 

INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (1, N'Entrée', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (2, N'Plat', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (3, N'Dessert', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (4, N'Salade', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (5, N'Boisson', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (6, N'Sandwich', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (7, N'Nigiri', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (8, N'Menus Midi', 1)
INSERT [dbo].[Categorie] ([IdCategorie], [Nom], [Statut]) VALUES (9, N'Maki', 1)
SET IDENTITY_INSERT [dbo].[Categorie] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([IdMenu], [IdRestaurant], [IdPhoto], [Nom], [Statut], [Prix]) VALUES (1, 2, 2, N'Menu 1', 1, CAST(13.40 AS Decimal(10, 2)))
INSERT [dbo].[Menu] ([IdMenu], [IdRestaurant], [IdPhoto], [Nom], [Statut], [Prix]) VALUES (2, 2, 2, N'Menu 2', 1, CAST(13.40 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Menu] OFF
INSERT [dbo].[MenuCategorie] ([IdMenu], [IdCategorie]) VALUES (1, 1)
INSERT [dbo].[MenuCategorie] ([IdMenu], [IdCategorie]) VALUES (1, 7)
INSERT [dbo].[MenuCategorie] ([IdMenu], [IdCategorie]) VALUES (2, 1)
INSERT [dbo].[MenuCategorie] ([IdMenu], [IdCategorie]) VALUES (2, 3)
INSERT [dbo].[MenuCategorie] ([IdMenu], [IdCategorie]) VALUES (2, 9)
SET IDENTITY_INSERT [dbo].[Photo] ON 

INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (1, N'Indien.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (2, N'Japonais.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (3, N'Pizza.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (4, N'Libanais.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (5, N'Boulangerie.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (6, N'FastFood.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (7, N'Traditionnel.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (8, N'Brasserie.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (9, N'Chinois.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (10, N'Kariya1.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (11, N'Kariya2.jpg')
INSERT [dbo].[Photo] ([IdPhoto], [Nom]) VALUES (12, N'Kariya3.jpg')
SET IDENTITY_INSERT [dbo].[Photo] OFF
SET IDENTITY_INSERT [dbo].[Produit] ON 

INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (1, 2, N'2 nigiris Saumon', CAST(4.30 AS Decimal(10, 2)), N'Du Saumon frais sur une galette de riz vinaigré', 185, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (2, 2, N'2 nigiris Dorade', CAST(4.35 AS Decimal(10, 2)), N'De la dorade sur une galette de riz vinaigré.', 195, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (4, 2, N'2 nigiris Thon', CAST(4.35 AS Decimal(10, 2)), N'Du thon albacore sur une galette de riz vinaigré.', 198, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (5, 2, N'Dorayaki', CAST(4.10 AS Decimal(10, 2)), N'Gâteau à la génoise et à la confiture de haricot rouge', 100, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (6, 2, N'2 Mochis à la poudre de sésame', CAST(4.10 AS Decimal(10, 2)), N'Pâte de haricot rouge sucrée entourée de pâte de riz au sésame.', 200, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (7, 2, N'Coca 1,5L', CAST(4.00 AS Decimal(10, 2)), N'Coca', 100, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (9, 2, N'Eau Gazeuse 50CL', CAST(2.00 AS Decimal(10, 2)), N'Eau Gazeuse', 97, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (10, 2, N'Eau Minerale 50CL', CAST(2.00 AS Decimal(10, 2)), N'Eau Minerale', 100, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (11, 2, N'Riz vinaigré', CAST(2.50 AS Decimal(10, 2)), N'Du riz vinaigré.', 94, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (12, 2, N'Salade de choux', CAST(2.50 AS Decimal(10, 2)), N'Une salade de choux', 90, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (13, 2, N'6 makis Saumon', CAST(4.65 AS Decimal(10, 2)), N'Du saumon et des graines de sésame.', 600, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (15, 2, N'6 makis Thon', CAST(5.00 AS Decimal(10, 2)), N'Du thon albacore et des graines de sésame grillées.', 600, 1)
INSERT [dbo].[Produit] ([IdProduit], [IdPhoto], [Nom], [Prix], [Description], [Quantite], [Statut]) VALUES (16, 2, N'6 makis Concombre', CAST(3.80 AS Decimal(10, 2)), N'Du concombre finement coupé et des graines de sésame grillées.', 6, 1)
SET IDENTITY_INSERT [dbo].[Produit] OFF
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (1, 7, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (2, 7, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (4, 7, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (5, 3, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (6, 3, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (7, 5, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (9, 5, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (10, 5, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (11, 1, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (12, 1, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (13, 9, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (15, 9, 2)
INSERT [dbo].[ProduitCategorie] ([IdProduit], [IdCategorie], [IdRestaurant]) VALUES (16, 9, 2)
SET IDENTITY_INSERT [dbo].[Restaurant] ON 

INSERT [dbo].[Restaurant] ([IdRestaurant], [IdUtilisateur], [IdTypeCuisine], [Nom], [Description], [Adresse], [Tel], [Mobile], [Email], [CodePostal], [Ville], [Statut]) VALUES (2, 2, 2, N'Kariya Sushi', N' le  restaurant Japonais au  coeur de Maison Alfort 94700. Au fil du temps, cette belle histoire familiale devient une véritable institution du quartier accueillant chaque semaine familles, couples, amis et groupes autour de grandes tables rondes et d’une cuisine Japonais savoureuse et authentique.', N'133 rue Jean Jaurès', N'0212456487', N'0698456478', N'kariya@gmail.com', N'94700', N'Maison-Alfort', 1)
SET IDENTITY_INSERT [dbo].[Restaurant] OFF
INSERT [dbo].[RestaurantPhoto] ([IdRestaurant], [IdPhoto]) VALUES (2, 10)
INSERT [dbo].[RestaurantPhoto] ([IdRestaurant], [IdPhoto]) VALUES (2, 11)
INSERT [dbo].[RestaurantPhoto] ([IdRestaurant], [IdPhoto]) VALUES (2, 12)
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([IdRole], [Nom]) VALUES (1, N'Utilisateur')
INSERT [dbo].[Role] ([IdRole], [Nom]) VALUES (2, N'Restaurateur')
INSERT [dbo].[Role] ([IdRole], [Nom]) VALUES (3, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
INSERT [dbo].[SessionUtilisateur] ([IdSession], [DateSession]) VALUES (N'4flnjtriqr1pg1zpczhsv2y3', CAST(N'2020-03-13T09:38:15.780' AS DateTime))
INSERT [dbo].[SessionUtilisateur] ([IdSession], [DateSession]) VALUES (N'ix4dzsg1uxkd5vjofjqhaccy', CAST(N'2020-03-13T09:46:57.697' AS DateTime))
SET IDENTITY_INSERT [dbo].[TypeCuisine] ON 

INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (1, 1, N'Indien')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (2, 2, N'Japonais')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (3, 3, N'Pizza')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (4, 4, N'Libanais')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (5, 5, N'Boulangerie')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (6, 6, N'Fast Food')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (7, 7, N'Traditionnel')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (8, 8, N'Brasserie')
INSERT [dbo].[TypeCuisine] ([IdTypeCuisine], [IdPhoto], [Nom]) VALUES (9, 9, N'Chinois')
SET IDENTITY_INSERT [dbo].[TypeCuisine] OFF
SET IDENTITY_INSERT [dbo].[TypeVersement] ON 

INSERT [dbo].[TypeVersement] ([IdTypeVersement], [Nom], [Statut]) VALUES (1, N'Espece', 1)
INSERT [dbo].[TypeVersement] ([IdTypeVersement], [Nom], [Statut]) VALUES (2, N'Ticket Resto', 1)
SET IDENTITY_INSERT [dbo].[TypeVersement] OFF
SET IDENTITY_INSERT [dbo].[Utilisateur] ON 

INSERT [dbo].[Utilisateur] ([IdUtilisateur], [Nom], [Prenom], [Matricule], [Password], [Statut], [Solde], [IdRole], [IdSession]) VALUES (1, N'Moreau', N'Charlie', N'cda511', N'AF1zlgWIpyGx2ta8v3HM2OnUf5lZP3w4VS+eZUNSCRFmQbPaEk/LjzUaqbbekFrmpw==', 1, CAST(395.95 AS Decimal(10, 2)), 1, N'4flnjtriqr1pg1zpczhsv2y3')
INSERT [dbo].[Utilisateur] ([IdUtilisateur], [Nom], [Prenom], [Matricule], [Password], [Statut], [Solde], [IdRole], [IdSession]) VALUES (2, N'Grominet', N'Titi', N'cda102', N'ADN6DiSGvNOETb8cwJxoku9m/wH6o3kpZFgRtbOm5oxAzWM+AKIWvdiTBdyELE3omw==', 1, CAST(13.75 AS Decimal(10, 2)), 2, NULL)
INSERT [dbo].[Utilisateur] ([IdUtilisateur], [Nom], [Prenom], [Matricule], [Password], [Statut], [Solde], [IdRole], [IdSession]) VALUES (3, N'Coucou', N'Toto', N'admin', N'ADN6DiSGvNOETb8cwJxoku9m/wH6o3kpZFgRtbOm5oxAzWM+AKIWvdiTBdyELE3omw==', 1, CAST(0.00 AS Decimal(10, 2)), 3, NULL)
SET IDENTITY_INSERT [dbo].[Utilisateur] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_Utilisateur]    Script Date: 13/03/2020 09:47:15 ******/
ALTER TABLE [dbo].[Utilisateur] ADD  CONSTRAINT [UC_Utilisateur] UNIQUE NONCLUSTERED 
(
	[Matricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Commande]  WITH CHECK ADD  CONSTRAINT [FK_Commande_EtatCommande] FOREIGN KEY([IdEtatCommande])
REFERENCES [dbo].[EtatCommande] ([IdEtatCommande])
GO
ALTER TABLE [dbo].[Commande] CHECK CONSTRAINT [FK_Commande_EtatCommande]
GO
ALTER TABLE [dbo].[Commande]  WITH CHECK ADD  CONSTRAINT [FK_Commande_Restaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurant] ([IdRestaurant])
GO
ALTER TABLE [dbo].[Commande] CHECK CONSTRAINT [FK_Commande_Restaurant]
GO
ALTER TABLE [dbo].[Commande]  WITH CHECK ADD  CONSTRAINT [FK_Commande_Utilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Commande] CHECK CONSTRAINT [FK_Commande_Utilisateur]
GO
ALTER TABLE [dbo].[CommandeProduit]  WITH CHECK ADD  CONSTRAINT [FK_CommandeProduit_Commande] FOREIGN KEY([IdCommande])
REFERENCES [dbo].[Commande] ([IdCommande])
GO
ALTER TABLE [dbo].[CommandeProduit] CHECK CONSTRAINT [FK_CommandeProduit_Commande]
GO
ALTER TABLE [dbo].[CommandeProduit]  WITH CHECK ADD  CONSTRAINT [FK_CommandeProduit_Produit] FOREIGN KEY([IdProduit])
REFERENCES [dbo].[Produit] ([IdProduit])
GO
ALTER TABLE [dbo].[CommandeProduit] CHECK CONSTRAINT [FK_CommandeProduit_Produit]
GO
ALTER TABLE [dbo].[CommandeProduitMenu]  WITH CHECK ADD  CONSTRAINT [FK_CommandeProduitMenu_CommandeProduit] FOREIGN KEY([IdCommandeProduit])
REFERENCES [dbo].[CommandeProduit] ([IdCommandeProduit])
GO
ALTER TABLE [dbo].[CommandeProduitMenu] CHECK CONSTRAINT [FK_CommandeProduitMenu_CommandeProduit]
GO
ALTER TABLE [dbo].[CommandeProduitMenu]  WITH CHECK ADD  CONSTRAINT [FK_CommandeProduitMenu_Menu] FOREIGN KEY([IdMenu])
REFERENCES [dbo].[Menu] ([IdMenu])
GO
ALTER TABLE [dbo].[CommandeProduitMenu] CHECK CONSTRAINT [FK_CommandeProduitMenu_Menu]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Photo] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photo] ([IdPhoto])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Photo]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Restaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurant] ([IdRestaurant])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Restaurant]
GO
ALTER TABLE [dbo].[MenuCategorie]  WITH CHECK ADD  CONSTRAINT [FK_MenuCategorie_Categorie] FOREIGN KEY([IdCategorie])
REFERENCES [dbo].[Categorie] ([IdCategorie])
GO
ALTER TABLE [dbo].[MenuCategorie] CHECK CONSTRAINT [FK_MenuCategorie_Categorie]
GO
ALTER TABLE [dbo].[MenuCategorie]  WITH CHECK ADD  CONSTRAINT [FK_MenuCategorie_Menu] FOREIGN KEY([IdMenu])
REFERENCES [dbo].[Menu] ([IdMenu])
GO
ALTER TABLE [dbo].[MenuCategorie] CHECK CONSTRAINT [FK_MenuCategorie_Menu]
GO
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_TypeVersement] FOREIGN KEY([IdTypeVersement])
REFERENCES [dbo].[TypeVersement] ([IdTypeVersement])
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_TypeVersement]
GO
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Utilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Utilisateur]
GO
ALTER TABLE [dbo].[Produit]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Photo] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photo] ([IdPhoto])
GO
ALTER TABLE [dbo].[Produit] CHECK CONSTRAINT [FK_Produit_Photo]
GO
ALTER TABLE [dbo].[ProduitCategorie]  WITH CHECK ADD  CONSTRAINT [FK_ProduitCategorie_Categorie] FOREIGN KEY([IdCategorie])
REFERENCES [dbo].[Categorie] ([IdCategorie])
GO
ALTER TABLE [dbo].[ProduitCategorie] CHECK CONSTRAINT [FK_ProduitCategorie_Categorie]
GO
ALTER TABLE [dbo].[ProduitCategorie]  WITH CHECK ADD  CONSTRAINT [FK_ProduitCategorie_Produit] FOREIGN KEY([IdProduit])
REFERENCES [dbo].[Produit] ([IdProduit])
GO
ALTER TABLE [dbo].[ProduitCategorie] CHECK CONSTRAINT [FK_ProduitCategorie_Produit]
GO
ALTER TABLE [dbo].[ProduitCategorie]  WITH CHECK ADD  CONSTRAINT [FK_ProduitCategorie_Restaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurant] ([IdRestaurant])
GO
ALTER TABLE [dbo].[ProduitCategorie] CHECK CONSTRAINT [FK_ProduitCategorie_Restaurant]
GO
ALTER TABLE [dbo].[ProduitPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProduitPhoto_Photo] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photo] ([IdPhoto])
GO
ALTER TABLE [dbo].[ProduitPhoto] CHECK CONSTRAINT [FK_ProduitPhoto_Photo]
GO
ALTER TABLE [dbo].[ProduitPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProduitPhoto_Produit] FOREIGN KEY([IdProduit])
REFERENCES [dbo].[Produit] ([IdProduit])
GO
ALTER TABLE [dbo].[ProduitPhoto] CHECK CONSTRAINT [FK_ProduitPhoto_Produit]
GO
ALTER TABLE [dbo].[Restaurant]  WITH CHECK ADD  CONSTRAINT [FK_Restaurant_TypeCuisine] FOREIGN KEY([IdTypeCuisine])
REFERENCES [dbo].[TypeCuisine] ([IdTypeCuisine])
GO
ALTER TABLE [dbo].[Restaurant] CHECK CONSTRAINT [FK_Restaurant_TypeCuisine]
GO
ALTER TABLE [dbo].[Restaurant]  WITH CHECK ADD  CONSTRAINT [FK_Restaurant_Utilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Restaurant] CHECK CONSTRAINT [FK_Restaurant_Utilisateur]
GO
ALTER TABLE [dbo].[RestaurantPhoto]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantPhoto_Photo] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photo] ([IdPhoto])
GO
ALTER TABLE [dbo].[RestaurantPhoto] CHECK CONSTRAINT [FK_RestaurantPhoto_Photo]
GO
ALTER TABLE [dbo].[RestaurantPhoto]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantPhoto_Restaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurant] ([IdRestaurant])
GO
ALTER TABLE [dbo].[RestaurantPhoto] CHECK CONSTRAINT [FK_RestaurantPhoto_Restaurant]
GO
ALTER TABLE [dbo].[TypeCuisine]  WITH CHECK ADD  CONSTRAINT [FK_TypeCuisine_Photo] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photo] ([IdPhoto])
GO
ALTER TABLE [dbo].[TypeCuisine] CHECK CONSTRAINT [FK_TypeCuisine_Photo]
GO
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[Utilisateur] CHECK CONSTRAINT [FK_Utilisateur_Role]
GO
ALTER TABLE [dbo].[UtilisateurFavoris]  WITH CHECK ADD  CONSTRAINT [FK_UtilisateurFavoris_Restaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurant] ([IdRestaurant])
GO
ALTER TABLE [dbo].[UtilisateurFavoris] CHECK CONSTRAINT [FK_UtilisateurFavoris_Restaurant]
GO
ALTER TABLE [dbo].[UtilisateurFavoris]  WITH CHECK ADD  CONSTRAINT [FK_UtilisateurFavoris_Utilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[UtilisateurFavoris] CHECK CONSTRAINT [FK_UtilisateurFavoris_Utilisateur]
GO
USE [master]
GO
ALTER DATABASE [AfpEAT] SET  READ_WRITE 
GO
