USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[CheckLecturerTeachingSubjects]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckLecturerTeachingSubjects]
    @LecturerID INT,
    @IsTeaching BIT OUTPUT
AS
BEGIN
    SET @IsTeaching = (SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
                       FROM Subject
                       WHERE LecturerID = @LecturerID)
END
GO
