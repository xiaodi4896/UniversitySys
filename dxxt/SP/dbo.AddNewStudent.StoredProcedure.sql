USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[AddNewStudent]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewStudent]
    @StudentName NVARCHAR(20),
    @Email VARCHAR(30),
    @Intake DATE,
    @IsDelete BIT
AS
BEGIN
    INSERT INTO Student (StudentName, Email, Intake, IsDelete)
    VALUES (@StudentName, @Email, @Intake, @IsDelete);

    SELECT SCOPE_IDENTITY() AS NewStudentID;
END
GO
