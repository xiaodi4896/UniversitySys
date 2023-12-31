USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDetails]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentDetails]
    @StudentID INT
AS
BEGIN
    SELECT 
        s.StudentID, 
        s.StudentName, 
        s.Email, 
        s.Intake,
        sub.SubjectName, 
        sc.Marks
    FROM Student s
    LEFT JOIN Score sc ON s.StudentID = sc.StudentID
    LEFT JOIN Subject sub ON sc.SubjectID = sub.SubjectID
    WHERE s.StudentID = @StudentID
END
GO
