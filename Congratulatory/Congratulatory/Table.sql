CREATE TABLE [dbo].[Birthdays]
(
	[IdBirthday] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [date] DATETIME NOT NULL, 
    [fio] VARCHAR(100) NOT NULL, 
    [personStatus] VARCHAR(500) NULL 
)