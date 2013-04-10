using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using SidejobModel;

public partial class EmailTemplatesProfessionalProfessionalRefund : Page
{

        public int ProjectID { get; set; }
        public int PDTID { get; set; }
        public int ProfessionalID { get; set; }

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
            if (!Page.IsPostBack)
            {
                MessageProperties();
            }
        }

        protected void MessageProperties()
        {
            //GET THE PDTID FORM ARCHIVE REFUND
            const string singlespace = " ";
            const string startofBoldRed = "<span class='DarkRed'><strong>";
            const string lastofBoldRed = "</strong></span>";
            PDTID = Convert.ToInt32(Request.QueryString["pdtid"]);
            ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
            ProfessionalID = Convert.ToInt32(Request.QueryString["pid"]);
            if (ProjectID == 0) return;
            ProjectNotification.Text = Resources.Resource.Project + singlespace + ProjectID + singlespace +
                                       Resources.Resource.Notification;
            ConfirmationEmail.Text = Resources.Resource.RefundEmailMessage1 + singlespace
                                           + startofBoldRed + ProjectID + lastofBoldRed + singlespace +
                                           Resources.Resource.RefundEmailMessage2;
            using (var context = new SidejobEntities())
            {
                var archiveRefund = (from c in context.ArchivedRefundProfessionalSuccessfulPDTs
                                     where c.PDTID == PDTID
                                     select c).FirstOrDefault();
                if (archiveRefund == null) return;
                Amount.Text = archiveRefund.GrossTotal.ToString(CultureInfo.InvariantCulture) +
                              singlespace + archiveRefund.CurrencyCode.ToString(CultureInfo.InvariantCulture) + singlespace;
                Transaction.Text = archiveRefund.TransactionId;
                NameLabel.Text = archiveRefund.FirstName + singlespace + archiveRefund.LastName;
            }
        }
    }
