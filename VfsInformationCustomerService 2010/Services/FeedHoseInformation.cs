using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VfsInformationFeedService.Configuration;
using System.Configuration;

using VfsCustomerService.Business;

namespace VfsInformationFeedService
{
    public class FeedHoseInformation : MainThread
    {
        public override void Execute()
        {
            int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            int commandBlockSize = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfItem"]);
            string userName = ConfigurationManager.AppSettings["SmsUserName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];

            while (true)
            {
                try
                {
                    InformationFeedSession informationFeedSession = new InformationFeedSession(commandBlockSize);
                    SendEmailSession sendEmailSession = new SendEmailSession(commandBlockSize);
                    SendSMSSession smsSession = new SendSMSSession(commandBlockSize, userName, password);

                    Ultility.Info("----- Begin Sending email -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    sendEmailSession.SendAllMailNotStart();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of sending email-----");

                    Ultility.Info("----- Begin Sending SMS -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    smsSession.SendAllSMSNotStart();                    
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of sending SMS-----");
                   
                    Ultility.Info("----- Begin Feed Hose information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedHoseNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding Hose information -----");

                    Ultility.Info("----- Begin Feed Hnx information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedHnxNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding Hnx information -----");                   

                    Ultility.Info("----- Begin Feed Cafef information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedCafefNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding Cafef information -----");

                    /*Ultility.Info("----- Begin Feed VnEconomy information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedVnEconomyNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding VnEconomy information -----"); 
                    */
                    Ultility.Info("----- Begin Feed Stox information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedStoxNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding Stox information -----");

                    Ultility.Info("----- Begin Feed VSD information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    informationFeedSession.FeedVsdNews();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding VSD information -----");
                    /*
                    Ultility.Info("----- Begin Create birthday sms -----");
                    SmsService.SendBirthdaySmsMessage(Convert.ToInt32(ConfigurationManager.AppSettings["BirthdaySmsTemplate"].ToString()));
                    Ultility.Info("----- End of creating birthday sms -----");
                    */
                    Thread.Sleep(intervalMinutes * 1000 * 60);
                }
                catch (Exception ex)
                {
                    Ultility.Error(ex);
                }
            }
        }

        private void LogReport(InformationFeedSession informationFeedSession)
        {
            //StringBuilder result = new StringBuilder();
            //string dateTimeFormatString = "yyyy/MM/dd HH:mm:ss";

            //if (sendEmailSession.FinishSendEmailTime == DateTime.MaxValue)
            //{
            //    result.AppendFormat("SENDING EMAIL FAIL");
            //    for (int i = 0; i < sendEmailSession.ListError.Count; i++)
            //    {
            //        result.Append("\r\n");
            //        result.Append("Error: " + sendEmailSession.ListError[i].Message);
            //    }
            //    Logger.Error(result);
            //}
            //else
            //{
            //    if (sendEmailSession.ListError.Count == 0)
            //    {
            //        result.AppendFormat("SENDING EMAIL SUCCESS");
            //    }
            //    else
            //    {
            //        result.AppendFormat("SENDING EMAIL SUCCESS WITH {0} ERROR(S)", sendEmailSession.ListError.Count);

            //        SendEmailException sendEmailException = new SendEmailException();
            //        string errorMessage = "";
            //        for (int i = 0; i < sendEmailSession.ListError.Count; i++)
            //        {
            //            sendEmailException = (SendEmailException)sendEmailSession.ListError[i];
            //            errorMessage = string.Format("Error when sending email from: {0}; to: {1}; title: {2}", sendEmailException.EmailException.SenderEmail, sendEmailException.EmailException.ReceiverEmail, sendEmailException.EmailException.Subject);
            //            result.Append("\r\n");
            //            result.Append(errorMessage);
            //        }
            //    }

            //    result.Append("\r\n")
            //        .AppendFormat("    Start at: {0}", sendEmailSession.StartSendEmailTime.ToString(dateTimeFormatString))
            //        .AppendFormat("    Finish at: {0}", sendEmailSession.FinishSendEmailTime.ToString(dateTimeFormatString))
            //        .AppendFormat("    Number of emails: {0}", sendEmailSession.NumberOfEmails);
            //    Logger.Info(result);
            //}
        }

    }
}
