USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[UpdateLecturer]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateLecturer]
    @LecturerID INT,
    @LecturerName NVARCHAR(20), 
    @IsDelete BIT
AS
BEGIN
    UPDATE Lecturer WITH (ROWLOCK)
    SET LecturerName = @LecturerName,
        IsDelete = @IsDelete
    WHERE LecturerID = @LecturerID;

END
GO
