using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using SidejobModel;

public partial class PartOfManageEmailTemplatesProfessionalProfessionalReject : System.Web.UI.Page
{
    public int ProjectID { get; set; }
    public int ProfessionalID { get; set; }
    public int BidderID { get; set; }

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
        const string startofBoldRed = "<span class='DarkRed'><strong>";
        const string lastofBoldRed = "</strong></span>";
        ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
        ProfessionalID = Convert.ToInt32(Request.QueryString["pid"]);
        BidderID = Convert.ToInt32(Request.QueryString["bid"]);
        if (ProjectID == 0) return;
        ProjectNotification.Text = Resources.Resource.Project + singlespace + ProjectID + singlespace + Resources.Resource.Notification;
        HadRejectedYourOfferLabel.Text = singlespace + startofBoldRed + GetProfessional(BidderID).UserName + lastofBoldRed + singlespace + Resources.Resource.RejectedYourOffer;
        NameLabel.Text = GetProfessional(ProfessionalID).FirstName + singlespace + GetProfessional(ProfessionalID).LastName;
    }

    public ProfessionalGeneral GetProfessional(int proID)
    {
        using (var context = new SidejobEntities())
        {
            return (from c in context.ProfessionalGenerals
                    where c.ProID == proID
                    select c).FirstOrDefault();
        }
    }

}