USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetScoreList]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetScoreList]
AS
BEGIN
    SELECT 
        score.ScoreID, 
        student.StudentID, 
        student.StudentName, 
        subject.SubjectID, 
        subject.SubjectName, 
        score.Marks, 
        score.IsDelete 
    FROM Score score
    INNER JOIN Student student ON score.StudentID = student.StudentID
    INNER JOIN Subject subject ON score.SubjectID = subject.SubjectID
    WHERE score.IsDelete = 0;
END
GO
