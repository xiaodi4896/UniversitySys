USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentList]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentList]
AS
BEGIN
    SELECT *
    FROM Student WHERE IsDelete=0;
END
GO
