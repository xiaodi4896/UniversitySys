USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[CheckSubjectNameExists]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckSubjectNameExists]
    @SubjectName NVARCHAR(30),
    @Exists BIT OUTPUT
AS
BEGIN
    SET @Exists = (SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
                   FROM Subject
                   WHERE SubjectName = @SubjectName)
END
GO
