CREATE TABLE [dbo].[Users] (
    [UserId]   INT             IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

