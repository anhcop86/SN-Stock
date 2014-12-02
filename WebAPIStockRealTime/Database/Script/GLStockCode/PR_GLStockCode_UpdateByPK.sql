GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_GLStockCode_UpdateByPK]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_GLStockCode_UpdateByPK]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_GLStockCode_UpdateByPK] @Id = 1, @Code = 'Code', @Name = 'Name', @Remark = 'Remark', @CreateDate = 1, @IdMenber = 1

CREATE PROCEDURE [dbo].[PR_GLStockCode_UpdateByPK]

		@Id         		int,
		@Code       		char (3),
		@Name       		nvarchar (125),
		@Remark     		nvarchar (256),
		@CreateDate 		int,
		@IdMenber   		int

AS

SET NOCOUNT ON;

BEGIN TRY
BEGIN TRAN

UPDATE [dbo].[GLStockCode]
SET
		[Code] = @Code,
		[Name] = @Name,
		[Remark] = @Remark,
		[CreateDate] = @CreateDate,
		[IdMenber] = @IdMenber
WHERE [dbo].[GLStockCode].[Id] = @Id

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