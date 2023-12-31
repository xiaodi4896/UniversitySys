USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetScoreListPagedAndSearched]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetScoreListPagedAndSearched]
    @PageNumber INT,
    @PageSize INT,
    @SearchQuery NVARCHAR(100) = NULL,
    @TotalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @TotalCount = COUNT(*)
    FROM Score WITH (NOLOCK)
    INNER JOIN Student ON Score.StudentID = Student.StudentID
    INNER JOIN Subject ON Score.SubjectID = Subject.SubjectID
    WHERE (@SearchQuery IS NULL OR Student.StudentName LIKE '%' + @SearchQuery + '%');

    SELECT Score.*, Student.StudentName, Subject.SubjectName
    FROM Score
    INNER JOIN Student ON Score.StudentID = Student.StudentID
    INNER JOIN Subject ON Score.SubjectID = Subject.SubjectID
    WHERE (@SearchQuery IS NULL OR Student.StudentName LIKE '%' + @SearchQuery + '%')
    ORDER BY Student.StudentName, Subject.SubjectName
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO
