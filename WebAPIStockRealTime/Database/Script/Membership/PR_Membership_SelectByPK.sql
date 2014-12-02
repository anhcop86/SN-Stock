GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_Membership_SelectByPK]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_Membership_SelectByPK]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_Membership_SelectByPK] @Id = 1

CREATE PROCEDURE [dbo].[PR_Membership_SelectByPK]

		@Id 		int

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[Membership].[Id],
		[dbo].[Membership].[MemberName],
		[dbo].[Membership].[Password],
		[dbo].[Membership].[Email],
		[dbo].[Membership].[IsApproved],
		[dbo].[Membership].[IsLockedOut],
		[dbo].[Membership].[CreateDate],
		[dbo].[Membership].[LastLoginDate],
		[dbo].[Membership].[LoginCount]
FROM  [dbo].[Membership]

WHERE [dbo].[Membership].[Id] = @Id

GO