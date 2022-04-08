# First Project with .NET & Angular

Basic CRUD and file upload with:

  * SQL
  * .NET
  * Angular 

For running successively: 
## 1 Connect to server as .(Dot)

![image](https://user-images.githubusercontent.com/44801609/162410094-c5644cb5-13e4-402d-8379-b10a61314bce.png)

## 2 Create Table Named Books

![image](https://user-images.githubusercontent.com/44801609/162410564-068ffcbb-e166-4a15-a283-db9aa7d64796.png)

_Info:_ Simple CREATE TABLE _TableName_ command

## 3 Create Procedures

###   1) spBooks_Create

```
CREATE PROCEDURE [dbo].[spBooks_Create]

@BookID INT,
@BookName NVARCHAR(20),
@Author NVARCHAR(20),
@BookPrice INT,
@BookIDOut INT OUTPUT

AS
BEGIN
    IF @BookID = 0
        BEGIN
        SELECT @BookID=max(BookID)+1 FROM dbo.Books
            INSERT INTO dbo.Books(
                BookID,
                BookName,
                Author,
                BookPrice
            )
             VALUES (
                @BookID,
                @BookName,
                @Author,
                @BookPrice
            );
            SET @BookIDOut = @BookID;
        END
    ELSE
        BEGIN
            UPDATE dbo.Books 
            SET BookName = @BookName,
                Author = @Author,
                BookPrice = @BookPrice
            WHERE BookID = @BookID;
            SET @BookIDOut = @BookID;
        END

END
```

###   2) spBooks_GetAll
```
CREATE PROCEDURE [dbo].[spBooks_GetAll]
AS
BEGIN
	SELECT *
	FROM dbo.Books;
END
```

###   3) spBooks_UpdateFile
```
CREATE PROCEDURE [dbo].[spBooks_UpdateFile]

@BookID INT,
@PhotoPath NVARCHAR(50)

AS
BEGIN
    UPDATE dbo.Books 
    SET PhotoPath = @PhotoPath
    WHERE BookID = @BookID;
END
```
###   4) spBooks_Delete
```
CREATE PROCEDURE [dbo].[spBooks_Delete]

    @BookID INT

    AS
    BEGIN
        DELETE FROM dbo.Books WHERE BookID = @BookID;
    END
```

Thats It Enjoy 🖤
