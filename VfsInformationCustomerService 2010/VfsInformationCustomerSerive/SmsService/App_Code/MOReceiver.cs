using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using VietFrist.Db;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;
using VfsCustomerService.Utility;
using Vfs.Sms.Utility;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]

public class MOReceiver : System.Web.Services.WebService
{
    public MOReceiver()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }   

    [WebMethod]
    public string messageReceiver(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password)
    {               
        try
        {
            return SmsService.ReceviveIncomingMessage(UserID, 
                                                       ServiceID, 
                                                       CommandCode, 
                                                       Message, 
                                                       RequestID, 
                                                       MoID, 
                                                       Username, 
                                                       Password,
                                                       Convert.ToInt32(ApplicationHelper.TransferMoneyContentTemplateId),
                                                       ApplicationHelper.EmailTemp, 
                                                       Convert.ToInt32(ApplicationHelper.InvalidSMSContentTemplateId),
                                                       Convert.ToInt32(ApplicationHelper.ReplyRejectRelatedMessageContentTemplateId),
                                                       Convert.ToInt32(ApplicationHelper.InvalidAccountSMSContentTemplateId));            
        }
        catch (Exception ex)
        {
            Log(DateTime.Now.ToString() + " - " + ex.Message + ApplicationHelper.DoMD5("vfsuser") + " " + ApplicationHelper.DoMD5("vfsuser2009sms353"));
        }
        return "-1";
    }

    private void Log(string mess)
    {
        try
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            FileStream fs = new FileStream(path + "log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(mess);
            writer.Flush();
            writer.Close();
            fs.Close();
        }
        catch
        {
        }
    }
    
}
