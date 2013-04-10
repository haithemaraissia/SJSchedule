using System;
using System.Globalization;
using System.Threading;
using SidejobModel;
using System.Linq;


namespace EmailTemplates
{
    public partial class EmailTemplatesAutomatedMessage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            var lang = Request.QueryString["l"];
            if (lang != null | !string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Session["LCID"] = ScheduleUtility.GetLCID(lang);
            }
            else
            {
                if (Session["LCID"] != null)
                {
                    Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(ScheduleUtility.GetLanguage(Convert.ToInt32(Session["LCID"])));
                    Thread.CurrentThread.CurrentCulture =
                        CultureInfo.CreateSpecificCulture(ScheduleUtility.GetLanguage(Convert.ToInt32(Session["LCID"])));
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LCID"] == null)
            {
                Session["LCID"] = 1;
            }
            MessageProperties();
        }

        protected void MessageProperties()
        {
            var messageId = Convert.ToInt32(Request.QueryString["id"]);

            if ( messageId != 0)
            {
                using (var context = new SidejobEntities())
                {
                    var message = (from c in context.AutomatedMessages
                                   where c.MessageID == messageId
                                   select c).FirstOrDefault();

                    if ( message != null)
                    {
                        EmailTitle.Text = message.Title;
                        EmailMessage.Text = message.Message;
                    }
                }

            }
        }

    }
}