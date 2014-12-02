GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_Parameter_SelectAll]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_Parameter_SelectAll]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_Parameter_SelectAll]

CREATE PROCEDURE [dbo].[PR_Parameter_SelectAll]

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[Parameter].[Key],
		[dbo].[Parameter].[Id],
		[dbo].[Parameter].[Value],
		[dbo].[Parameter].[CreateDate]
FROM  [dbo].[Parameter]

GO