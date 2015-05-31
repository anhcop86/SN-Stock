
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class BirthdayMessageLogService
    {
        #region BirthdayMessageLog

        public static void CreateBirthdayMessageLog(BirthdayMessageLog birthdayMessageLog)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                birthdayMessageLogDAO.CreateBirthdayMessageLog(birthdayMessageLog);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessCreateBirthdayMessageLogException, ex);
            }
        }        

        public static void UpdateBirthdayMessageLog(BirthdayMessageLog birthdayMessageLog)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                birthdayMessageLogDAO.UpdateBirthdayMessageLog(birthdayMessageLog);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessUpdateBirthdayMessageLogException, ex);
            }
        }        

        public static void DeleteBirthdayMessageLog(string birthdayMessageDay)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                birthdayMessageLogDAO.DeleteBirthdayMessageLog(birthdayMessageDay);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessDeleteBirthdayMessageLogException, ex);
            }
        }
        
        public static BirthdayMessageLog GetBirthdayMessageLog(string birthdayMessageDay)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                return birthdayMessageLogDAO.GetBirthdayMessageLog(birthdayMessageDay);                
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetBirthdayMessageLogException, ex);
            }
        }                

        public static BirthdayMessageLogCollection GetBirthdayMessageLogList(BirthdayMessageLogColumns orderBy, string orderDirection)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                return birthdayMessageLogDAO.GetBirthdayMessageLogList(orderBy, orderDirection);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetBirthdayMessageLogListException, ex);
            }
        }        

        public static BirthdayMessageLogCollection GetBirthdayMessageLogList(BirthdayMessageLogColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                BirthdayMessageLogDAO birthdayMessageLogDAO = new BirthdayMessageLogDAO();
                return birthdayMessageLogDAO.GetBirthdayMessageLogList(orderBy, orderDirection, page, pageSize, out totalRecords);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetBirthdayMessageLogListException, ex);
            }
        }        

        #endregion
    }
}