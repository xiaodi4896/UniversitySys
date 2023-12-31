USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectListPagedAndSearched]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectListPagedAndSearched]
    @PageNumber INT,
    @PageSize INT,
    @SearchQuery NVARCHAR(25) = NULL,
    @TotalCount INT OUTPUT
AS
BEGIN

    SELECT @TotalCount = COUNT(*)
    FROM Subject WITH (NOLOCK)
    INNER JOIN Lecturer ON Subject.LecturerID = Lecturer.LecturerID
    WHERE (@SearchQuery IS NULL OR Subject.SubjectName LIKE '%' + @SearchQuery + '%');

    SELECT Subject.*, Lecturer.LecturerName
    FROM Subject
    INNER JOIN Lecturer ON Subject.LecturerID = Lecturer.LecturerID
    WHERE (@SearchQuery IS NULL OR Subject.SubjectName LIKE '%' + @SearchQuery + '%')
    ORDER BY Subject.SubjectName, Lecturer.LecturerName
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO
