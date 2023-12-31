USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[CheckStudentEmailExists]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckStudentEmailExists]
    @Email NVARCHAR(255),
    @Exists BIT OUTPUT
AS
BEGIN
    SET @Exists = (SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
                   FROM Student
                   WHERE Email = @Email)
END
GO
