CREATE TABLE [dbo].[Product] (
    [UPC]          INT            IDENTITY (100, 1) NOT NULL,
    [ProductType]  NVARCHAR (100) NULL,
    [ProductName]  NVARCHAR (100) NULL,
    [ProductPrice] INT            NULL
);

