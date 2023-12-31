USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectList]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectList]
AS
BEGIN
    SELECT subject.SubjectID, subject.SubjectName, subject.LecturerID, lecturer.LecturerName, subject.IsDelete
    FROM Subject subject
    INNER JOIN Lecturer lecturer ON subject.LecturerID = lecturer.LecturerID
	WHERE subject.IsDelete = 0;
END
GO
