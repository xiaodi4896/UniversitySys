USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetAvailableSubjectsForStudent]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAvailableSubjectsForStudent]
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SubjectID,SubjectName,LecturerID,IsDelete
FROM Subject
WHERE SubjectID NOT IN (
    SELECT SubjectID
    FROM Score
    WHERE StudentID = @StudentID)
    
END
GO
