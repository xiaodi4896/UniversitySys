USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetFailedOrPendingSubjectCount]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFailedOrPendingSubjectCount]
    @StudentID INT,
    @ExcludeScoreID INT = NULL  -- Add an optional parameter to exclude a score
AS
BEGIN
    SELECT COUNT(*) 
    FROM Score
    WHERE StudentID = @StudentID AND ScoreID <> ISNULL(@ExcludeScoreID, 0) AND 
          (Marks < 50 OR Marks IS NULL)
END
GO
