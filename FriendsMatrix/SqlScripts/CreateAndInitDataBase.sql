
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/09/2017 17:12:54
-- Generated from EDMX file: C:\Users\vitaliy.oleksyn\Desktop\MS\FriendsMatrix\FriendsMatrix\Models\DbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
--specify path to db
USE [LOCALDB.MDF] --[C:\USERS\VITALIY.OLEKSYN\DESKTOP\MS\FRIENDSMATRIX\FRIENDSMATRIX\APP_DATA\LOCALDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Relations_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Relations] DROP CONSTRAINT [FK_Relations_ToTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Relations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Relations];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Relations'
CREATE TABLE [dbo].[Relations] (
    [Id] int  NOT NULL IDENTITY (1,1),
    [UserId] int  NOT NULL,
    [FriendId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL IDENTITY (1,1),
    [Name] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [PK_Relations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [FK_Relations_ToTable]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Relations_ToTable'
CREATE INDEX [IX_FK_Relations_ToTable]
ON [dbo].[Relations]
    ([UserId]);
GO


-- --------------------------------------------------
-- Create stored
-- --------------------------------------------------


CREATE PROCEDURE GetFriendsLevels
	@Level int,
	@UserID int
AS

BEGIN 

	SET NOCOUNT ON;

	WITH UserCTE ([Level], FriendID) AS
		(
			 Select [Level] = 1, FriendID
			 From Relations 
			 Where UserID = @UserID
    
			 UNION ALL
    
			 Select [Level] + 1, r.FriendID 
			 From Relations r
			 JOIN UserCTE uc
			 ON uc.FriendID = r.UserID
			 WHERE [Level] < @Level AND uc.FriendID != @UserID
		)

	select u.Id, u.Name, uc.[Level] from UserCTE uc
	join Users u
	on u.id = uc.FriendID
	WHERE uc.FriendID != @UserID

END

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------