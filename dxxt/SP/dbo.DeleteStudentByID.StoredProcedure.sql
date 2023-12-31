USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudentByID]
    @StudentID INT
AS
BEGIN
    UPDATE Student WITH (ROWLOCK)
    SET IsDelete = 1
    WHERE StudentID = @StudentID;

    SELECT @@ROWCOUNT;
END
GO
