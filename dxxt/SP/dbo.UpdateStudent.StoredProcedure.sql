USE [dxxtContext]
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 12/21/2023 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudent]
    @StudentID INT,
    @StudentName NVARCHAR(20),
    @Email VARCHAR(30),
    @Intake DATE,
    @IsDelete BIT
AS
BEGIN
    UPDATE Student WITH (ROWLOCK)
    SET StudentName = @StudentName,
        Email = @Email,
        Intake = @Intake,
        IsDelete = @IsDelete
    WHERE StudentID = @StudentID;

    SELECT @@ROWCOUNT;
END
GO
