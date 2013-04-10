using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using SidejobModel;

namespace EmailTemplates.Professional
{
    public partial class ProfessionalNewOpportunity : System.Web.UI.Page
    {
        public int ProjectID { get; set; }
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
                CustomLink();
            }
        }

        protected void MessageProperties()
        {
            const string singlespace = " ";
            const string startofBoldRed = "<span class='DarkRed'><strong>";
            const string lastofBoldRed = "</strong></span>";
            ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
            ProfessionalID = Convert.ToInt32(Request.QueryString["pid"]);
            ProjectNotification.Text = Resources.Resource.Project + singlespace + ProjectID + singlespace +
                                           Resources.Resource.Notification;
            if (ProfessionalID != 0)
            {
                UsernameLabel.Text = GetProfessional().UserName;
            }
            YouaretheSecondBidder.Text = Resources.Resource.SecondHighestBidder + singlespace + startofBoldRed + ProjectID + lastofBoldRed + Resources.Resource.Period;
            Posterwouldlikeyoutoaccept.Text = Resources.Resource.Poster + singlespace +
               startofBoldRed + GetPoster() + lastofBoldRed + singlespace + Resources.Resource.WouldLikeToAcceptYourBids;
        }

        protected void ConfirmYourChoiceButtonClick(object sender, EventArgs e)
        {
            if (SelectionRadioButtonList.SelectedIndex == -1) return;
            MessageProperties();
            if (SelectionRadioButtonList.Items[0].Selected)
            {
                NewAggrement(ProjectID, ProfessionalID);
                var start = new NewProjectBidProcess(ProfessionalID, ProjectID);
                
                
                start.StartProcess();
            }
        }

        protected void NewAggrement(int projectid, int customerid)
        {
            using (var context = new SidejobEntities())
            {

                //the other party aggree had already Aggreed
                //add the new record to ProjectSecondChance
                var bidderparty = (from c in context.ProjectSecondChances
                                   where c.ProjectID == projectid && c.AgreerID == ProfessionalID
                                   select c).FirstOrDefault();

                if (bidderparty != null) return;
                var newparty = new ProjectSecondChance
                                   {
                                       ProjectID = projectid,
                                       AgreerID = ProfessionalID,
                                       AgreerRole = "PRO",
                                       DateTime = DateTime.Now.Date,
                                       AgreerProjectRole = GetProjectRole(projectid)
                                   };
                context.AddToProjectSecondChances(newparty);
                context.SaveChanges();
            }

        }

        public string GetProjectRole(int projectid)
        {
            using (var context = new SidejobEntities())
            {
                var project = (from c in context.ClosedProjects
                               where c.ProjectID == projectid
                               select c).FirstOrDefault();

                if (project == null)
                {
                    return "";
                }
                if (project.PosterID == ProfessionalID)
                    return "Poster";

                if (project.BidderID == ProfessionalID)
                    return "Bidder";
            }
            return "New Bidder";
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

        public ProfessionalGeneral GetProfessional()
        {
            using (var context = new SidejobEntities())
            {
                return (from c in context.ProfessionalGenerals
                        where c.ProID == ProfessionalID
                        select c).FirstOrDefault();
            }
        }

        protected string GetPoster()
        {
              using (var context = new SidejobEntities())
              {
                  var t = (from c in context.ResponseDelays
                           where c.ProjectID == ProjectID
                           select c).FirstOrDefault();
                  if (t == null) return "";
                  if (t.PosterID != 0)
                  {
                      if (t.PosterRole == "CUS")
                      {
                          return (GetCustomer(t.PosterID).UserName);
                      }
                      if (t.PosterRole == "PRO")
                      {
                          return (GetProfessional().UserName);
                      }
                  }
              }
            return "";
        }

        protected void OkButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("http://my-side-job.com/Authenticated/Professional/ProfessionalProfile.aspx");
        }

        protected void CustomLink()
        {
            //var link = Request.Url.AbsoluteUri;
            //ConfirmYourChoiceButton.NavigateUrl = link.Replace("EmailTemplates/Professional/", "EmailTemplates/Professional/InsideWebsite/");
            ConfirmYourChoiceButton.NavigateUrl = "http://www.my-side-job.com/EmailTemplates/Professional/ProfessionalNewOpportunity.aspx";
        }

    }
}