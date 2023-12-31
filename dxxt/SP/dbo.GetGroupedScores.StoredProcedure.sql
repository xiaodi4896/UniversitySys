USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetGroupedScores]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGroupedScores]
AS
BEGIN
    SELECT 
        s.ScoreID, s.StudentID, s.SubjectID, stu.StudentName, sub.SubjectName, s.Marks, s.IsDelete
    FROM 
        Score s
    INNER JOIN Student stu ON s.StudentID = stu.StudentID
    INNER JOIN Subject sub ON s.SubjectID = sub.SubjectID
    ORDER BY 
        s.StudentID, s.SubjectID
END
GO
