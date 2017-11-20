CREATE TABLE [dbo].[Books_Authors] (
    [BookID]   INT NOT NULL,
    [AuthorID] INT NOT NULL,
    CONSTRAINT [PK_Books_Authors] PRIMARY KEY CLUSTERED ([BookID] ASC, [AuthorID] ASC),
    CONSTRAINT [FK_Authors] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Authors] ([AuthorID]),
    CONSTRAINT [FK_Books] FOREIGN KEY ([BookID]) REFERENCES [dbo].[Books] ([BookID])
);

