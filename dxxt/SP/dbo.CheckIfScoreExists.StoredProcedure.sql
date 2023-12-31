USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[CheckIfScoreExists]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckIfScoreExists]
    @StudentID INT,
    @SubjectID INT,
    @ExcludeScoreID INT = NULL
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Score
        WHERE StudentID = @StudentID 
        AND SubjectID = @SubjectID 
        AND ISNULL(ScoreID, 0) <> ISNULL(@ExcludeScoreID, 0)
    )
        SELECT 1 AS ExistsFlag
    ELSE
        SELECT 0 AS ExistsFlag
END
GO
