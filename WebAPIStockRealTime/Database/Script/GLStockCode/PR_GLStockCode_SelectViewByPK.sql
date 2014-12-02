GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_GLStockCode_SelectViewByPK]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_GLStockCode_SelectViewByPK]
END
GO

/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PR_GLStockCode_SelectViewByPK] @Id = 1

CREATE PROCEDURE [dbo].[PR_GLStockCode_SelectViewByPK]

		@Id 		int

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[GLStockCode].[Id],
		[dbo].[GLStockCode].[Code],
		[dbo].[GLStockCode].[Name],
		[dbo].[GLStockCode].[Remark],
		[dbo].[GLStockCode].[CreateDate],
		[dbo].[GLStockCode].[IdMenber]
FROM  [dbo].[GLStockCode]

WHERE [dbo].[GLStockCode].[Id] = @Id

GO