USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[DeleteSubjectByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubjectByID]
    @SubjectID INT
AS
BEGIN
    UPDATE Subject WITH (ROWLOCK)
    SET IsDelete = 1
    WHERE SubjectID = @SubjectID;

    SELECT @@ROWCOUNT;
END
GO
