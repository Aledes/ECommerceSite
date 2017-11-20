CREATE TABLE [dbo].[Authors] (
    [AuthorID]  INT            IDENTITY (1, 1) NOT NULL,
    [firstName] NVARCHAR (100) NULL,
    [lastName]  NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([AuthorID] ASC)
);

