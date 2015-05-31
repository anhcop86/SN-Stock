using System;
using System.Collections.Generic;
using System.Text;

using CoreSecurityService.Entities;
using CoreSecurityService.Data;

namespace CoreSecurityService.Business
{
    public class SessionCompanyService
    {
        public static SessionCompanyCollection GetSessionCompanyHastcList(DateTime UpdateTime)
        {
            try
            {
                SessionCompanyDAO sessionCompanyDAO = new SessionCompanyDAO();
                return sessionCompanyDAO.GetSessionCompanyHastcList(UpdateTime);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        }
        public static SessionCompanyCollection GetSessionCompanyHoseList(DateTime UpdateLETime)
        {
            try
            {
                SessionCompanyDAO sessionCompanyDAO = new SessionCompanyDAO();
                return sessionCompanyDAO.GetSessionCompanyHoseList(UpdateLETime);
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
                throw new ApplicationException(SR.BusinessGetCustomerListException, ex);
            }
        } 
    }
}
