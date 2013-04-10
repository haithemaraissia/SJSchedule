using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using SidejobModel;

namespace EmailTemplates.Customer
{
    public partial class EmailTemplatesCustomerCustomerBlocked : System.Web.UI.Page
    {
        public int ProjectID { get; set; }
        public int PDTID { get; set; }
        public int CustomerID { get; set; }

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
            const string singlespace = " ";
            ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
            CustomerID = Convert.ToInt32(Request.QueryString["cid"]);
            if (ProjectID == 0) return;
            ProjectNotification.Text = Resources.Resource.Notification;
            using (var context = new SidejobEntities())
            {
                var lockedcustomer = (from c in context.LockedCustomers
                                      where c.CustomerID == CustomerID
                                      select c).FirstOrDefault();
                if (lockedcustomer == null) return;
                NameLabel.Text = lockedcustomer.FirstName + singlespace + lockedcustomer.LastName;
            }
        }
    }
}