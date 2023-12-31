USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[UpdateScore]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateScore]
    @ScoreID INT,
	@StudentID INT,
	@SubjectID INT,
    @Marks DECIMAL(4,1),

    @IsDelete BIT
AS
BEGIN
    UPDATE Score WITH (ROWLOCK)
    SET Marks = @Marks, IsDelete = @IsDelete, StudentID = @StudentID, SubjectID = @SubjectID
    WHERE ScoreID = @ScoreID;
END
GO
