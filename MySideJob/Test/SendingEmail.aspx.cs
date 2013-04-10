using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Test
{
    public partial class TestSendingEmail : System.Web.UI.Page
    {

        public enum Message
        {
            Notification,
            Warning,
            NewOption,
            NewOpportunity,
            Refund,
            Blocked
        };
        public int BidderLCID { get; set; }
        public string BidderUsername { get; set; }
        public string BidderRole { get; set; }
        public int BidderID { get; set; }
        public string BidderEmailAddress { get; set; }
        public int PosterLCID { get; set; }
        public string PosterUsername { get; set; }
        public string PosterRole { get; set; }
        public int PosterID { get; set; }
        public string PosterEmailAddress { get; set; }
        public int ProjectID { get; set; }
        public int ProjectLCID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = 5;
            
        }
        protected void SendEmail(string userEmail, string userName, int lcid, int reminderLevel, string templateUrl, Message messageType)
        {
            //var url = "http://www.my-side-job.com/Schedule/MySideJob/EmailTemplates/PasswordRecovery.aspx?psw=" + psw +
            //          "&usn=" + username + "&l=" + lang;
            // var lang = Request.QueryString["l"] ?? "en-US";
            string url = "";
            switch (messageType)
            {
                case Message.Notification:

                    url = templateUrl + "?&l=" + GetLanguage(lcid) + "&usn=" + userName + "&prid=" + ProjectID + "&bidder=" + BidderUsername + "&rem=" + reminderLevel;
                    break;

                case Message.Warning:

                    url = templateUrl + "?&l=" + GetLanguage(lcid) + "&usn=" + userName + "&prid=" + ProjectID + "&rem=" + reminderLevel;
                    break;

                case Message.NewOption:
                    url = templateUrl + "?&l=" + GetLanguage(lcid) + "&usn=" + userName + "&prid=" + ProjectID + "&rem=" + reminderLevel;
                    break;

                case Message.NewOpportunity:
                    url = templateUrl + "?&l=" + GetLanguage(lcid) + "&usn=" + userName + "&prid=" + ProjectID + "&rem=" + reminderLevel;
                    break;

            }

            const string strFrom = "postmaster@my-side-job.com";
            var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
                              {
                                  BodyEncoding = Encoding.Default,
                                  Subject = Resources.Resource.ForgotPassword,
                                  Body = GetHtmlFrom(url),
                                  Priority = MailPriority.High,
                                  IsBodyHtml = true
                              };
            var smtpMail = new SmtpClient();
            var basicAuthenticationInfo = new NetworkCredential("postmaster@my-side-job.com", "haithem759163");
            smtpMail.Host = "mail.my-side-job.com";
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = basicAuthenticationInfo;
            try
            {
                smtpMail.Send(mailMsg);
            }
            catch (Exception)
            {
                Response.Redirect(Request.Url.ToString());
                throw;
            }
        }

        protected void Button1Click(object sender, EventArgs e)
        {
            BidderEmailAddress = "haithemaraissia@yahoo.com";
            BidderUsername = "Jack";
            BidderLCID = 1;

            SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, 1,
                               "http://www.my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerReminder.aspx", Message.Warning);
       Response.Write("Email Sent");
        }

        public static string GetHtmlFrom(string url)
        {
            var wc = new WebClient();
            var resStream = wc.OpenRead(url);
            if (resStream != null)
            {
                var sr = new StreamReader(resStream, System.Text.Encoding.Default);
                return sr.ReadToEnd();
            }
            return "null";
        }


        public static int GetLCID(string lang)
        {
            switch (lang)
            {

                case "en-US":
                    return 1;

                    break;
                case "fr":
                    return 2;

                    break;
                case "es":
                    return 3;

                    break;
                case "zh-CN":
                    return 4;

                    break;
                case "ru":
                    return 5;

                    break;
                case "ar":
                    return 6;

                    break;
                case "ja":
                    return 7;

                    break;
                case "de":
                    return 8;
                    break;

                default:
                    return 1;
                    break;
            }

        }

        public static string GetLanguage(int langid)
        {
            switch (langid)
            {

                case 1:
                    return "en-US";

                    break;
                case 2:
                    return "fr";

                    break;
                case 3:
                    return "es";

                    break;
                case 4:
                    return "zh-CN";

                    break;
                case 5:
                    return "ru";

                    break;
                case 6:
                    return "ar";

                    break;
                case 7:
                    return "ja";

                    break;
                case 8:
                    return "de";
                    break;

                default:
                    return "en-US";
                    break;
            }

        }

        public static string GetCurrentLCID(string lang)
        {
            if (lang != null | !string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                return GetLCID(lang).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return "1";
            }

        }
    }
}