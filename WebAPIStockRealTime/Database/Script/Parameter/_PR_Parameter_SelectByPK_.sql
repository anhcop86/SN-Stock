IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_Parameter_SelectByPK]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_Parameter_SelectByPK]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_Parameter_SelectByPK] @Key = 'Key'

CREATE PROCEDURE [dbo].[PR_Parameter_SelectByPK]

		@Key 		char (10)

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[Parameter].[Key],
		[dbo].[Parameter].[Id],
		[dbo].[Parameter].[Value],
		[dbo].[Parameter].[CreateDate]
FROM  [dbo].[Parameter]

WHERE [dbo].[Parameter].[Key] = @Key

GO