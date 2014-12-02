

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_IPProxy_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_IPProxy_Insert]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_Membership_Insert] @Id = 1, @MemberName = 'MemberName', @Password = 'Password', @Email = 'Email', @IsApproved = 1, @IsLockedOut = 1, @CreateDate = 1, @LastLoginDate = 1, @LoginCount = 1

CREATE PROCEDURE [dbo].[PR_IPProxy_Insert]	
		@Id int output,
		@IPAddress char(16),
		@IPPort int,
		@StatusIP bit,
		@CreateDate int
		

AS

SET NOCOUNT ON;

BEGIN TRY
BEGIN TRAN

INSERT [dbo].[IPProxy]
(
	[IPAddress]
      ,[IPPort]
      ,[StatusIP]
      ,[CreateDate]
)
VALUES
(
		@IPAddress,
		@IPPort,
		@StatusIP,
		@CreateDate
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