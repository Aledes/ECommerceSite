CREATE TABLE [dbo].[Books] (
    [BookID]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (100) NULL,
    [Year]       INT            NULL,
    [Price]      MONEY          NULL,
    [Quantity]   INT            NULL,
    [CoverImage] NVARCHAR (300) NULL,

    [Description] NTEXT NULL, 
    [DateCreated] DATETIME NULL DEFAULT GetDate(),
	[rowguid] UNIQUEIDENTIFIER NULL DEFAULT NewID(),
	NewColumn NCHAR(10) NULL,
    CONSTRAINT PK_Books PRIMARY KEY (BookID),
	CONSTRAINT UQ_Book_rowguid UNIQUE(rowguid)

);

