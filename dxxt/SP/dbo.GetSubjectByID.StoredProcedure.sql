USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectByID]
    @SubjectID INT
AS
BEGIN
    SELECT subject.SubjectID, subject.SubjectName, subject.LecturerID, subject.IsDelete, lecturer.LecturerName
    FROM Subject subject
    JOIN Lecturer lecturer ON subject.LecturerID = lecturer.LecturerID
    WHERE subject.SubjectID = @SubjectID AND Subject.IsDelete = 0;
END
GO
