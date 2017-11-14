CREATE TABLE [dbo].[Customer] (
    [CustomerID]        INT            IDENTITY (2000, 1) NOT NULL,
    [CustomerFirstName] NVARCHAR (100) NULL,
    [CustomerLastName]  NVARCHAR (100) NULL,
    [CustomerEmail]     NVARCHAR (100) NULL
);

