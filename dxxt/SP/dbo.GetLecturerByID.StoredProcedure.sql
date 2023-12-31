USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetLecturerByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLecturerByID]
    @LecturerID INT
AS
BEGIN
	SELECT lecturer.LecturerID, lecturer.LecturerName, lecturer.IsDelete
    FROM Lecturer lecturer
    WHERE lecturer.LecturerID = @LecturerID AND lecturer.IsDelete = 0;
END
GO
