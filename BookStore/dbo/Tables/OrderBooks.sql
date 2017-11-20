CREATE TABLE [dbo].[OrderBooks]
(
	OrderID INT NOT NULL,
	BookID INT NOT NULL,
	CONSTRAINT [PK_OrderBook] PRIMARY KEY (BookID, OrderID),
	CONSTRAINT [FK_OrderBook_Order] FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
	CONSTRAINT [FK_OrderBook_Book] FOREIGN KEY (BookID) REFERENCES Books(BookID),
	Quantity INT NOT NULL DEFAULT 1,
		[PlacedName] NVARCHAR(100) NULL,
	[PlacedUnitPrice] MONEY NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GetDate(),
	[DateLastModified] DATETIME NOT NULL DEFAULT GetDate(),
	
)
