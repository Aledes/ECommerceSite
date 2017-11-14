CREATE TABLE [dbo].[Store] (
    [StoreID]      INT            IDENTITY (1000, 1) NOT NULL,
    [BusinessName] NVARCHAR (100) NULL,
    CONSTRAINT [Pk_Store] PRIMARY KEY CLUSTERED ([StoreID] ASC)
);

