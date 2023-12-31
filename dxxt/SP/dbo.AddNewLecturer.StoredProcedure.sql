USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[AddNewLecturer]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewLecturer]
    @LecturerName NVARCHAR(20),
    @IsDelete BIT
AS
BEGIN
    INSERT INTO Lecturer (LecturerName, IsDelete)
    VALUES (@LecturerName, @IsDelete);

    SELECT SCOPE_IDENTITY();
END
GO
