USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[AddNewScore]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewScore]
    @StudentID INT,
    @SubjectID INT,
    @Marks DECIMAL(5,2),
    @IsDelete BIT
AS
BEGIN
    INSERT INTO Score (StudentID, SubjectID, Marks, IsDelete)
    VALUES (@StudentID, @SubjectID, @Marks, @IsDelete)

    SELECT SCOPE_IDENTITY() AS ScoreID;
END
GO
