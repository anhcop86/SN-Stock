 /****** Object:  Stored Procedure [dbo].ServiceTypeDelete    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[spExistServiceTypeIdForServiceType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spExistServiceTypeIdForServiceType
GO
	
CREATE PROCEDURE spExistServiceTypeIdForServiceType
	@ServiceTypeDescription nvarchar(50)
AS

SELECT 	
	ServiceTypeID,
	ServiceTypeDescription,
	CreatedDate,
	ModifiedDate
FROM ServiceType
WHERE ServiceTypeDescription = @ServiceTypeDescription
