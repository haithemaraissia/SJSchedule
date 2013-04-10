using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using SidejobModel;

namespace EmailTemplates.Customer
{
    public partial class CustomerNewOption : System.Web.UI.Page
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

        public static int ProjectID { get; set; }
        public int CustomerID { get; set; }
        public string Role { get; set; }
        public string BidderRole { get; set; }
        public static int BidderID { get; set; }
        public int BidderLCID { get; set; }
        public string BidderUsername { get; set; }
        public string Biddermessage { get; set; }
        public string BidderEmailAddress { get; set; }
        public static int PreviousBidderID { get; set; }

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
                CustomLink();
            }

        }

        protected void MessageProperties()
        {
            const string singlespace = " ";
            ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
            Role = Request.QueryString["rol"];
            CustomerID = Convert.ToInt32(Request.QueryString["cid"]);
            if (Role != null)
            {
                ProjectNotification.Text = Resources.Resource.Project + singlespace + ProjectID + singlespace +
                                           Resources.Resource.Notification;
            }
            if (CustomerID != 0)
            {
                UsernameLabel.Text = GetCustomer(CustomerID).UserName;
            }
            if (ProjectID == 0) return;
            var context = new SidejobEntities();
            GetNextBidder(1, context, ProjectID);
        }

        public ProfessionalGeneral GetProfessional(int professionalid)
        {
            using (var context = new SidejobEntities())
            {
                return (from c in context.ProfessionalGenerals
                        where c.ProID == professionalid
                        select c).FirstOrDefault();
            }
        }

        public CustomerGeneral GetCustomer(int customerid)
        {
            using (var context = new SidejobEntities())
            {
                return (from c in context.CustomerGenerals
                        where c.CustomerID == customerid
                        select c).FirstOrDefault();
            }
        }

        protected void GetNextBidderLCID(string bidderrole)
        {
            using (var context = new SidejobEntities())
            {

                if (bidderrole == "CUS")
                {
                    var c0 = (from c in context.Customers
                              where c.CustomerID == BidderID
                              select c).FirstOrDefault();
                    if (c0 != null)
                    {
                        BidderLCID = c0.LCID;
                    }
                }


                if (bidderrole != "PRO") return;
                var c1 = (from c in context.Professionals
                          where c.ProID == BidderID
                          select c).FirstOrDefault();
                if (c1 != null)
                {
                    BidderLCID = c1.LCID;
                }
            }
        }

        protected void GetNextBidder(int action, SidejobEntities context, int projectid)
        {
            var t = (from c in context.ResponseDelays
                     where c.ProjectID == projectid
                     select c).FirstOrDefault();
            if (t == null) return;
            if (t.BidderID != null) PreviousBidderID = (int)t.BidderID;

            //NextBidder
            var nextbidder = (from c in context.Bids
                              where c.ProjectID == ProjectID
                                    && c.BidderID != PreviousBidderID
                              orderby c.AmountOffered descending
                              select c).FirstOrDefault();

            if (nextbidder == null) return;
            BidderRole = nextbidder.BidderRole;
            BidderID = nextbidder.BidderID;
            BidderEmailAddress = GetProfessional(nextbidder.BidderID).EmailAddress;
            BidderUsername = nextbidder.BidderUserName;
            GetNextBidderLCID(BidderRole);
            switch (action)
            {
                case 1:
                    //Message Property
                    Bidder.Text = nextbidder.BidderUserName;
                    NewBidAmount.Text = nextbidder.AmountOffered.ToString(CultureInfo.InvariantCulture);
                    Currency.Text = GetCurrency(nextbidder.CurrencyID);
                    break;
                default:
                    Bidder.Text = nextbidder.BidderUserName;
                    NewBidAmount.Text = nextbidder.AmountOffered.ToString(CultureInfo.InvariantCulture);
                    Currency.Text = GetCurrency(nextbidder.CurrencyID);
                    break;
            }
        }

        protected string GetCurrency(int currencyid)
        {
            using (var context = new SidejobEntities())
            {
                return (from c in context.Currencies
                        where c.CurrencyID == currencyid
                        select c.CurrencyValue).FirstOrDefault();
            }
        }

        protected void OkButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("http://my-side-job.com");
        }

        protected void CustomLink()
        {
            //var link = Request.Url.AbsoluteUri;
            //ConfirmYourChoiceButton.NavigateUrl = link.Replace("EmailTemplates/Customer/", "EmailTemplates/Customer/InsideWebsite/");

            ConfirmYourChoiceButton.NavigateUrl = "http://www.my-side-job.com/EmailTemplates/Customer/CustomerNewOption.aspx";
        }
    }
}
