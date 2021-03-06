USE [master]
GO

/****** Object:  Database [TVItems]    Script Date: 3/17/2016 4:32:08 PM ******/
CREATE DATABASE [TVItems]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TVItems', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TVItems.mdf' , SIZE = 12288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TVItems_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TVItems_log.ldf' , SIZE = 24384KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [TVItems] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TVItems].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TVItems] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TVItems] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TVItems] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TVItems] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TVItems] SET ARITHABORT OFF 
GO

ALTER DATABASE [TVItems] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TVItems] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TVItems] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TVItems] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TVItems] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TVItems] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TVItems] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TVItems] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TVItems] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TVItems] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TVItems] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TVItems] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TVItems] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TVItems] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TVItems] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TVItems] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TVItems] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TVItems] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TVItems] SET  MULTI_USER 
GO

ALTER DATABASE [TVItems] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TVItems] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TVItems] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TVItems] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [TVItems] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TVItems] SET  READ_WRITE 
GO

