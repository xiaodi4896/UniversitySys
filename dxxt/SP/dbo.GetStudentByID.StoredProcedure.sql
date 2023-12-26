USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentByID]
    @StudentId INT
AS
BEGIN
    SELECT *
    FROM Student
    WHERE StudentID = @StudentID;
END
GO
