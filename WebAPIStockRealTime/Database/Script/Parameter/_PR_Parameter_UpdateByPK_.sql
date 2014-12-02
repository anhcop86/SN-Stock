GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_Parameter_UpdateByPK]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_Parameter_UpdateByPK]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_Parameter_UpdateByPK] @Key = 'Key', @Id = 'Id', @Value = 'Value', @CreateDate = 1

CREATE PROCEDURE [dbo].[PR_Parameter_UpdateByPK]

		@Key        		char (10),
		@Id         		char (32),
		@Value      		char (32),
		@CreateDate 		int

AS

SET NOCOUNT ON;

BEGIN TRY
BEGIN TRAN

UPDATE [dbo].[Parameter]
SET
		[Id] = @Id,
		[Value] = @Value,
		[CreateDate] = @CreateDate
WHERE [dbo].[Parameter].[Key] = @Key

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