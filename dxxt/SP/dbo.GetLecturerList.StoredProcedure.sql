USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[GetLecturerList]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLecturerList]
AS
BEGIN
	SELECT * FROM Lecturer
	WHERE IsDelete=0;
END
GO
