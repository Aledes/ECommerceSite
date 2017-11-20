CREATE TABLE Books (
BookID INT IDENTITY(1,1) PRIMARY KEY,
Title NVARCHAR(100),
Year INT,
Price MONEY,
Quantity INT,
CoverImage NVARCHAR(300),
)
CREATE TABLE Authors(
AuthorID INT IDENTITY(1,1) PRIMARY KEY,
firstName NVARCHAR(100),
lastName NVARCHAR(100),
)
CREATE TABLE Books_Authors(
BookID INT,
AuthorID INT,
CONSTRAINT PK_Books_Authors PRIMARY KEY (BookID, AuthorID),
CONSTRAINT FK_Books
       FOREIGN KEY (BookID) REFERENCES Books (BookID),
CONSTRAINT FK_Authors
       FOREIGN KEY (AuthorID) References Authors (AuthorID)
)
