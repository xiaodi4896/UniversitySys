USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSubject]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubject]
    @SubjectID INT,
    @SubjectName NVARCHAR(30),
	@LecturerID INT,
    @IsDelete BIT
AS
BEGIN
    UPDATE Subject WITH (ROWLOCK)
    SET SubjectName = @SubjectName,
		LecturerID = @LecturerID,
        IsDelete = @IsDelete
    WHERE SubjectID = @SubjectID;

    SELECT @@ROWCOUNT;
END
GO
