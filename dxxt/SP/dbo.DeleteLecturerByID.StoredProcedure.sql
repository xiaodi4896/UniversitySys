USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[DeleteLecturerByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLecturerByID]
    @LecturerID INT
AS
BEGIN
    UPDATE Lecturer WITH (ROWLOCK)
    SET IsDelete = 1
    WHERE LecturerID = @LecturerID;

    SELECT @@ROWCOUNT;
END
GO
