USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_create_database]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_create_database] (@dbname varchar(30)) as

   DECLARE @SQL varchar(4000)

   SET @SQL = 'USE [master]'
   EXEC (@SQL)

   SET @SQL =        'CREATE DATABASE [' + @dbname + '] ON  PRIMARY '
   SET @SQL = @SQL + '( NAME = N''' + @dbname + '_Data'', FILENAME = N''C:\ESOURCE_DB\DATA\' + @dbname + '.mdf'' , SIZE = 50944KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%) '
   SET @SQL = @SQL + ' LOG ON ' 
   SET @SQL = @SQL + '( NAME = N''' + @dbname + '_Log'', FILENAME = N''C:\ESOURCE_DB\LOG\' + @dbname + '_log.ldf'' , SIZE = 241216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)'
   EXEC (@SQL)

   EXEC ('ALTER DATABASE [' + @dbname + '] SET COMPATIBILITY_LEVEL = 80')
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ANSI_NULL_DEFAULT OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ANSI_NULLS OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ANSI_PADDING OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ANSI_WARNINGS OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ARITHABORT OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET AUTO_CLOSE OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET AUTO_CREATE_STATISTICS ON') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET AUTO_SHRINK OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET AUTO_UPDATE_STATISTICS ON') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET CURSOR_CLOSE_ON_COMMIT OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET CURSOR_DEFAULT  GLOBAL') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET CONCAT_NULL_YIELDS_NULL OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET NUMERIC_ROUNDABORT OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET QUOTED_IDENTIFIER OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET RECURSIVE_TRIGGERS OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET  DISABLE_BROKER') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET AUTO_UPDATE_STATISTICS_ASYNC OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET DATE_CORRELATION_OPTIMIZATION OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET TRUSTWORTHY OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET ALLOW_SNAPSHOT_ISOLATION OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET PARAMETERIZATION SIMPLE') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET READ_COMMITTED_SNAPSHOT OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET HONOR_BROKER_PRIORITY OFF') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET  READ_WRITE')
   EXEC ('ALTER DATABASE [' + @dbname + '] SET RECOVERY FULL') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET  MULTI_USER') 
   EXEC ('ALTER DATABASE [' + @dbname + '] SET PAGE_VERIFY NONE')  
   EXEC ('ALTER DATABASE [' + @dbname + '] SET DB_CHAINING OFF') 

   EXEC sp_create_schema @dbname=@dbname
GO
