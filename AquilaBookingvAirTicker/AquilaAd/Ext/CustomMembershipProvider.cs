using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace AquilaAd.Ext
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            //System.Web.Security.MembershipUser currentUser = System.Web.Security.Membership.GetUser(System.Security.Principal.IPrincipal.User.Identity.Name, userIsOnline: true);
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRepo = new ShareHolderCore.Domain.Repositories.MembershipRepository();

            ShareHolderCore.Domain.Model.Membership CheckUsr = userRepo.GetByLoginId(username, CustomMembershipProvider.GetMD5Hash(oldPassword));
            if (CheckUsr == null)
            {
                throw new ApplicationException(Resources.UserInterface.InvalidOldPassword);
            }
            else
            {
                try
                {
                    string newPass = CustomMembershipProvider.GetMD5Hash(newPassword);
                    userRepo.ChangePassword(username, CustomMembershipProvider.GetMD5Hash(oldPassword), newPass);
                    ShareHolderCore.Domain.Model.Membership usr = userRepo.GetByLoginId(username);
                    if (usr != null)
                    {
                        if (string.IsNullOrEmpty(usr.Email) == false)
                        {
                            ChangePasswordEmail changePassEmail = new ChangePasswordEmail();
                            changePassEmail.FileName = ApplicationHelper.ChangePasswordEmailTemplatePath;
                            changePassEmail.IsHtmlMail = true;
                            changePassEmail.SmtpServer = ApplicationHelper.SmtpServer;
                            changePassEmail.SmtpPort = ApplicationHelper.SmtpPort;
                            changePassEmail.Sender = ApplicationHelper.ApplicationEmailAddress;
                            changePassEmail.SmtpEmail = ApplicationHelper.ApplicationEmailAddress;
                            changePassEmail.SmtpPassword = ApplicationHelper.ApplicationEmailPassword;
                            changePassEmail.Receiver = usr.Email;
                            changePassEmail.Subject = Resources.UserInterface.changePassEmailSubject;
                            changePassEmail.UseContentTemplate = true;
                            changePassEmail.SenderName = Resources.UserInterface.senderName;
                            changePassEmail.ReceiverName = usr.MemberName;
                            changePassEmail.UserName = usr.LoginId;
                            changePassEmail.Password = newPassword;
                            changePassEmail.Send();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (HttpContext.Current.Session["LoginByFacebook"] == null) HttpContext.Current.Session["LoginByFacebook"] = "";
            if (HttpContext.Current.Session["LoginByFacebook"].ToString() != "Y")
            {
                if (GetUserNameByEmail(email) == "Y" || !email.Contains("@"))
                {
                    status = MembershipCreateStatus.DuplicateEmail;
                    return null;
                }
            }



            MembershipUser user = GetUser(username, true);

            if (user == null)
            {
                ShareHolderCore.Domain.Model.Membership userObj = new ShareHolderCore.Domain.Model.Membership();
                userObj.MemberName = username;
                userObj.LoginId = username;
                userObj.Password = GetMD5Hash(password);
                userObj.Email = email;

                IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                userRep.Save(userObj);

                status = MembershipCreateStatus.Success;

                return GetUser(username, true);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
            ShareHolderCore.Domain.Model.Membership membership = userRep.GetByLoginId(username);

            if (membership != null)
            {
                MembershipUser memUser = new MembershipUser("CustomMembershipProvider", username, membership.MemberId, membership.Email,
                                                            string.Empty, string.Empty,
                                                            true, false, DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            string resual = "N";
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
            ShareHolderCore.Domain.Model.Membership membership = userRep.GetByEmail(email);

            if (membership != null)
            {
                resual = "Y";
            }

            return resual;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword(string username, string answer)
        {
            string newPass = this.RandomString(10, true);
            try
            {

                IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRepo = new ShareHolderCore.Domain.Repositories.MembershipRepository();
                ShareHolderCore.Domain.Model.Membership usr = userRepo.GetByEmail(username);

                if (usr == null) return "EmailNotFound";

                userRepo.ResetPassword(usr.Email, CustomMembershipProvider.GetMD5Hash(newPass));

                ChangePasswordEmail changePassEmail = new ChangePasswordEmail();
                changePassEmail.FileName = ApplicationHelper.ChangePasswordEmailTemplatePath;
                changePassEmail.IsHtmlMail = true;
                changePassEmail.SmtpServer = ApplicationHelper.SmtpServer;
                changePassEmail.SmtpPort = ApplicationHelper.SmtpPort;
                changePassEmail.Sender = ApplicationHelper.ApplicationEmailAddress;
                changePassEmail.SmtpEmail = ApplicationHelper.ApplicationEmailAddress;
                changePassEmail.SmtpPassword = ApplicationHelper.ApplicationEmailPassword;
                changePassEmail.Receiver = usr.Email;
                changePassEmail.Subject = Resources.UserInterface.changePassEmailSubject;
                changePassEmail.UseContentTemplate = true;
                changePassEmail.SenderName = Resources.UserInterface.senderName;
                changePassEmail.ReceiverName = usr.MemberName;
                changePassEmail.UserName = usr.LoginId;
                changePassEmail.Password = newPass;
                changePassEmail.Send();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return newPass;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            string sha1Pswd = GetMD5Hash(password);
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
            ShareHolderCore.Domain.Model.Membership userObj = membershipRepo.GetByLoginId(username, sha1Pswd);
            if (userObj != null)
            {
                //HttpContext.Current.Session["AdminLoginObject"] = userObj;
                
                userObj.LastLoginDate = DateTime.UtcNow.AddHours(7);
                membershipRepo.Update(userObj);
                return true;
            }
            return false;
        }

       

        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}