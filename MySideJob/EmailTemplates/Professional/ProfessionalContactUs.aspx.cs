using System;
using System.Globalization;
using System.Threading;

namespace EmailTemplates.Professional
{
    public partial class EmailTemplatesProfessionalProfessionalContactUs : System.Web.UI.Page
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
            var id = Request.QueryString["id"];
            if (id != null)
            {
                ProfessionalID.Text = id;
            }
            var message = Request.QueryString["msg"];
            if (message != null)
            {
                Message.Text = message;
            }
        }
    }
}