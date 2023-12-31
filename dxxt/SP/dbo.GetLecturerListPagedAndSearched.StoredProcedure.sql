USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetLecturerListPagedAndSearched]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLecturerListPagedAndSearched]
    @PageNumber INT,
    @PageSize INT,
    @SearchQuery NVARCHAR(25) = NULL,
    @TotalCount INT OUTPUT
AS
BEGIN
    SELECT @TotalCount = COUNT(*)
    FROM Lecturer WITH (NOLOCK)
    WHERE IsDelete = 0
    AND (@SearchQuery IS NULL OR LecturerName LIKE '%' + @SearchQuery + '%');

    SELECT * FROM Lecturer
    WHERE IsDelete = 0
    AND (@SearchQuery IS NULL OR LecturerName LIKE '%' + @SearchQuery + '%')
    ORDER BY LecturerName
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO
