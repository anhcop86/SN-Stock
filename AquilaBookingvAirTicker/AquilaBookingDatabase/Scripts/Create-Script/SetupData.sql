-- =============================================
-- Script Template
-- =============================================

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRANSACTION
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (1,N'BioMatrix',N'Bio Matrix identification service','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (2,N'AccountService',N'Account services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (3,N'PrintingService',N'Printing services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (4,N'BillPaymentService',N'Billing services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (5,N'CashDepositeService',N'CashDepositeService','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (6,N'TopUpService',N'Topup services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (7,N'BusinessIntelligence',N'Business Intelligence analytical services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (8,N'UtilitiesService',N'Utilities services','2012-01-01 00:00:00','tuan.duong')
INSERT INTO MaterService (MaterServiceId,MaterServiceCode, Name, CreatedDate, CreatedBy) VALUES (9,N'CrmService',N'CRM services','2012-01-01 00:00:00','tuan.duong')


INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (1,'VerificationWithCard',1, 'verfication with card (card +FP)',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (2,'VerificatioWithPin',1, 'verfication with card (pin +FP)',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (3,'Identification',1, 'identifiaction',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (4,'AccountSignUp',2, 'Account sign up at kiosk',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (5,'A4FullStatement',3, 'Print A4 Full statement',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (6,'PassBook',3, 'PassBook Printing',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (7,'PowerPayment',4, 'Power Bill payment',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (8,'WaterPayment',4, 'Water Bill Payment',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (9,'CashDeposit',5, 'Cash deposit at Kiosk',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (10,'PrepaidCardTopUp',6, 'Prepaid Card Topup',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (11,'KioskBI',7, 'BI at kiosk side',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (12,'ProviderBI',7, 'BI for Banks',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (13,'CardRetainAlert',8, 'Card Retain Alert',null, '2012-01-01 00:00:00','tuan.duong')
INSERT INTO SubService (SubServiceId, SubServiceCode, MaterServiceId,Name ,ParentId, CreatedDate, CreatedBy) VALUES (14,'PhysicalFraudAlert',8, ' Physical fraud alert',null, '2012-01-01 00:00:00','tuan.duong')
GO
INSERT INTO Province (Name, EnglishName, CreatedDate, CreatedBy)   VALUES (N'Hồ Chí Minh',NULL, '2012-01-01 00:00:00','tuan.duong')
 GO

 INSERT INTO Location (ProvinceId, LocationNumber, streetName, ward, Remark, CreatedDate, CreatedBy)  VALUES    (1,123,N'Nguyễn Thị Minh Khai',N'Quận 3', NULL,'2012-01-01 00:00:00','tuan.duong');
 GO
 INSERT INTO Tenant (Code,LocationId, Name, EnglishName, CreatedDate, CreatedBy) VALUES ('01',1,'ACB','ACB', '2012-01-01 00:00:00','tuan.duong')
 GO
 INSERT INTO Branch (Code,TenantId, Name, Remark, CreatedDate, CreatedBy) VALUES ('01', 1, N'Hội sở ngân hàng Á Châu', NULL, '2012-01-01 00:00:00','tuan.duong')
GO
 INSERT INTO SeftServiceTerminal(BranchId, Name, GeneralConfig, SeftServiceTerminalDes, CreatedDate, CreatedBy) VALUES (1, 'SST So 0001', '2gb', NULL, '2012-01-01 00:00:00','tuan.duong')
 GO
 INSERT INTO SeftServiceTerminalReg (SeftServiceTerminalId, LocationId, TenantId, IPAddress, BeginDate, EndDate, HardDiskKey, CPUKey, MainboardKey, keyboardKey, CombineKey, SeftServiceTerminalStatus, CreatedDate, CreatedBy)
								VALUES(1,1,1,'192.168.1.101','20120101', NULL, '123','11','11','11','11','A','2012-01-01 00:00:00','tuan.duong')

GO
INSERT INTO SystemParameter(ParameterId,ParameterName,ParameterValue,ParameterType,CreatedDate,CreatedBy) 
			Values('21d8bf61-b739-4a94-8050-297f03c8f0dc','DIEBOLD_VERIFY_ACCOUNT','NO','String','01-01-2012','system')

GO

GO
---- power----
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('e40b83b0-2ec3-43e3-84ad-1803bc0e947e','PE01000042886','TRAN VAN AN', '123 Nguyen Trai','123456','0090281','200000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('94ccbf3b-4cd3-4ad5-8048-5bdb32921389','PE001','TRAN VAN AN', '123 TRAN HUNG DAO','5523457','0095656','251000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('79e96187-c2e6-4c16-b782-238b4d835a73','PE002','NGUYEN VAN BINH AN', '123 LE DUAN','565','1190281','145700','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('0fa937a1-494c-4bce-ae8d-f2d2f3ca1769','PE003','LE THANH BINH', '123 NGUYEN DU','65656','0790881','122100','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('ce798163-7944-4e5f-a679-2bdc14d0050d','PE004','TRAN THAN HUONG', '123 TON DUC THANG','6565','198000','3361469','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('74b4d1c7-b3e1-4832-b838-ee0b0faeb4b6','PE005','NGUYEN GIA BAO', '123 DUONG 3/2','565','0090281','1550000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','POWER','',0,'01-01-2012','system');

 ----water----

 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('8656f42a-7651-40dc-ba10-9bd4dbaf17a1','W001','TRAN VAN AN', '6767 Tran Dinh xu','123456','0090281','200000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('7419246f-b53c-49e3-b22a-0e0fc36c83fe','W002','TRAN VAN AN', '344 HOANG HOA THAM','5523457','0095656','251000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('95b38b5b-8970-4cba-9fee-164fbb3e81f3','W003','NGUYEN VAN BINH AN', '556 LE VAN HUU','565','1190281','145700','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('7ffa2bb5-fa83-4c7c-8dd0-03acc15427fa','W004','LE THANH BINH', '90/3 NGUYEN VAN CU','65656','0790881','122100','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('583e804f-d9eb-4fb8-9b29-7e9f65226ca6','W005','TRAN THAN HUONG', '12/3/3 NGUYEN DINH CHIEU','6565','198000','3361469','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('d5e3f59b-bb18-44ed-8d73-27b97cebc6a3','W006','NGUYEN GIA BAO', '962 DUONG 3/2','565','0090281','1550000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','WATER','',0,'01-01-2012','system');
 GO
 ----iNTERNET ADSL----
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('4c2e9b7b-63e2-4e82-a666-a1dd2c2374b1','I001','TRAN VAN AN', '6767 Tran Dinh xu','123456','0090281','200000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('07e2dda0-84e4-426c-b9c1-4dadd83afb04','I002','TRAN VAN AN', '344 HOANG HOA THAM','5523457','0095656','251000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('8cbf18f2-b1af-40d7-a6ae-5439ae6fb53d','I003','NGUYEN VAN BINH AN', '556 LE VAN HUU','565','1190281','145700','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('11874faa-b9b2-41c1-9bb5-9d2d7121ccb1','I004','LE THANH BINH', '90/3 NGUYEN VAN CU','65656','0790881','122100','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('efb67788-62c6-4089-8063-0c1327accd13','I005','TRAN THAN HUONG', '12/3/3 NGUYEN DINH CHIEU','6565','198000','3361469','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('f2a07f66-9873-4475-b3c1-d27b6f51a078','I006','NGUYEN GIA BAO', '962 DUONG 3/2','565','0090281','330000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','ADSL','',0,'01-01-2012','system');

 ----CABLE TV----
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('50ef8dd6-7543-4a1b-b601-d1e342360c24','TV001','TRAN VAN AN', '6767 Tran Dinh xu','123456','0090281','200000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('e2eeee7f-01eb-4d29-bf4a-e3095d87928c','TV002','TRAN VAN AN', '344 HOANG HOA THAM','5523457','0095656','251000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('b8c0ce95-063b-40f1-879f-73dda202e3ba','TV003','NGUYEN VAN BINH AN', '556 LE VAN HUU','565','1190281','145700','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('a658f456-7bf5-4953-8c85-d960916f5a92','TV004','LE THANH BINH', '90/3 NGUYEN VAN CU','65656','0790881','122100','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('f00c14eb-e835-4820-9014-4af56f050a62','TV005','TRAN THAN HUONG', '12/3/3 NGUYEN DINH CHIEU','6565','198000','3361469','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('d4d9d701-ed4f-4ce2-a389-ab059240e8b3','TV006','NGUYEN GIA BAO', '962 DUONG 3/2','565','0090281','330000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','CABLETV','',0,'01-01-2012','system');


 ----VIETTEL MOBILE----
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('66f89e85-56cf-45ed-89dd-218ae93563dc','VT001','TRAN VAN AN', '6767 Tran Dinh xu','123456','3535656','200000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('3c4ab959-223f-4442-980e-8944550c2519','VT002','TRAN VAN AN', '344 HOANG HOA THAM','5523457','35656','251000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('302773f3-e4d9-4119-a5d1-8d02a47fdf69','VT003','NGUYEN VAN BINH AN', '556 LE VAN HUU','565','56565','145700','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('e7827c5d-7d94-4b85-9f08-b3ab3c5dd698','VT004','LE THANH BINH', '90/3 NGUYEN VAN CU','65656','089786','122100','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('52dac845-8b77-46d2-a29c-d5a708592ca3','VT005','TRAN THAN HUONG', '12/3/3 NGUYEN DINH CHIEU','1212','198000','3361469','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');
 
 INSERT INTO BillInfoSimulator (BillInfoId,CustomerCode,CustomerName,[Address],BillId,BillNumber,DueAmount,DueDate,IssuedDate,Period,ChargeFrom,ChargeTo,EnabledOverPay,[Status],Currency,ServiceType,SubcriptionNumber,OverPaidAmount,CreatedDate,CreatedBy)
 VALUES('28f2b6af-4391-4312-9225-40efe782f29e','VT006','NGUYEN GIA BAO', '962 DUONG 3/2','565','54545','156000','20111219','20111215','01/2012','20111115','20111215','Y','U','VND','MOBILE','',0,'01-01-2012','system');


 ---CIF---
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('52e1ec6d-90f0-411c-aa2d-7d75ca586e33', '0000000001', '000000001','','', '01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('0dd33d87-c8fa-477c-abad-630f5416bb6b', '0000000002', '000000002','','', '01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('5ba53452-58a7-47a1-a0e1-e9721e6088a7', '0000000003', '000000003','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('21e17947-098b-48de-be3c-a5e88b9cec43', '0000000004', '000000004','','', '01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('13860b8c-95dd-4b4b-a8e6-2efece78a7e8', '0000000005', '000000005','','','01-01-2012','system');

 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('63b2a87a-09b8-4bf2-b7e9-80cb1bb6496e', '0000000006', '000000006','','', '01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('a1ce222d-7f5c-4895-aefa-925bc1258cd7', '0000000007', '000000007','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('92137027-db5d-4a94-95f8-3468f4ec7a0d', '0000000008', '000000008','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('62669f21-325f-471b-9e90-d15998da1ad5', '0000000009', '000000009','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('8ff72fc0-455b-4224-a1b3-0450196afd8c', '0000000010', '000000010','','','01-01-2012','system');

 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('e8f09c42-7dce-4c06-9c75-0241503266c4', '0000000011', '000000011','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('1603e88e-75da-4a56-968c-0609e3c0c560', '0000000012', '000000012','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('098a8844-6a99-4734-8d42-205d6487fdd9', '0000000013', '000000013','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('18bbe48e-1519-4606-8072-9bdf55745ad7', '0000000014', '000000014','','','01-01-2012','system');
 INSERT INTO AccountCif (Id, CIF, SSN, [PassPort], [Address], CreatedDate, CreatedBy) VALUES ('13860b8c-95dd-4b4b-a8e6-2efece78a7e8', '0000000015', '000000015','','','01-01-2012','system');

IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION

INSERT INTO SystemVersion(SysVersion,VersionName,UpdateDate,VersionDescription)
VALUES('1.001','Deploy Data script',GETDATE(),'database  for Gateway') 
COMMIT TRANSACTION