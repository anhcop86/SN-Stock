
IF EXISTS (SELECT * FROM sys.sysobjects WHERE id = object_id(N'[dbo].[PR_IPProxy_SelectAll]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
		DROP PROCEDURE [dbo].[PR_IPProxy_SelectAll]
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

CREATE PROCEDURE [dbo].[PR_IPProxy_SelectAll]

AS

SET NOCOUNT ON;

SELECT [Id]
      ,[IPAddress]
      ,[IPPort]
      ,[StatusIP]
      ,[CreateDate]
  FROM [IPProxy]

GO