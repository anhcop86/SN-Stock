

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_Membership_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_Membership_Insert]
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

CREATE PROCEDURE [dbo].[PR_Membership_Insert]

		@Id            		int OUTPUT,
		@MemberName    		nvarchar (128),
		@Password      		nvarchar (128),
		@Email         		nvarchar (256),
		@IsApproved    		bit,
		@IsLockedOut   		bit,
		@CreateDate    		int,
		@LastLoginDate 		int,
		@LoginCount    		int

AS

SET NOCOUNT ON;

BEGIN TRY
BEGIN TRAN

INSERT [dbo].[Membership]
(
		[MemberName],
		[Password],
		[Email],
		[IsApproved],
		[IsLockedOut],
		[CreateDate],
		[LastLoginDate],
		[LoginCount]
)
VALUES
(
		@MemberName,
		@Password,
		@Email,
		@IsApproved,
		@IsLockedOut,
		@CreateDate,
		@LastLoginDate,
		@LoginCount
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