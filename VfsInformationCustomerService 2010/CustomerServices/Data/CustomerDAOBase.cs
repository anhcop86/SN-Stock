
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VfsCustomerService.Entities;

namespace VfsCustomerService.Data
{    
    public abstract class CustomerDAOBase
    {
        #region Common methods
        public virtual Customer CreateCustomerFromReader(IDataReader reader)
        {
            Customer item = new Customer();
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal("BranchCode"))) item.BranchCode = (string)reader["BranchCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("ContractNumber"))) item.ContractNumber = (string)reader["ContractNumber"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerId"))) item.CustomerId = (string)reader["CustomerId"];
                if (!reader.IsDBNull(reader.GetOrdinal("BrokerId"))) item.BrokerId = (int)reader["BrokerId"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerName"))) item.CustomerName = (string)reader["CustomerName"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerNameViet"))) item.CustomerNameViet = (string)reader["CustomerNameViet"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerType"))) item.CustomerType = (string)reader["CustomerType"];
                if (!reader.IsDBNull(reader.GetOrdinal("DomesticForeign"))) item.DomesticForeign = (string)reader["DomesticForeign"];
                if (!reader.IsDBNull(reader.GetOrdinal("Dob"))) item.Dob = (DateTime)reader["Dob"];
                if (!reader.IsDBNull(reader.GetOrdinal("Sex"))) item.Sex = (string)reader["Sex"];
                if (!reader.IsDBNull(reader.GetOrdinal("SignatureImage1"))) item.SignatureImage1 = (byte[])reader["SignatureImage1"];
                if (!reader.IsDBNull(reader.GetOrdinal("SignatureImage2"))) item.SignatureImage2 = (byte[])reader["SignatureImage2"];
                if (!reader.IsDBNull(reader.GetOrdinal("OpenDate"))) item.OpenDate = (DateTime)reader["OpenDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("CloseDate"))) item.CloseDate = (DateTime)reader["CloseDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("CardType"))) item.CardType = (int)reader["CardType"];
                if (!reader.IsDBNull(reader.GetOrdinal("CardIdentity"))) item.CardIdentity = (string)reader["CardIdentity"];
                if (!reader.IsDBNull(reader.GetOrdinal("CardDate"))) item.CardDate = (DateTime)reader["CardDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("CardIssuer"))) item.CardIssuer = (string)reader["CardIssuer"];
                if (!reader.IsDBNull(reader.GetOrdinal("Address"))) item.Address = (string)reader["Address"];
                if (!reader.IsDBNull(reader.GetOrdinal("AddressViet"))) item.AddressViet = (string)reader["AddressViet"];
                if (!reader.IsDBNull(reader.GetOrdinal("Tel"))) item.Tel = (string)reader["Tel"];
                if (!reader.IsDBNull(reader.GetOrdinal("Fax"))) item.Fax = (string)reader["Fax"];
                if (!reader.IsDBNull(reader.GetOrdinal("Mobile"))) item.Mobile = (string)reader["Mobile"];
                if (!reader.IsDBNull(reader.GetOrdinal("Mobile2"))) item.Mobile2 = (string)reader["Mobile2"];
                if (!reader.IsDBNull(reader.GetOrdinal("Email"))) item.Email = (string)reader["Email"];
                if (!reader.IsDBNull(reader.GetOrdinal("UserCreate"))) item.UserCreate = (int)reader["UserCreate"];
                if (!reader.IsDBNull(reader.GetOrdinal("DateCreate"))) item.DateCreate = (DateTime)reader["DateCreate"];
                if (!reader.IsDBNull(reader.GetOrdinal("UserModify"))) item.UserModify = (int)reader["UserModify"];
                if (!reader.IsDBNull(reader.GetOrdinal("DateModify"))) item.DateModify = (DateTime)reader["DateModify"];
                if (!reader.IsDBNull(reader.GetOrdinal("ProxyStatus"))) item.ProxyStatus = (bool)reader["ProxyStatus"];
                if (!reader.IsDBNull(reader.GetOrdinal("AccountStatus"))) item.AccountStatus = (string)reader["AccountStatus"];
                if (!reader.IsDBNull(reader.GetOrdinal("Notes"))) item.Notes = (string)reader["Notes"];
                if (!reader.IsDBNull(reader.GetOrdinal("WorkingAddress"))) item.WorkingAddress = (string)reader["WorkingAddress"];
                if (!reader.IsDBNull(reader.GetOrdinal("UserIntroduce"))) item.UserIntroduce = (int)reader["UserIntroduce"];
                if (!reader.IsDBNull(reader.GetOrdinal("AttitudePoint"))) item.AttitudePoint = (int)reader["AttitudePoint"];
                if (!reader.IsDBNull(reader.GetOrdinal("DepositPoint"))) item.DepositPoint = (int)reader["DepositPoint"];
                if (!reader.IsDBNull(reader.GetOrdinal("ActionPoint"))) item.ActionPoint = (int)reader["ActionPoint"];
                if (!reader.IsDBNull(reader.GetOrdinal("Country"))) item.Country = (string)reader["Country"];
                if (!reader.IsDBNull(reader.GetOrdinal("Website"))) item.Website = (string)reader["Website"];
                if (!reader.IsDBNull(reader.GetOrdinal("TaxCode"))) item.TaxCode = (string)reader["TaxCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("AccountType"))) item.AccountType = (string)reader["AccountType"];
                if (!reader.IsDBNull(reader.GetOrdinal("OrderType"))) item.OrderType = (string)reader["OrderType"];
                if (!reader.IsDBNull(reader.GetOrdinal("ReceiveReport"))) item.ReceiveReport = (string)reader["ReceiveReport"];
                if (!reader.IsDBNull(reader.GetOrdinal("ReceiveReportBy"))) item.ReceiveReportBy = (string)reader["ReceiveReportBy"];
                if (!reader.IsDBNull(reader.GetOrdinal("MarriageStatus"))) item.MarriageStatus = (string)reader["MarriageStatus"];
                if (!reader.IsDBNull(reader.GetOrdinal("KnowledgeLevel"))) item.KnowledgeLevel = (string)reader["KnowledgeLevel"];
                if (!reader.IsDBNull(reader.GetOrdinal("Job"))) item.Job = (string)reader["Job"];
                if (!reader.IsDBNull(reader.GetOrdinal("OfficeFunction"))) item.OfficeFunction = (string)reader["OfficeFunction"];
                if (!reader.IsDBNull(reader.GetOrdinal("OfficeTel"))) item.OfficeTel = (string)reader["OfficeTel"];
                if (!reader.IsDBNull(reader.GetOrdinal("OfficeFax"))) item.OfficeFax = (string)reader["OfficeFax"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeName"))) item.HusbandWifeName = (string)reader["HusbandWifeName"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeCardNumber"))) item.HusbandWifeCardNumber = (string)reader["HusbandWifeCardNumber"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeCardDate"))) item.HusbandWifeCardDate = (DateTime)reader["HusbandWifeCardDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeCardLocation"))) item.HusbandWifeCardLocation = (string)reader["HusbandWifeCardLocation"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeTel"))) item.HusbandWifeTel = (string)reader["HusbandWifeTel"];
                if (!reader.IsDBNull(reader.GetOrdinal("HusbandWifeEmail"))) item.HusbandWifeEmail = (string)reader["HusbandWifeEmail"];
                if (!reader.IsDBNull(reader.GetOrdinal("JoinStockMarket"))) item.JoinStockMarket = (string)reader["JoinStockMarket"];
                if (!reader.IsDBNull(reader.GetOrdinal("InvestKnowledge"))) item.InvestKnowledge = (string)reader["InvestKnowledge"];
                if (!reader.IsDBNull(reader.GetOrdinal("InvestedIn"))) item.InvestedIn = (string)reader["InvestedIn"];
                if (!reader.IsDBNull(reader.GetOrdinal("InvestTarget"))) item.InvestTarget = (string)reader["InvestTarget"];
                if (!reader.IsDBNull(reader.GetOrdinal("RiskAccepted"))) item.RiskAccepted = (string)reader["RiskAccepted"];
                if (!reader.IsDBNull(reader.GetOrdinal("InvestFund"))) item.InvestFund = (string)reader["InvestFund"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegatePersonName"))) item.DelegatePersonName = (string)reader["DelegatePersonName"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegatePersonFunction"))) item.DelegatePersonFunction = (string)reader["DelegatePersonFunction"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegatePersonCardNumber"))) item.DelegatePersonCardNumber = (string)reader["DelegatePersonCardNumber"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegateCardDate"))) item.DelegateCardDate = (DateTime)reader["DelegateCardDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegateCardLocation"))) item.DelegateCardLocation = (string)reader["DelegateCardLocation"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegatePersonTel"))) item.DelegatePersonTel = (string)reader["DelegatePersonTel"];
                if (!reader.IsDBNull(reader.GetOrdinal("DelegatePersonEmail"))) item.DelegatePersonEmail = (string)reader["DelegatePersonEmail"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancyName"))) item.ChiefAccountancyName = (string)reader["ChiefAccountancyName"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancyCI"))) item.ChiefAccountancyCI = (string)reader["ChiefAccountancyCI"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancyCD"))) item.ChiefAccountancyCD = (DateTime)reader["ChiefAccountancyCD"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancyIssuer"))) item.ChiefAccountancyIssuer = (string)reader["ChiefAccountancyIssuer"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancySign1"))) item.ChiefAccountancySign1 = (byte[])reader["ChiefAccountancySign1"];
                if (!reader.IsDBNull(reader.GetOrdinal("ChiefAccountancySign2"))) item.ChiefAccountancySign2 = (byte[])reader["ChiefAccountancySign2"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxyName"))) item.CaProxyName = (string)reader["CaProxyName"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxyCI"))) item.CaProxyCI = (string)reader["CaProxyCI"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxyCD"))) item.CaProxyCD = (DateTime)reader["CaProxyCD"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxyIssuer"))) item.CaProxyIssuer = (string)reader["CaProxyIssuer"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxySign1"))) item.CaProxySign1 = (byte[])reader["CaProxySign1"];
                if (!reader.IsDBNull(reader.GetOrdinal("CaProxySign2"))) item.CaProxySign2 = (byte[])reader["CaProxySign2"];
                if (!reader.IsDBNull(reader.GetOrdinal("CompanySign1"))) item.CompanySign1 = (byte[])reader["CompanySign1"];
                if (!reader.IsDBNull(reader.GetOrdinal("CompanySign2"))) item.CompanySign2 = (byte[])reader["CompanySign2"];
                if (!reader.IsDBNull(reader.GetOrdinal("TradeCode"))) item.TradeCode = (string)reader["TradeCode"];
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerAccount"))) item.CustomerAccount = (int)reader["CustomerAccount"];
                if (!reader.IsDBNull(reader.GetOrdinal("MobileSms"))) item.MobileSms = (string)reader["MobileSms"];
                if (!reader.IsDBNull(reader.GetOrdinal("IsHere"))) item.IsHere = (bool)reader["IsHere"];
                if (!reader.IsDBNull(reader.GetOrdinal("MoneyDepositeNumber"))) item.MoneyDepositeNumber = (string)reader["MoneyDepositeNumber"];
                if (!reader.IsDBNull(reader.GetOrdinal("MoneyDepositeLocation"))) item.MoneyDepositeLocation = (string)reader["MoneyDepositeLocation"];
                if (!reader.IsDBNull(reader.GetOrdinal("DepartmentId"))) item.DepartmentId = (int)reader["DepartmentId"];
                if (!reader.IsDBNull(reader.GetOrdinal("PublicCompanyManage"))) item.PublicCompanyManage = (string)reader["PublicCompanyManage"];
                if (!reader.IsDBNull(reader.GetOrdinal("PublicCompanyHolder"))) item.PublicCompanyHolder = (string)reader["PublicCompanyHolder"];
                if (!reader.IsDBNull(reader.GetOrdinal("ParentCompanyName"))) item.ParentCompanyName = (string)reader["ParentCompanyName"];
                if (!reader.IsDBNull(reader.GetOrdinal("ParentCompanyAddress"))) item.ParentCompanyAddress = (string)reader["ParentCompanyAddress"];
                if (!reader.IsDBNull(reader.GetOrdinal("ParentCompanyEmail"))) item.ParentCompanyEmail = (string)reader["ParentCompanyEmail"];
                if (!reader.IsDBNull(reader.GetOrdinal("ParentCompanyTel"))) item.ParentCompanyTel = (string)reader["ParentCompanyTel"];
                if (!reader.IsDBNull(reader.GetOrdinal("PostType"))) item.PostType = (short)reader["PostType"];
                if (!reader.IsDBNull(reader.GetOrdinal("ReOpenDate"))) item.ReOpenDate = (DateTime)reader["ReOpenDate"];
                if (!reader.IsDBNull(reader.GetOrdinal("UserTakeCared"))) item.UserTakeCared = (int)reader["UserTakeCared"];
                if (!reader.IsDBNull(reader.GetOrdinal("TypeID"))) item.TypeID = (int)reader["TypeID"];
                if (!reader.IsDBNull(reader.GetOrdinal("SendYN"))) item.SendYN = (string)reader["SendYN"];
                if (!reader.IsDBNull(reader.GetOrdinal("ReceiveRelatedStockEmail"))) item.ReceiveRelatedStockEmail = (string)reader["ReceiveRelatedStockEmail"];
                if (!reader.IsDBNull(reader.GetOrdinal("ReceiveRelatedStockSms"))) item.ReceiveRelatedStockSms = (string)reader["ReceiveRelatedStockSms"];
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateCustomerFromReaderException, ex);
            }
            return item;
        }
        #endregion
        
        #region CreateCustomer methods
            
        public virtual void CreateCustomer(Customer customer)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerInsert");
                
                database.AddInParameter(dbCommand, "@BranchCode", DbType.AnsiString, customer.BranchCode);
                database.AddInParameter(dbCommand, "@ContractNumber", DbType.AnsiString, customer.ContractNumber);
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, customer.CustomerId);
                database.AddInParameter(dbCommand, "@BrokerId", DbType.Int32, customer.BrokerId);
                database.AddInParameter(dbCommand, "@CustomerName", DbType.AnsiString, customer.CustomerName);
                database.AddInParameter(dbCommand, "@CustomerNameViet", DbType.String, customer.CustomerNameViet);
                database.AddInParameter(dbCommand, "@CustomerType", DbType.AnsiStringFixedLength, customer.CustomerType);
                database.AddInParameter(dbCommand, "@DomesticForeign", DbType.AnsiStringFixedLength, customer.DomesticForeign);
                database.AddInParameter(dbCommand, "@Dob", DbType.DateTime, customer.Dob);
                database.AddInParameter(dbCommand, "@Sex", DbType.AnsiStringFixedLength, customer.Sex);
                database.AddInParameter(dbCommand, "@SignatureImage1", DbType.Binary, customer.SignatureImage1);
                database.AddInParameter(dbCommand, "@SignatureImage2", DbType.Binary, customer.SignatureImage2);
                database.AddInParameter(dbCommand, "@OpenDate", DbType.DateTime, customer.OpenDate);
                database.AddInParameter(dbCommand, "@CloseDate", DbType.DateTime, customer.CloseDate);
                database.AddInParameter(dbCommand, "@CardType", DbType.Int32, customer.CardType);
                database.AddInParameter(dbCommand, "@CardIdentity", DbType.AnsiString, customer.CardIdentity);
                database.AddInParameter(dbCommand, "@CardDate", DbType.DateTime, customer.CardDate);
                database.AddInParameter(dbCommand, "@CardIssuer", DbType.AnsiString, customer.CardIssuer);
                database.AddInParameter(dbCommand, "@Address", DbType.AnsiString, customer.Address);
                database.AddInParameter(dbCommand, "@AddressViet", DbType.String, customer.AddressViet);
                database.AddInParameter(dbCommand, "@Tel", DbType.AnsiString, customer.Tel);
                database.AddInParameter(dbCommand, "@Fax", DbType.AnsiString, customer.Fax);
                database.AddInParameter(dbCommand, "@Mobile", DbType.AnsiString, customer.Mobile);
                database.AddInParameter(dbCommand, "@Mobile2", DbType.AnsiString, customer.Mobile2);
                database.AddInParameter(dbCommand, "@Email", DbType.AnsiString, customer.Email);
                database.AddInParameter(dbCommand, "@UserCreate", DbType.Int32, customer.UserCreate);
                database.AddInParameter(dbCommand, "@DateCreate", DbType.DateTime, customer.DateCreate);
                database.AddInParameter(dbCommand, "@UserModify", DbType.Int32, customer.UserModify);
                database.AddInParameter(dbCommand, "@DateModify", DbType.DateTime, customer.DateModify);
                database.AddInParameter(dbCommand, "@ProxyStatus", DbType.Boolean, customer.ProxyStatus);
                database.AddInParameter(dbCommand, "@AccountStatus", DbType.AnsiStringFixedLength, customer.AccountStatus);
                database.AddInParameter(dbCommand, "@Notes", DbType.String, customer.Notes);
                database.AddInParameter(dbCommand, "@WorkingAddress", DbType.String, customer.WorkingAddress);
                database.AddInParameter(dbCommand, "@UserIntroduce", DbType.Int32, customer.UserIntroduce);
                database.AddInParameter(dbCommand, "@AttitudePoint", DbType.Int32, customer.AttitudePoint);
                database.AddInParameter(dbCommand, "@DepositPoint", DbType.Int32, customer.DepositPoint);
                database.AddInParameter(dbCommand, "@ActionPoint", DbType.Int32, customer.ActionPoint);
                database.AddInParameter(dbCommand, "@Country", DbType.AnsiString, customer.Country);
                database.AddInParameter(dbCommand, "@Website", DbType.String, customer.Website);
                database.AddInParameter(dbCommand, "@TaxCode", DbType.AnsiString, customer.TaxCode);
                database.AddInParameter(dbCommand, "@AccountType", DbType.AnsiStringFixedLength, customer.AccountType);
                database.AddInParameter(dbCommand, "@OrderType", DbType.AnsiStringFixedLength, customer.OrderType);
                database.AddInParameter(dbCommand, "@ReceiveReport", DbType.AnsiStringFixedLength, customer.ReceiveReport);
                database.AddInParameter(dbCommand, "@ReceiveReportBy", DbType.AnsiStringFixedLength, customer.ReceiveReportBy);
                database.AddInParameter(dbCommand, "@MarriageStatus", DbType.AnsiStringFixedLength, customer.MarriageStatus);
                database.AddInParameter(dbCommand, "@KnowledgeLevel", DbType.AnsiStringFixedLength, customer.KnowledgeLevel);
                database.AddInParameter(dbCommand, "@Job", DbType.AnsiStringFixedLength, customer.Job);
                database.AddInParameter(dbCommand, "@OfficeFunction", DbType.String, customer.OfficeFunction);
                database.AddInParameter(dbCommand, "@OfficeTel", DbType.AnsiString, customer.OfficeTel);
                database.AddInParameter(dbCommand, "@OfficeFax", DbType.AnsiString, customer.OfficeFax);
                database.AddInParameter(dbCommand, "@HusbandWifeName", DbType.String, customer.HusbandWifeName);
                database.AddInParameter(dbCommand, "@HusbandWifeCardNumber", DbType.AnsiString, customer.HusbandWifeCardNumber);
                database.AddInParameter(dbCommand, "@HusbandWifeCardDate", DbType.DateTime, customer.HusbandWifeCardDate);
                database.AddInParameter(dbCommand, "@HusbandWifeCardLocation", DbType.String, customer.HusbandWifeCardLocation);
                database.AddInParameter(dbCommand, "@HusbandWifeTel", DbType.String, customer.HusbandWifeTel);
                database.AddInParameter(dbCommand, "@HusbandWifeEmail", DbType.String, customer.HusbandWifeEmail);
                database.AddInParameter(dbCommand, "@JoinStockMarket", DbType.AnsiString, customer.JoinStockMarket);
                database.AddInParameter(dbCommand, "@InvestKnowledge", DbType.AnsiStringFixedLength, customer.InvestKnowledge);
                database.AddInParameter(dbCommand, "@InvestedIn", DbType.AnsiStringFixedLength, customer.InvestedIn);
                database.AddInParameter(dbCommand, "@InvestTarget", DbType.AnsiStringFixedLength, customer.InvestTarget);
                database.AddInParameter(dbCommand, "@RiskAccepted", DbType.AnsiStringFixedLength, customer.RiskAccepted);
                database.AddInParameter(dbCommand, "@InvestFund", DbType.AnsiStringFixedLength, customer.InvestFund);
                database.AddInParameter(dbCommand, "@DelegatePersonName", DbType.String, customer.DelegatePersonName);
                database.AddInParameter(dbCommand, "@DelegatePersonFunction", DbType.String, customer.DelegatePersonFunction);
                database.AddInParameter(dbCommand, "@DelegatePersonCardNumber", DbType.AnsiString, customer.DelegatePersonCardNumber);
                database.AddInParameter(dbCommand, "@DelegateCardDate", DbType.DateTime, customer.DelegateCardDate);
                database.AddInParameter(dbCommand, "@DelegateCardLocation", DbType.String, customer.DelegateCardLocation);
                database.AddInParameter(dbCommand, "@DelegatePersonTel", DbType.AnsiString, customer.DelegatePersonTel);
                database.AddInParameter(dbCommand, "@DelegatePersonEmail", DbType.String, customer.DelegatePersonEmail);
                database.AddInParameter(dbCommand, "@ChiefAccountancyName", DbType.String, customer.ChiefAccountancyName);
                database.AddInParameter(dbCommand, "@ChiefAccountancyCI", DbType.AnsiString, customer.ChiefAccountancyCI);
                database.AddInParameter(dbCommand, "@ChiefAccountancyCD", DbType.DateTime, customer.ChiefAccountancyCD);
                database.AddInParameter(dbCommand, "@ChiefAccountancyIssuer", DbType.AnsiString, customer.ChiefAccountancyIssuer);
                database.AddInParameter(dbCommand, "@ChiefAccountancySign1", DbType.Binary, customer.ChiefAccountancySign1);
                database.AddInParameter(dbCommand, "@ChiefAccountancySign2", DbType.Binary, customer.ChiefAccountancySign2);
                database.AddInParameter(dbCommand, "@CaProxyName", DbType.String, customer.CaProxyName);
                database.AddInParameter(dbCommand, "@CaProxyCI", DbType.AnsiString, customer.CaProxyCI);
                database.AddInParameter(dbCommand, "@CaProxyCD", DbType.DateTime, customer.CaProxyCD);
                database.AddInParameter(dbCommand, "@CaProxyIssuer", DbType.AnsiString, customer.CaProxyIssuer);
                database.AddInParameter(dbCommand, "@CaProxySign1", DbType.Binary, customer.CaProxySign1);
                database.AddInParameter(dbCommand, "@CaProxySign2", DbType.Binary, customer.CaProxySign2);
                database.AddInParameter(dbCommand, "@CompanySign1", DbType.Binary, customer.CompanySign1);
                database.AddInParameter(dbCommand, "@CompanySign2", DbType.Binary, customer.CompanySign2);
                database.AddInParameter(dbCommand, "@TradeCode", DbType.AnsiString, customer.TradeCode);
                database.AddInParameter(dbCommand, "@CustomerAccount", DbType.Int32, customer.CustomerAccount);
                database.AddInParameter(dbCommand, "@MobileSms", DbType.AnsiString, customer.MobileSms);
                database.AddInParameter(dbCommand, "@IsHere", DbType.Boolean, customer.IsHere);
                database.AddInParameter(dbCommand, "@MoneyDepositeNumber", DbType.AnsiString, customer.MoneyDepositeNumber);
                database.AddInParameter(dbCommand, "@MoneyDepositeLocation", DbType.String, customer.MoneyDepositeLocation);
                database.AddInParameter(dbCommand, "@DepartmentId", DbType.Int32, customer.DepartmentId);
                database.AddInParameter(dbCommand, "@PublicCompanyManage", DbType.String, customer.PublicCompanyManage);
                database.AddInParameter(dbCommand, "@PublicCompanyHolder", DbType.String, customer.PublicCompanyHolder);
                database.AddInParameter(dbCommand, "@ParentCompanyName", DbType.String, customer.ParentCompanyName);
                database.AddInParameter(dbCommand, "@ParentCompanyAddress", DbType.String, customer.ParentCompanyAddress);
                database.AddInParameter(dbCommand, "@ParentCompanyEmail", DbType.String, customer.ParentCompanyEmail);
                database.AddInParameter(dbCommand, "@ParentCompanyTel", DbType.AnsiString, customer.ParentCompanyTel);
                database.AddInParameter(dbCommand, "@PostType", DbType.Int16, customer.PostType);
                database.AddInParameter(dbCommand, "@ReOpenDate", DbType.DateTime, customer.ReOpenDate);
                database.AddInParameter(dbCommand, "@UserTakeCared", DbType.Int32, customer.UserTakeCared);
                database.AddInParameter(dbCommand, "@TypeID", DbType.Int32, customer.TypeID);
                database.AddInParameter(dbCommand, "@SendYN", DbType.String, customer.SendYN);
                database.AddInParameter(dbCommand, "@ReceiveRelatedStockEmail", DbType.String, customer.ReceiveRelatedStockEmail);
                database.AddInParameter(dbCommand, "@ReceiveRelatedStockSms", DbType.String, customer.ReceiveRelatedStockSms);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessCreateCustomerException, ex);
            }
        }

        #endregion

        #region UpdateCustomer methods
        
        public virtual void UpdateCustomer(Customer customer)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerUpdate");            
                
                database.AddInParameter(dbCommand, "@BranchCode", DbType.AnsiString, customer.BranchCode);
                database.AddInParameter(dbCommand, "@ContractNumber", DbType.AnsiString, customer.ContractNumber);
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, customer.CustomerId);
                database.AddInParameter(dbCommand, "@BrokerId", DbType.Int32, customer.BrokerId);
                database.AddInParameter(dbCommand, "@CustomerName", DbType.AnsiString, customer.CustomerName);
                database.AddInParameter(dbCommand, "@CustomerNameViet", DbType.String, customer.CustomerNameViet);
                database.AddInParameter(dbCommand, "@CustomerType", DbType.AnsiStringFixedLength, customer.CustomerType);
                database.AddInParameter(dbCommand, "@DomesticForeign", DbType.AnsiStringFixedLength, customer.DomesticForeign);
                database.AddInParameter(dbCommand, "@Dob", DbType.DateTime, customer.Dob);
                database.AddInParameter(dbCommand, "@Sex", DbType.AnsiStringFixedLength, customer.Sex);
                database.AddInParameter(dbCommand, "@SignatureImage1", DbType.Binary, customer.SignatureImage1);
                database.AddInParameter(dbCommand, "@SignatureImage2", DbType.Binary, customer.SignatureImage2);
                database.AddInParameter(dbCommand, "@OpenDate", DbType.DateTime, customer.OpenDate);
                database.AddInParameter(dbCommand, "@CloseDate", DbType.DateTime, customer.CloseDate);
                database.AddInParameter(dbCommand, "@CardType", DbType.Int32, customer.CardType);
                database.AddInParameter(dbCommand, "@CardIdentity", DbType.AnsiString, customer.CardIdentity);
                database.AddInParameter(dbCommand, "@CardDate", DbType.DateTime, customer.CardDate);
                database.AddInParameter(dbCommand, "@CardIssuer", DbType.AnsiString, customer.CardIssuer);
                database.AddInParameter(dbCommand, "@Address", DbType.AnsiString, customer.Address);
                database.AddInParameter(dbCommand, "@AddressViet", DbType.String, customer.AddressViet);
                database.AddInParameter(dbCommand, "@Tel", DbType.AnsiString, customer.Tel);
                database.AddInParameter(dbCommand, "@Fax", DbType.AnsiString, customer.Fax);
                database.AddInParameter(dbCommand, "@Mobile", DbType.AnsiString, customer.Mobile);
                database.AddInParameter(dbCommand, "@Mobile2", DbType.AnsiString, customer.Mobile2);
                database.AddInParameter(dbCommand, "@Email", DbType.AnsiString, customer.Email);
                database.AddInParameter(dbCommand, "@UserCreate", DbType.Int32, customer.UserCreate);
                database.AddInParameter(dbCommand, "@DateCreate", DbType.DateTime, customer.DateCreate);
                database.AddInParameter(dbCommand, "@UserModify", DbType.Int32, customer.UserModify);
                database.AddInParameter(dbCommand, "@DateModify", DbType.DateTime, customer.DateModify);
                database.AddInParameter(dbCommand, "@ProxyStatus", DbType.Boolean, customer.ProxyStatus);
                database.AddInParameter(dbCommand, "@AccountStatus", DbType.AnsiStringFixedLength, customer.AccountStatus);
                database.AddInParameter(dbCommand, "@Notes", DbType.String, customer.Notes);
                database.AddInParameter(dbCommand, "@WorkingAddress", DbType.String, customer.WorkingAddress);
                database.AddInParameter(dbCommand, "@UserIntroduce", DbType.Int32, customer.UserIntroduce);
                database.AddInParameter(dbCommand, "@AttitudePoint", DbType.Int32, customer.AttitudePoint);
                database.AddInParameter(dbCommand, "@DepositPoint", DbType.Int32, customer.DepositPoint);
                database.AddInParameter(dbCommand, "@ActionPoint", DbType.Int32, customer.ActionPoint);
                database.AddInParameter(dbCommand, "@Country", DbType.AnsiString, customer.Country);
                database.AddInParameter(dbCommand, "@Website", DbType.String, customer.Website);
                database.AddInParameter(dbCommand, "@TaxCode", DbType.AnsiString, customer.TaxCode);
                database.AddInParameter(dbCommand, "@AccountType", DbType.AnsiStringFixedLength, customer.AccountType);
                database.AddInParameter(dbCommand, "@OrderType", DbType.AnsiStringFixedLength, customer.OrderType);
                database.AddInParameter(dbCommand, "@ReceiveReport", DbType.AnsiStringFixedLength, customer.ReceiveReport);
                database.AddInParameter(dbCommand, "@ReceiveReportBy", DbType.AnsiStringFixedLength, customer.ReceiveReportBy);
                database.AddInParameter(dbCommand, "@MarriageStatus", DbType.AnsiStringFixedLength, customer.MarriageStatus);
                database.AddInParameter(dbCommand, "@KnowledgeLevel", DbType.AnsiStringFixedLength, customer.KnowledgeLevel);
                database.AddInParameter(dbCommand, "@Job", DbType.AnsiStringFixedLength, customer.Job);
                database.AddInParameter(dbCommand, "@OfficeFunction", DbType.String, customer.OfficeFunction);
                database.AddInParameter(dbCommand, "@OfficeTel", DbType.AnsiString, customer.OfficeTel);
                database.AddInParameter(dbCommand, "@OfficeFax", DbType.AnsiString, customer.OfficeFax);
                database.AddInParameter(dbCommand, "@HusbandWifeName", DbType.String, customer.HusbandWifeName);
                database.AddInParameter(dbCommand, "@HusbandWifeCardNumber", DbType.AnsiString, customer.HusbandWifeCardNumber);
                database.AddInParameter(dbCommand, "@HusbandWifeCardDate", DbType.DateTime, customer.HusbandWifeCardDate);
                database.AddInParameter(dbCommand, "@HusbandWifeCardLocation", DbType.String, customer.HusbandWifeCardLocation);
                database.AddInParameter(dbCommand, "@HusbandWifeTel", DbType.String, customer.HusbandWifeTel);
                database.AddInParameter(dbCommand, "@HusbandWifeEmail", DbType.String, customer.HusbandWifeEmail);
                database.AddInParameter(dbCommand, "@JoinStockMarket", DbType.AnsiString, customer.JoinStockMarket);
                database.AddInParameter(dbCommand, "@InvestKnowledge", DbType.AnsiStringFixedLength, customer.InvestKnowledge);
                database.AddInParameter(dbCommand, "@InvestedIn", DbType.AnsiStringFixedLength, customer.InvestedIn);
                database.AddInParameter(dbCommand, "@InvestTarget", DbType.AnsiStringFixedLength, customer.InvestTarget);
                database.AddInParameter(dbCommand, "@RiskAccepted", DbType.AnsiStringFixedLength, customer.RiskAccepted);
                database.AddInParameter(dbCommand, "@InvestFund", DbType.AnsiStringFixedLength, customer.InvestFund);
                database.AddInParameter(dbCommand, "@DelegatePersonName", DbType.String, customer.DelegatePersonName);
                database.AddInParameter(dbCommand, "@DelegatePersonFunction", DbType.String, customer.DelegatePersonFunction);
                database.AddInParameter(dbCommand, "@DelegatePersonCardNumber", DbType.AnsiString, customer.DelegatePersonCardNumber);
                database.AddInParameter(dbCommand, "@DelegateCardDate", DbType.DateTime, customer.DelegateCardDate);
                database.AddInParameter(dbCommand, "@DelegateCardLocation", DbType.String, customer.DelegateCardLocation);
                database.AddInParameter(dbCommand, "@DelegatePersonTel", DbType.AnsiString, customer.DelegatePersonTel);
                database.AddInParameter(dbCommand, "@DelegatePersonEmail", DbType.String, customer.DelegatePersonEmail);
                database.AddInParameter(dbCommand, "@ChiefAccountancyName", DbType.String, customer.ChiefAccountancyName);
                database.AddInParameter(dbCommand, "@ChiefAccountancyCI", DbType.AnsiString, customer.ChiefAccountancyCI);
                database.AddInParameter(dbCommand, "@ChiefAccountancyCD", DbType.DateTime, customer.ChiefAccountancyCD);
                database.AddInParameter(dbCommand, "@ChiefAccountancyIssuer", DbType.AnsiString, customer.ChiefAccountancyIssuer);
                database.AddInParameter(dbCommand, "@ChiefAccountancySign1", DbType.Binary, customer.ChiefAccountancySign1);
                database.AddInParameter(dbCommand, "@ChiefAccountancySign2", DbType.Binary, customer.ChiefAccountancySign2);
                database.AddInParameter(dbCommand, "@CaProxyName", DbType.String, customer.CaProxyName);
                database.AddInParameter(dbCommand, "@CaProxyCI", DbType.AnsiString, customer.CaProxyCI);
                database.AddInParameter(dbCommand, "@CaProxyCD", DbType.DateTime, customer.CaProxyCD);
                database.AddInParameter(dbCommand, "@CaProxyIssuer", DbType.AnsiString, customer.CaProxyIssuer);
                database.AddInParameter(dbCommand, "@CaProxySign1", DbType.Binary, customer.CaProxySign1);
                database.AddInParameter(dbCommand, "@CaProxySign2", DbType.Binary, customer.CaProxySign2);
                database.AddInParameter(dbCommand, "@CompanySign1", DbType.Binary, customer.CompanySign1);
                database.AddInParameter(dbCommand, "@CompanySign2", DbType.Binary, customer.CompanySign2);
                database.AddInParameter(dbCommand, "@TradeCode", DbType.AnsiString, customer.TradeCode);
                database.AddInParameter(dbCommand, "@CustomerAccount", DbType.Int32, customer.CustomerAccount);
                database.AddInParameter(dbCommand, "@MobileSms", DbType.AnsiString, customer.MobileSms);
                database.AddInParameter(dbCommand, "@IsHere", DbType.Boolean, customer.IsHere);
                database.AddInParameter(dbCommand, "@MoneyDepositeNumber", DbType.AnsiString, customer.MoneyDepositeNumber);
                database.AddInParameter(dbCommand, "@MoneyDepositeLocation", DbType.String, customer.MoneyDepositeLocation);
                database.AddInParameter(dbCommand, "@DepartmentId", DbType.Int32, customer.DepartmentId);
                database.AddInParameter(dbCommand, "@PublicCompanyManage", DbType.String, customer.PublicCompanyManage);
                database.AddInParameter(dbCommand, "@PublicCompanyHolder", DbType.String, customer.PublicCompanyHolder);
                database.AddInParameter(dbCommand, "@ParentCompanyName", DbType.String, customer.ParentCompanyName);
                database.AddInParameter(dbCommand, "@ParentCompanyAddress", DbType.String, customer.ParentCompanyAddress);
                database.AddInParameter(dbCommand, "@ParentCompanyEmail", DbType.String, customer.ParentCompanyEmail);
                database.AddInParameter(dbCommand, "@ParentCompanyTel", DbType.AnsiString, customer.ParentCompanyTel);
                database.AddInParameter(dbCommand, "@PostType", DbType.Int16, customer.PostType);
                database.AddInParameter(dbCommand, "@ReOpenDate", DbType.DateTime, customer.ReOpenDate);
                database.AddInParameter(dbCommand, "@UserTakeCared", DbType.Int32, customer.UserTakeCared);
                database.AddInParameter(dbCommand, "@TypeID", DbType.Int32, customer.TypeID);
                database.AddInParameter(dbCommand, "@SendYN", DbType.String, customer.SendYN);
                database.AddInParameter(dbCommand, "@ReceiveRelatedStockEmail", DbType.String, customer.ReceiveRelatedStockEmail);
                database.AddInParameter(dbCommand, "@ReceiveRelatedStockSms", DbType.String, customer.ReceiveRelatedStockSms);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessUpdateCustomerException, ex);
            }
        }
        
        #endregion

        #region DeleteCustomer methods
        public virtual void DeleteCustomer(string customerId)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerDelete");
                
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, customerId);
                
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessDeleteCustomerException, ex);
            }
        }

        #endregion

        #region GetCustomer methods
        
        public virtual Customer GetCustomer(string customerId)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerGet");
                
                database.AddInParameter(dbCommand, "@CustomerId", DbType.AnsiString, customerId);
                
                Customer customer =  null;
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        customer = CreateCustomerFromReader(reader);
                        reader.Close();
                    }
                }
                return customer;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerException, ex);
            }
        }
        
        #endregion

        #region GetCustomerList methods
        public virtual CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerGetList");
                
                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);
                
                CustomerCollection customerCollection = new CustomerCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer customer = CreateCustomerFromReader(reader);
                        customerCollection.Add(customer);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return customerCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }
        
        public virtual CustomerCollection GetCustomerList(CustomerColumns orderBy, string orderDirection)
        {            
            int totalRecords = 0;
            return GetCustomerList(orderBy, orderDirection, 0, 0, out totalRecords);
        }
        
        #endregion

        #region extend

        public virtual CustomerCollection GetCustomerListSearch(int TypeId, string CustomerId, string CustomerName, string CustomerNameViet, string Email,string mobile, CustomerColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomersGetListSearch");

                database.AddInParameter(dbCommand, "@TypeId", DbType.Int32, TypeId);
                database.AddInParameter(dbCommand, "@CustomerId", DbType.String, CustomerId.ToString());
                database.AddInParameter(dbCommand, "@CustomerName", DbType.String, CustomerName.ToString());
                database.AddInParameter(dbCommand, "@CustomerNameViet", DbType.String, CustomerNameViet.ToString());
                database.AddInParameter(dbCommand, "@Email", DbType.String, Email.ToString());
                database.AddInParameter(dbCommand, "@Mobile", DbType.String, mobile.ToString());

                database.AddInParameter(dbCommand, "@OrderBy", DbType.AnsiString, orderBy.ToString());
                database.AddInParameter(dbCommand, "@OrderDirection", DbType.AnsiString, orderDirection.ToString());
                database.AddInParameter(dbCommand, "@Page", DbType.Int32, page);
                database.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
                database.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 4);

                CustomerCollection customerCollection = new CustomerCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer customer = CreateCustomerFromReader(reader);
                        customerCollection.Add(customer);
                    }
                    reader.Close();
                }
                totalRecords = (int)database.GetParameterValue(dbCommand, "@TotalRecords");
                return customerCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }

        #endregion

        public virtual CustomerCollection GetCustomerListByEmail(string email)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase("CustommerServiceConnection");
                DbCommand dbCommand = database.GetStoredProcCommand("spCustomerGetListByEmail");

                database.AddInParameter(dbCommand, "@email", DbType.String, email);                

                CustomerCollection customerCollection = new CustomerCollection();
                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer customer = CreateCustomerFromReader(reader);
                        customerCollection.Add(customer);
                    }
                    reader.Close();
                }                
                return customerCollection;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.DataAccessGetCustomerListException, ex);
            }
        }
    }
}