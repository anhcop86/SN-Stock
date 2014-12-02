GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_GLStockCode_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_GLStockCode_Insert]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_GLStockCode_Insert] @Id = 1, @Code = 'Code', @Name = 'Name', @Remark = 'Remark', @CreateDate = 1, @IdMenber = 1

CREATE PROCEDURE [dbo].[PR_GLStockCode_Insert]

		@Id         		int OUTPUT,
		@Code       		char (3),
		@Name       		nvarchar (125),
		@Remark     		nvarchar (256),
		@CreateDate 		int,
		@IdMenber   		int

AS

SET NOCOUNT ON;

BEGIN TRY
BEGIN TRAN

INSERT [dbo].[GLStockCode]
(
		[Code],
		[Name],
		[Remark],
		[CreateDate],
		[IdMenber]
)
VALUES
(
		@Code,
		@Name,
		@Remark,
		@CreateDate,
		@IdMenber
)

SET @Id = @@IDENTITY

COMMIT TRAN
END TRY

BEGIN CATCH
ROLLBACK TRAN

DECLARE @ErrorNumber_INT INT;
DECLARE @ErrorSeverity_INT INT;
DECLARE @ErrorProcedure_VC VARCHAR(200);
DECLARE @ErrorLine_INT INT;
DECLARE @ErrorMessage_NVC NVARCHAR(4000);

SELECT
		@ErrorMessage_NVC = ERROR_MESSAGE(),
		@ErrorSeverity_INT = ERROR_SEVERITY(),
		@ErrorNumber_INT = ERROR_NUMBER(),
		@ErrorProcedure_VC = ERROR_PROCEDURE(),
		@ErrorLine_INT = ERROR_LINE()

RAISERROR(@ErrorMessage_NVC,@ErrorSeverity_INT,1);

END CATCH

GO