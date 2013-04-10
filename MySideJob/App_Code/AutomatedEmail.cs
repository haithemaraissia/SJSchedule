using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SidejobModel;

/// <summary>
/// Summary description for AutomatedEmail
/// </summary>
public class AutomatedEmail
{
    public  AutomatedEmail()
    {
        
    }
    public void StartProcess()
    {
        AutomatedMessageProcessStart();
    }

    public void AutomatedMessageProcessStart()
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
                              m.Title, m.MessageID.ToString(CultureInfo.InvariantCulture));
                   // context.DeleteObject(m);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    CatchMessage(context, m);
                }
            }
        }
    }

    public void SendEmail(string userEmail, string templateUrl, string subject, string messageid)
    {
        string url = templateUrl + "?id=" + messageid;
        const string strFrom = "postmaster@my-side-job.com";
        var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
        {
            BodyEncoding = Encoding.UTF8,
            Subject = subject,
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
            CatchMessage(messageid);
        }
    }

    public void CatchMessage(SidejobEntities context, AutomatedMessage m)
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

    public void CatchMessage(string id)
    {
        using (var context = new SidejobEntities())
        {
            var messageProblem = new AutomationEmailProblem
                                     {
                                         MessageID = GetMessageID(context),
                                         EmailAddress = "From Sending Message",
                                         Title = "Go Find It",
                                         Message ="Check AutomatedMessageTable"
                                     };
            context.AddToAutomationEmailProblems(messageProblem);
            context.SaveChanges();
        }
    }

    public int GetMessageID(SidejobEntities context)
    {
        int messageId;
        var max = context.AutomationEmailProblems.OrderByDescending(s => s.MessageID).FirstOrDefault();

        if (max == null)
        {
            messageId = 0;
        }
        else
        {
            messageId = max.MessageID + 1;
        }
        return messageId;
    }
}