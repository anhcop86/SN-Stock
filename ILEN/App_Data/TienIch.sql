--sap chép cấu trúc của 1 table sang table khác:
Select * into HoaDB.dbo.ThangDo from LongSurveyDB.dbo.ThangDo
--reset lại giá trị cho column identity về 1:
DBCC CHECKIDENT ('CapDoTieuChi', reseed, 0)