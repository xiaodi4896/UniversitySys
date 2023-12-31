USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[AddNewSubject]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewSubject]
    @SubjectName NVARCHAR(30),
    @LecturerID INT, 
    @IsDelete BIT
AS
BEGIN
    INSERT INTO Subject (SubjectName, LecturerID, IsDelete)
    VALUES (@SubjectName, @LecturerID, @IsDelete)

    SELECT SCOPE_IDENTITY() AS SubjectID; 
END
GO
