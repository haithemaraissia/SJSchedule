using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using SidejobModel;

public partial class AutomatedMessageProcess : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AutomatedMessageProcessStart();
    }

    private void AutomatedMessageProcessStart()
    {
        using (var context = new SidejobEntities())
        {
            var message = (from c in context.AutomatedMessages
                           select c).ToList();

            if (message.Count == 0) return;
            foreach (var m in message)
            {
                try
                {
                    SendEmail(m.EmailAddress,
                              "http://www.my-side-job.com/Schedule/MySideJob/EmailTemplates/AutomatedMessage.aspx",
                              m.MessageID.ToString(CultureInfo.InvariantCulture));
                    context.DeleteObject(m);
                }
                catch (Exception)
                {
                    CatchMessage(context, m);
                }
            }
        }
    }

    protected void SendEmail(string userEmail, string templateUrl, string messageid)
    {
        string url = templateUrl + "?id=" + messageid;
        const string strFrom = "postmaster@my-side-job.com";
        var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
        {
            BodyEncoding = Encoding.Default,
            Subject = Resources.Resource.ForgotPassword,
            Body = ScheduleUtility.GetHtmlFrom(url),
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

    protected void CatchMessage(SidejobEntities context, AutomatedMessage m)
    {

        var messageProblem = new AutomationEmailProblem
                                 {
                                     MessageID = m.MessageID,
                                     EmailAddress = m.EmailAddress,
                                     Title = m.Title,
                                     Message = m.Message
                                 };
        context.AddToAutomationEmailProblems(messageProblem);
        context.SaveChanges();
    }

}