USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectsByLecturerID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectsByLecturerID]
    @LecturerID INT
AS
BEGIN
    SELECT 
        subject.SubjectID, 
        subject.SubjectName,
		subject.LecturerID,
        subject.IsDelete
    FROM Subject subject
    WHERE subject.LecturerID = @LecturerID AND subject.IsDelete = 0;
END
GO
