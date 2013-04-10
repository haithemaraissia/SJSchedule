using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SidejobModel;

namespace EmailTemplates.Customer.InsideWebsite
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

        protected void ConfirmYourChoiceButtonClick(object sender, EventArgs e)
        {
            if (SelectionRadioButtonList.SelectedIndex == -1)return;
            MessageProperties();
            if (SelectionRadioButtonList.Items[0].Selected)
            {
                NewAggrement(ProjectID, CustomerID);
            }
            else
            {
                ExtendProject(ProjectID);
            }
            ThankYouPanelModalPopupExtender.Show();
        }

        private static void ExtendProject(int projectid)
        {
            using (var context = new SidejobEntities())
            {

                //Extend Project ( Make statusInt = 0 and ouput message)

                // Delete the previous bidder in Project
                var newBidProcess = new NewProjectBidProcess(PreviousBidderID, ProjectID);
                newBidProcess.DeleteAllBids(context, ProjectID, PreviousBidderID);

                //Delete From ClosedProject
                var closedproject = (from c in context.ClosedProjects
                                     where c.ProjectID == projectid
                                     select c).FirstOrDefault();
                if (closedproject != null)
                {
                    context.DeleteObject(closedproject);
                }

                //Delete From ResponseDelay
                var rd = (from c in context.ResponseDelays
                          where c.ProjectID == projectid
                          select c).FirstOrDefault();
                if (rd != null)
                {
                    context.DeleteObject(rd);
                    context.SaveChanges();
                }

                //Update Project Requirement End Date to Today + 7 days
                var projectrequirement = (from c in context.ProjectRequirements
                                          where c.ProjectID == projectid
                                          select c).FirstOrDefault();
                if (projectrequirement != null)
                {
                    projectrequirement.DatePosted = DateTime.UtcNow.Date.AddDays(7);
                    context.SaveChanges();
                }

                //Clear Project Winning Bid
                var project = (from c in context.Projects
                               where c.ProjectID == projectid
                               select c).FirstOrDefault();
                if (project == null) return;
                project.StatusInt = 0;
                project.Status = Resources.Resource.OPEN;
                project.HighestBid = null;
                project.HighestBidderID = null;
                project.HighestBidUsername = null;
                project.NumberofBids = 0;
                context.SaveChanges();
            }
        }

        protected void NewAggrement(int projectid, int customerid)
        {
            using (var context = new SidejobEntities())
            {
                //The other party aggree can only be Bidder
                //add the new record to ProjectSecondChance
                var posterparty = (from c in context.ProjectSecondChances
                                   where c.ProjectID == projectid && c.AgreerID == customerid
                                   select c).FirstOrDefault();

                if (posterparty != null) return;
                var newparty = new ProjectSecondChance
                                   {
                                       ProjectID = projectid,
                                       AgreerID = customerid,
                                       AgreerRole = "CUS",
                                       DateTime = DateTime.Now.Date,
                                       AgreerProjectRole = GetProjectRole(projectid, customerid)
                                   };
                context.AddToProjectSecondChances(newparty);
                context.SaveChanges();
                GetNextBidder(2, context, projectid);
            }

        }

        public void EmailBidder(Message messageType)
        {
            switch (messageType)
            {

                case Message.NewOpportunity:
                    {
                        if (BidderRole == "CUS")
                        {
                            SendEmail(BidderEmailAddress, BidderUsername, BidderLCID,
                                     "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNewOpportunity.aspx",
                                      Message.NewOpportunity);
                        }
                        if (BidderRole == "PRO")
                        {
                            SendEmail(BidderEmailAddress, BidderUsername, BidderLCID,
                                     "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNewOpportunity.aspx",
                                      Message.NewOpportunity);
                        }
                    }
                    break;

            }
        }

        protected void SendEmail(string userEmail, string userName, int lcid, string templateUrl,
                                 Message messageType)
        {
            var url = "";
            switch (messageType)
            {
                case Message.NewOption:
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&pid=" + BidderID;
                    break;

                case Message.NewOpportunity:
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&pid=" + BidderID;
                    break;
            }

            const string strFrom = "postmaster@my-side-job.com";
            var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
            {
                BodyEncoding = Encoding.Default,
                Subject = Resources.Resource.Notification,
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

        public string GetProjectRole(int projectid, int customerid)
        {
            using (var context = new SidejobEntities())
            {
                var project = (from c in context.ResponseDelays
                               where c.ProjectID == projectid
                               select c).FirstOrDefault();

                if (project == null)
                {
                    return "";
                }
                if (project.PosterID == customerid)
                    return "Poster";

                if (project.BidderID == customerid)
                    return "Bidder";
            }
            return "";
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
                    var c1 = (from c in context.Customers
                              where c.CustomerID == BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                    }
                }


                if (bidderrole == "PRO")
                {
                    var c1 = (from c in context.Professionals
                              where c.ProID == BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                    }
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
                case 2:
                    // Email Next Bidder
                    EmailBidder(Message.NewOpportunity);
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
    }
}
