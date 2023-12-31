USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[StudentEnrollCheck]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StudentEnrollCheck]
    @StudentID INT
AS
BEGIN
    DECLARE @FailingOrNullSubjectsCount INT

    SELECT @FailingOrNullSubjectsCount = COUNT(*)
    FROM Score
    WHERE StudentID = @StudentID AND (Marks < 50 OR Marks IS NULL)

    IF @FailingOrNullSubjectsCount < 10
        SELECT 1 AS CanEnroll 
    ELSE
        SELECT 0 AS CanEnroll 
END
GO
