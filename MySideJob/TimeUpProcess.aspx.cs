using System;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using SidejobModel;
using System.Linq;


public partial class TimeUpProcess : System.Web.UI.Page
{
    public enum Message
    {
        Automated
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        var timeupProcess = new TimeUp();
        timeupProcess.Start();

       //timeupProcess.Exit();

        var automatedMessage = new AutomatedEmail();
        automatedMessage.StartProcess();
        
    }

    //public void EmailAutomation()
    //{
    //    using (var context = new SidejobEntities())
    //    {
    //        var message = (from c in context.AutomatedMessages
    //                       select c).ToList();
    //        if (message.Count != 0)
    //        {
    //            foreach (var e in message)
    //            {
    //                try
    //                {
    //                    SendEmail(e.EmailAddress, e.Title, e.MessageID.ToString(CultureInfo.InvariantCulture), Server.MapPath("~/EmailTemplates/AutomatedMessage.aspx"), Message.Automated);
    //                }
    //                catch (Exception emailexception)
    //                {
    //                    var emailProblem = new AutomationEmailProblem
    //                                           {
    //                                               MessageID = e.MessageID,
    //                                               EmailAddress = e.EmailAddress,
    //                                               Title = e.Title,
    //                                               Message = e.Message
    //                                           };
    //                    context.AddToAutomationEmailProblems(emailProblem);
    //                }
    //            }
    //        }
    //        context.DeleteObject(message);
    //        context.SaveChanges();
    //    }
    //}

    //protected void SendEmail(string userEmail, string subject, string messageId, string templateUrl,
    //                     Message messageType)
    //{
    //    //var url = "http://www.my-side-job.com/Schedule/MySideJob/EmailTemplates/PasswordRecovery.aspx?psw=" + psw +
    //    //          "&usn=" + username + "&l=" + lang;
    //    // var lang = Request.QueryString["l"] ?? "en-US";

    //    var url = "";
    //    switch (messageType)
    //    {
    //        case Message.Automated:
    //            url = templateUrl + "?id=" + messageId;
    //            break;
    //    }

    //    const string strFrom = "postmaster@my-side-job.com";
    //    var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
    //    {
    //        BodyEncoding = Encoding.Default,
    //        Subject = subject,
    //        Body = ScheduleUtility.GetHtmlFrom(url),
    //        Priority = MailPriority.High,
    //        IsBodyHtml = true
    //    };
    //    var smtpMail = new SmtpClient();
    //    var basicAuthenticationInfo = new NetworkCredential("postmaster@my-side-job.com", "haithem759163");
    //    smtpMail.Host = "mail.my-side-job.com";
    //    smtpMail.UseDefaultCredentials = false;
    //    smtpMail.Credentials = basicAuthenticationInfo;
    //    try
    //    {
    //        smtpMail.Send(mailMsg);
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect(Request.Url.ToString());
    //        throw;
    //    }
    //}
}


