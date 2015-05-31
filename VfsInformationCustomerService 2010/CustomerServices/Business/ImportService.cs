using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using LumenWorks.Framework.IO.Csv;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Utility;
using System.Collections;
using System.Text.RegularExpressions;

namespace VfsCustomerService.Business
{
    public class ImportService
    {         
        private const char CSV_DELIMITER = '\t';   

        private static bool ValidateHeaders(string[] typicalHeaders, string[] headers)
        {
            return true;
        }

        public static void ReadCvsHeader(string csvFilePath, out string[] headers)
        {
            TextReader textReader = null;
            try
            {
                textReader = new StreamReader(csvFilePath, Encoding.Default);

                using (CsvReader csv = new CsvReader(textReader, true, CSV_DELIMITER))
                {
                    headers = csv.GetFieldHeaders();                    
                }
            }

            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        private static void ReadCvsContent(string csvFilePath, out List<string[]> data)
        {
            TextReader textReader = null;
            string[] headers;
            try
            {
                textReader = new StreamReader(csvFilePath, Encoding.Default);

                using (CsvReader csv = new CsvReader(textReader, true, CSV_DELIMITER))
                {
                    headers = csv.GetFieldHeaders();
                    data = new List<string[]>();
                    while (csv.ReadNextRecord())
                    {
                        string[] items = new string[headers.Length];
                        for (int i = 0; i < headers.Length; i++)
                        {
                            items[i] = csv[i];
                        }
                        data.Add(items);
                    }
                }
            }

            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        private static void ReadCvsData(string csvFilePath, out string[] headers, out List<string[]> data)
        {
            TextReader textReader = null;
            try
            {
                textReader = new StreamReader(csvFilePath, Encoding.Default);

                using (CsvReader csv = new CsvReader(textReader, true, CSV_DELIMITER))
                {
                    headers = csv.GetFieldHeaders();
                    data = new List<string[]>();
                    while (csv.ReadNextRecord())
                    {
                        string[] items = new string[headers.Length];
                        for (int i = 0; i < headers.Length; i++)
                        {
                            items[i] = csv[i];
                        }
                        data.Add(items);
                    }
                }
            }

            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        public static string UploadDocument(Stream stream, string filePath, string fileName)
        {
            string uploadFileName;
            return uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
        }

        public static void CreateMessage(string filePath, string fileName, ContentTemplate contentTemplate, NameValueCollection parameters, out Int32 successCount, out List<string[]> notImportedData)
        {
            string[] typicalHeaders = null;
            string[] headers ;
            List<string[]> data = null;
            Hashtable hastableParameters = new Hashtable();
            notImportedData = new List<string[]>();
            successCount = 0;
            try
            {                
                ReadCvsHeader(filePath + fileName, out headers);          
                ReadCvsData(filePath + fileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {
                    //shareHolder.ShareHolderCode = items[0];
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    Customer customer = null;
                    string customerId = items[0].Replace("K", "C");
                    customer = CustomerService.GetCustomer(customerId);

                    if (customer == null)
                    {
                        notImportedData.Add(items);
                    }
                    else
                    {
                        if (contentTemplate.ServiceTypeID == 1)
                            messageContent.Receiver = customer.Email;
                        else
                            messageContent.Receiver = customer.Mobile;

                        messageContent.Subject = contentTemplate.Subject;
                        messageContent.BodyContentType = contentTemplate.BodyContentType;
                        messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                        hastableParameters = ConvertParametersToHastable(parameters);                                              
                        messageContent.BodyMessage = ReplaceBodyMessage(contentTemplate.BodyMessage, hastableParameters, items);                                                
                        messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                        messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                        messageContent.ModifiedDate = DateTime.Now;
                        messageContent.CreatedDate = DateTime.Now;
                        messageContent.Status = 0;

                        if (contentTemplate.ServiceTypeID == 1)
                        {
                            if (ValidateEmail(messageContent.Receiver) == true)
                            {   
                                MessageContentService.CreateMessageContent(messageContent);
                                ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                                foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                                {
                                    MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                                    messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                                    messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                                    messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                                    messageContentAttachement.ModifiedDate = DateTime.Now;
                                    messageContentAttachement.CreatedDate = DateTime.Now;
                                    MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                                }
                                successCount++;
                            }
                        }
                        else
                        {
                            MessageContentService.CreateMessageContent(messageContent);
                            ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                            foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                            {
                                MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                                messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                                messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                                messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                                messageContentAttachement.ModifiedDate = DateTime.Now;
                                messageContentAttachement.CreatedDate = DateTime.Now;
                                MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                            }
                            successCount++;
                        }
                    }
                }
                if (File.Exists(filePath + fileName))
                {
                    File.Delete(filePath + fileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_CreateMessageWithEmailException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }

        public static string PreviewMessage(string filePath, string fileName, ContentTemplate contentTemplate, NameValueCollection parameters)
        {
            string[] typicalHeaders = null;
            string[] headers;
            List<string[]> data = null;
            Hashtable hastableParameters = new Hashtable();
            string returnValue = string.Empty;
            try
            {
                ReadCvsHeader(filePath + fileName, out headers);
                ReadCvsData(filePath + fileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {                    
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    Customer customer = null;
                    string customerId = items[0].Replace("K", "C");
                    customer = CustomerService.GetCustomer(customerId);

                    if (customer != null)
                    {
                        if (contentTemplate.ServiceTypeID == 1)
                            messageContent.Receiver = customer.Email;
                        else
                            messageContent.Receiver = customer.Mobile;

                        messageContent.Subject = contentTemplate.Subject;
                        messageContent.BodyContentType = contentTemplate.BodyContentType;
                        messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                        hastableParameters = ConvertParametersToHastable(parameters);
                        messageContent.BodyMessage = ReplaceBodyMessage(contentTemplate.BodyMessage, hastableParameters, items);
                        messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                        messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                        messageContent.ModifiedDate = DateTime.Now;
                        messageContent.CreatedDate = DateTime.Now;
                        messageContent.Status = 0;
                        returnValue = messageContent.BodyMessage;
                        break;
                    }
                }              
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_CreateMessageWithEmailException, ex);
            }

            finally
            {
                if (data != null) data = null;               
            }
            return returnValue;
        }

        public static Hashtable GetParameters(ContentTemplate contentTemplate)
        {
            MatchCollection mc;
            Hashtable result = new Hashtable();
            // Create a new Regex object and define the regular expression.
            Regex r = new Regex("{#[a-zA-Z0-9]*#}");
            // Use the Matches method to find all matches in the input string.
            //            mc = r.Matches("123 {#abc#} 4a {#bc#} d");            
            mc = r.Matches(contentTemplate.BodyMessage);
            // Loop through the match collection to retrieve all 
            // matches and positions.
            for (int i = 0; i < mc.Count; i++)
            {
                result.Add(mc[i].Value, mc[i].Value);
            }
            return result;
        }

        private static string ReplaceBodyMessage(string input, Hashtable parameters, string[] items)
        {
            string text = input;
           
            MatchCollection matchColl = Regex.Matches(text, @"{#[a-z]*[0-9]*#}");
            string result1;
            result1 = text;
            int i = 0;
            
            Match match = Regex.Match(text, @"{#[a-z]*[0-9]*#}");
            while (match.Success)
            {
                object item;
                item = (object)parameters[match.ToString()];

                result1 = result1.Replace(match.ToString(), items[Convert.ToInt16(item)] );
                match = match.NextMatch();
                i++;
            }
            return result1;
        }

        private static string ReplaceBodyMessage(string input, string replacingString)
        {
            string text = input;

            MatchCollection matchColl = Regex.Matches(text, @"{#[a-z]*[0-9]*#}");
            string result1;
            result1 = text;
            int i = 0;

            Match match = Regex.Match(text, @"{#[a-z]*[0-9]*#}");
            while (match.Success)
            {
                result1 = result1.Replace(match.ToString(), replacingString);
                match = match.NextMatch();
                i++;
            }
            return result1;
        }

        public static bool CheckParamaterDuplication(string bodyMessage)
        {
            bool result = false;
            Hashtable resultHashtable = new Hashtable();
            MatchCollection mc;
            
            // Create a new Regex object and define the regular expression.
            Regex r = new Regex("{#[a-zA-Z0-9]*#}");
            // Use the Matches method to find all matches in the input string.
            //            mc = r.Matches("123 {#abc#} 4a {#bc#} d");            
            mc = r.Matches(bodyMessage);
            // Loop through the match collection to retrieve all 
            // matches and positions.            
            try
            {
                for (int i = 0; i < mc.Count; i++)
                {
                    resultHashtable.Add(mc[i].Value, mc[i].Value);                
                }
            }
            catch(Exception e)
            {
                return true;
            }
            return result;
        }

        public static void CreateMessageWithEmail(string[] data, ContentTemplate contentTemplate, ref int numberCustomerSent)
        {            
            foreach (string item in data)
            {
                //shareHolder.ShareHolderCode = items[0];
                if (item != string.Empty && item != null)
                {
                    numberCustomerSent++;
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    messageContent.Receiver = item;
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage;
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    //in case the message is type of email check if the email address is valid
                    if (messageContent.ServiceTypeID == 1)
                    {
                        if (ValidateEmail(messageContent.Receiver) == true)
                        {
                            MessageContentService.CreateMessageContent(messageContent);
                            ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                            foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                            {
                                MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                                messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                                messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                                messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                                messageContentAttachement.ModifiedDate = DateTime.Now;
                                messageContentAttachement.CreatedDate = DateTime.Now;
                                MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                            }
                        }
                    }
                    else
                    {
                        MessageContentService.CreateMessageContent(messageContent);
                        ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                        foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                        {
                            MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                            messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                            messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                            messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                            messageContentAttachement.ModifiedDate = DateTime.Now;
                            messageContentAttachement.CreatedDate = DateTime.Now;
                            MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                        }
                    }
                }
            }
        }

        public static void CreateMessage(CustomerCollection data, string[] contentToBeSent, ContentTemplate contentTemplate, ref int numberCustomerSent)
        {
            int i = 0;
            foreach (Customer item in data)
            {
                //shareHolder.ShareHolderCode = items[0];
                if (item != null)
                {
                    numberCustomerSent++;
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;                    
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = ReplaceBodyMessage(contentTemplate.BodyMessage, contentToBeSent[i++]);
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    //in case the message is type of email check if the email address is valid
                    if (messageContent.ServiceTypeID == 1)
                    {
                        messageContent.Receiver = item.Email;
                        if (ValidateEmail(messageContent.Receiver) == true)
                        {
                            MessageContentService.CreateMessageContent(messageContent);
                            ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                            foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                            {
                                MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                                messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                                messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                                messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                                messageContentAttachement.ModifiedDate = DateTime.Now;
                                messageContentAttachement.CreatedDate = DateTime.Now;
                                MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                            }
                        }
                    }
                    else
                    {
                        messageContent.Receiver = item.Mobile;
                        MessageContentService.CreateMessageContent(messageContent);
                        ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                        foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                        {
                            MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                            messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                            messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                            messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                            messageContentAttachement.ModifiedDate = DateTime.Now;
                            messageContentAttachement.CreatedDate = DateTime.Now;
                            MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                        }
                    }
                }
            }
        }

        public static void CreateMessage(Customer cust, string contentToBeSent, string contentToBeSentSms, ContentTemplate contentTemplate, ContentTemplate contentTemplateSms)
        {
            if (cust != null)
            {
                SimpleAES encryptEmail = new SimpleAES();
                MessageContent messageContent = null;
                messageContent = new MessageContent();
                messageContent.Sender = contentTemplate.Sender;
                messageContent.Subject = contentTemplate.Subject;
                messageContent.BodyContentType = contentTemplate.BodyContentType;
                messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                messageContent.BodyMessage = ReplaceBodyMessage(contentTemplate.BodyMessage, contentToBeSent);                
                messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                messageContent.ModifiedDate = DateTime.Now;
                messageContent.CreatedDate = DateTime.Now;
                messageContent.Status = 0;
                //in case the message is type of email check if the email address is valid
                if (cust.ReceiveRelatedStockEmail.Trim() == "Y")
                {
                    messageContent.Receiver = cust.Email;
                    messageContent.BodyMessage = messageContent.BodyMessage.Replace("$#email#", encryptEmail.EncryptToString(cust.Email));
                    if (ValidateEmail(messageContent.Receiver) == true)
                    {
                        MessageContentService.CreateMessageContent(messageContent);
                        ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                        foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                        {
                            MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                            messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                            messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                            messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                            messageContentAttachement.ModifiedDate = DateTime.Now;
                            messageContentAttachement.CreatedDate = DateTime.Now;
                            MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                        }
                    }
                }

                if (cust.ReceiveRelatedStockSms.Trim() == "Y")
                {
                    messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplateSms.Sender;
                    messageContent.Subject = contentTemplateSms.Subject;
                    messageContent.BodyContentType = contentTemplateSms.BodyContentType;
                    messageContent.BodyEncoding = contentTemplateSms.BodyEncoding;
                    messageContent.BodyMessage = ReplaceBodyMessage(contentTemplateSms.BodyMessage, contentToBeSentSms);
                    messageContent.ContentTemplateID = contentTemplateSms.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplateSms.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    messageContent.Receiver = cust.Mobile;
                    MessageContentService.CreateMessageContent(messageContent);
                    ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplateSms.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                    foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                    {
                        MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                        messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                        messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                        messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                        messageContentAttachement.ModifiedDate = DateTime.Now;
                        messageContentAttachement.CreatedDate = DateTime.Now;
                        MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                    }
                }
            }
        }

        public static void CreateMessage(Customer cust, string contentToBeSentSms,  ContentTemplate contentTemplateSms)
        {
                MessageContent messageContent = new MessageContent();
                if (cust.ReceiveRelatedStockSms.Trim() == "Y")
                {
                    messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplateSms.Sender;
                    messageContent.Subject = contentTemplateSms.Subject;
                    messageContent.BodyContentType = contentTemplateSms.BodyContentType;
                    messageContent.BodyEncoding = contentTemplateSms.BodyEncoding;
                    messageContent.BodyMessage = ReplaceBodyMessage(contentTemplateSms.BodyMessage, contentToBeSentSms);
                    messageContent.ContentTemplateID = contentTemplateSms.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplateSms.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    messageContent.Receiver = cust.Mobile;
                    MessageContentService.CreateMessageContent(messageContent);                   
                }
            }
        
        public static void CreateMessageWithEmailEncrypt(string[] data, ContentTemplate contentTemplate, ref int numberCustomerSent)
        {
            SimpleAES encryptEmail = new SimpleAES();

            foreach (string item in data)
            {
                //shareHolder.ShareHolderCode = items[0];
                if (item != string.Empty && item != null)
                {
                    numberCustomerSent++;
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    messageContent.Receiver = item;
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage.Replace("$#email#", encryptEmail.EncryptToString(item));
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;

                    MessageContentService.CreateMessageContent(messageContent);
                    ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                    foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                    {
                        MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                        messageContentAttachement.AttachementDescription = attachement.AttachementDescription;                                                                                       
                        messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                        messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                        messageContentAttachement.ModifiedDate = DateTime.Now;
                        messageContentAttachement.CreatedDate = DateTime.Now;
                        MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                    }
                }
            }
        }

        public static void CreateAccountForStox(Stream stream, string filePath, string fileName, ContentTemplate contentTemplate)
        {
            string[] typicalHeaders = { "Name", "Email", "IDStoxPro", "Password" };
            string[] headers ;
            List<string[]> data = null;
            try
            {
                string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
                ReadCvsData(filePath + uploadFileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {

                    //shareHolder.ShareHolderCode = items[0];
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;

                    messageContent.Receiver = items[1];
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage.Replace("#name#", items[0]).Replace("#username#", items[2]).Replace("#password#", items[3]);
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;

                    MessageContentService.CreateMessageContent(messageContent);
                    ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                    foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
                    {
                        MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                        messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                        messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                        messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                        messageContentAttachement.ModifiedDate = DateTime.Now;
                        messageContentAttachement.CreatedDate = DateTime.Now;
                        MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                    }
                }
                if (File.Exists(filePath + uploadFileName))
                {
                    File.Delete(filePath + uploadFileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_CreateMessageWithEmailException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }

        public static void CreateMessageWithEmail(Stream stream, string filePath, string fileName, ContentTemplate contentTemplate)
        {
            string[] typicalHeaders = {"Receiver"};
            string[] headers;
            List<string[]> data = null;
            try
            {
                string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
                ReadCvsData(filePath + uploadFileName, out headers, out data);

                if (!ValidateHeaders(typicalHeaders, headers))
                {
                    throw new InvalidDataException("CSV file is invalid format!");
                }

                foreach (string[] items in data)
                {

                    //shareHolder.ShareHolderCode = items[0];
                    MessageContent messageContent = null;
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    
                    messageContent.Receiver = items[0];
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage   = contentTemplate.BodyMessage;
                    messageContent.ContentTemplateID   = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID   = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate   = DateTime.Now;
                    messageContent.CreatedDate   = DateTime.Now;
                    messageContent.Status   = 0;

                    MessageContentService.CreateMessageContent(messageContent);
                    ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
                    foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)    
                    {
                        MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                        messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                        messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                        messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                        messageContentAttachement.ModifiedDate   = DateTime.Now;
                        messageContentAttachement.CreatedDate   = DateTime.Now;
                        MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
                    }
                }
                if (File.Exists(filePath + uploadFileName))
                {
                    File.Delete(filePath + uploadFileName);
                }
            }

            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessImport_CreateMessageWithEmailException, ex);
            }

            finally
            {
                if (data != null) data = null;
            }
        }

        public static Hashtable ConvertParametersToHastable(NameValueCollection parameters)
        {
            Hashtable result = new Hashtable();
            for (int i = 0; i < parameters.Count; i++) 
            {
                result.Add(parameters.GetKey(i), parameters[i]);                
            }
            return result;
        }

        private static int GetStockSymbolId(string symbol)
        {
            Int32 stockSymbolId = 0;
            /*stock_SymbolCollection stock_symbolCollection = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
            foreach (stock_Symbol stockSymbol in stock_symbolCollection)
            {
                if (stockSymbol.Symbol == symbol)
                {
                    stockSymbolId = stockSymbol.SymbolID;
                    break;
                }
            }*/
            return stockSymbolId;
        }

        public static void ImportCustomer(Stream stream, string filePath, string fileName)
        {
            string[] typicalHeaders = { "CustomerId", "CustomerNameViet", "Email", "AddressViet", "Mobile",};
            string[] headers;
            List<string[]> data;
            string uploadFileName = UploadService.UploadDocument(stream, filePath, fileName);
            ReadCvsData(filePath + uploadFileName, out headers, out data);
            if (!ValidateHeaders(typicalHeaders, headers))
            {
                throw new InvalidDataException("CSV file is invalid format!");
            }
            try
            {        
                foreach (string[] items in data)
                {
                    Customer customer = new Customer();
                    customer.CustomerId = items[0];
                    customer.CustomerNameViet = items[1];
                    customer.Email = items[2];
                    customer.AddressViet = items[3];
                    customer.Mobile = items[4];
                    customer.TypeID = 2;
                    if (CustomerService.GetCustomer(items[0]) == null)
                        CustomerService.CreateCustomer(customer);
                    else
                        CustomerService.UpdateCustomer(customer);
                }
            }
            catch
            (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessCreateContentParameterException, ex);
            }
            finally
            {
                if (data != null) data = null;
            }
        }

        public static void CreateMessageWhenNewCustomer(Customer customer, ContentTemplate contentTemplate, string MorE)
        {
            MessageContent messageContent = null;
            messageContent = new MessageContent();
            messageContent.Sender = contentTemplate.Sender;
            if(MorE == "M")
                messageContent.Receiver = customer.Mobile;
            else
                messageContent.Receiver = customer.Email;
            messageContent.Subject = contentTemplate.Subject;
            messageContent.BodyContentType = contentTemplate.BodyContentType;
            messageContent.BodyEncoding = contentTemplate.BodyEncoding;
            messageContent.BodyMessage = contentTemplate.BodyMessage.Replace("#name#", customer.CustomerName).Replace("#customerid#", customer.CustomerId);
            messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
            messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
            messageContent.ModifiedDate = DateTime.Now;
            messageContent.CreatedDate = DateTime.Now;
            messageContent.Status = 0;

            MessageContentService.CreateMessageContent(messageContent);
            ContentTemplateAttachementCollection contentTemplateAttachementColl = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC");
            foreach (ContentTemplateAttachement attachement in contentTemplateAttachementColl)
            {
                MessageContentAttachement messageContentAttachement = new MessageContentAttachement();
                messageContentAttachement.AttachementDescription = attachement.AttachementDescription;
                messageContentAttachement.AttachementDocument = attachement.AttachementDocument;
                messageContentAttachement.MessageContentID = messageContent.MessageContentID;
                messageContentAttachement.ModifiedDate = DateTime.Now;
                messageContentAttachement.CreatedDate = DateTime.Now;
                MessageContentAttachementService.CreateMessageContentAttachement(messageContentAttachement);
            }
        }

        public static bool ValidateEmail(string inputEmail)
        {
            if (inputEmail == string.Empty) return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }    
    }   
}
