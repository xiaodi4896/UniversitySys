USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[DeleteScoreByID]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteScoreByID]
    @ScoreID INT
AS
BEGIN
    UPDATE Score WITH (ROWLOCK)
    SET IsDelete = 1
    WHERE ScoreID = @ScoreID;

    SELECT @@ROWCOUNT;
END
GO
