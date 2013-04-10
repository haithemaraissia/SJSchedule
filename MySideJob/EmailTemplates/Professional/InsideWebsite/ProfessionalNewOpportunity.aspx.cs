using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SidejobModel;

namespace EmailTemplates.Professional.InsideWebsite
{
    public partial class ProfessionalNewOpportunity : System.Web.UI.Page
    {
        public int ProfessionalID { get; set; }
        public int BidderLCID { get; set; }
        public string BidderUsername { get; set; }
        public string BidderRole { get; set; }
        public int BidderID { get; set; }
        public string BidderEmailAddress { get; set; }
        public int PosterLCID { get; set; }
        public string PosterUsername { get; set; }
        public string PosterRole { get; set; }
        public int PosterID { get; set; }
        public string PosterEmailAddress { get; set; }
        public int ProjectID { get; set; }
        public int ProjectLCID { get; set; }
        public int PDTID { get; set; }
        public enum Message
        {
            Reject
        };

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
            if (SelectionRadioButtonList.Items[1].Selected)
            {
                RejectOffer();
            }
            ThankYouPanelModalPopupExtender.Show();
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
                var project = (from c in context.ResponseDelays
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
            Response.Redirect("http://my-side-job.com");
        }

        protected void RejectOffer()
        { 
            using (var context = new SidejobEntities())
            {
                //Refund Poster
                var rd = (from c in context.ResponseDelays
                          where c.ProjectID == ProjectID
                              select c).FirstOrDefault();
                if (rd == null) return;
                GetBidderPosterProjectProperties(rd);
                RefundPoster();

                //Email The Poster about the rejection.
                EmailPoster(Message.Reject);

                //Delete all References to this project
                CleanUpProjectReferences(context);
            }
        }

        /// <summary>
        /// Refund Poster Process
        /// </summary>
        /// <param name="rd"></param>

        public void GetBidderPosterProjectProperties(ResponseDelay rd)
        {
            if (rd.BidderID != null) BidderID = (int)rd.BidderID;
            PosterID = rd.PosterID;
            ProjectID = rd.ProjectID;
            ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////
            using (var context = new SidejobEntities())
            {
                if (rd.BidderRole == "CUS")
                {
                    BidderRole = "CUS";
                    var c1 = (from c in context.Customers
                              where c.CustomerID == rd.BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                        BidderUsername = c1.UserName;
                        var cg = (from c in context.CustomerGenerals
                                  where c.CustomerID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (cg != null)
                        {
                            BidderEmailAddress = cg.EmailAddress;
                        }
                    }
                }

                if (rd.BidderRole == "PRO")
                {
                    BidderRole = "PRO";
                    var c1 = (from c in context.Professionals
                              where c.ProID == rd.BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                        BidderUsername = c1.UserName;
                        var pg = (from c in context.ProfessionalGenerals
                                  where c.ProID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (pg != null)
                        {
                            BidderEmailAddress = pg.EmailAddress;
                        }
                    }
                }

                if (rd.PosterRole == "CUS")
                {
                    PosterRole = "CUS";
                    var c1 = (from c in context.Customers
                              where c.CustomerID == PosterID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        PosterLCID = c1.LCID;
                        PosterUsername = c1.UserName;
                        var cg = (from c in context.CustomerGenerals
                                  where c.CustomerID == rd.PosterID
                                  select c).FirstOrDefault();
                        if (cg != null)
                        {
                            PosterEmailAddress = cg.EmailAddress;
                        }
                    }
                }

                if (rd.PosterRole == "PRO")
                {
                    PosterRole = "PRO";
                    var c1 = (from c in context.Professionals
                              where c.ProID == PosterID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        PosterLCID = c1.LCID;
                        PosterUsername = c1.UserName;
                        var pg = (from c in context.ProfessionalGenerals
                                  where c.ProID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (pg != null)
                        {
                            BidderEmailAddress = pg.EmailAddress;
                        }
                    }
                }

                var p1 = (from c in context.ProjectRequirements
                          where c.ProjectID == ProjectID
                          select c).FirstOrDefault();
                if (p1 != null) ProjectLCID = p1.LCID;
            }

        }

        public void RefundPoster()
        {
            if (PosterRole == "CUS")
            {
                RefundCustomer(PosterID);
            }
            if (PosterRole == "PRO")
            {
                RefundProfessional(PosterID);
            }

        }

        public void RefundCustomer(int customerId)
        {
            //Check Successful PDT and Check ArchivedSuccessful PDT
            using (var context = new SidejobEntities())
            {
                //Check Successful PDT
                var e = (from c in context.CustomerSuccessfulPDTs
                         where c.CustomerID == customerId && c.ProjectID == ProjectID
                         select c).FirstOrDefault();
                if (e == null)
                {
                    CheckCustomerArchivedPDT(customerId, context);
                }
                else
                {
                    CheckRefundedCustomerPDT(customerId, e, context);
                    context.DeleteObject(e);
                    context.SaveChanges();
                }
            }
        }

        public void CheckRefundedCustomerPDT(int customerId, CustomerSuccessfulPDT e, SidejobEntities context)
        {
            //Check to see if exist
            var r = (from c in context.RefundCustomerSuccessfulPDTs
                     where c.ProjectID == ProjectID && c.CustomerID == customerId
                     select c).FirstOrDefault();
            if (r == null)
            {
                //Check if Record exist
                if ((from c in context.RefundCustomerSuccessfulPDTs
                     where PDTID == e.PDTID
                     select c).FirstOrDefault() == null)
                {
                    //Insert the new refund from the successful table
                    var refund = new RefundCustomerSuccessfulPDT
                    {
                        PDTID = e.PDTID,
                        GrossTotal = e.GrossTotal,
                        Invoice = e.Invoice,
                        PaymentStatus = e.PaymentStatus,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PaymentFee = e.PaymentFee,
                        BusinessEmail = e.BusinessEmail,
                        TxToken = e.TxToken,
                        ReceiverEmail = e.ReceiverEmail,
                        ItemName = e.ItemName,
                        CurrencyCode = e.CurrencyCode,
                        TransactionId = e.TransactionId,
                        Custom = e.Custom,
                        subscriberId = e.subscriberId,
                        CustomerID = e.CustomerID,
                        ProjectID = e.ProjectID
                    };
                    context.AddToRefundCustomerSuccessfulPDTs(refund);
                    context.SaveChanges();
                    PDTID = e.PDTID;
                }
            }
        }

        public void CheckCustomerArchivedPDT(int customerId, SidejobEntities context)
        {
            //Check ArchivedSuccessful PDT
            var e1 = (from c in context.ArchivedCustomerSuccessfulPDTs
                      where c.CustomerID == customerId && c.ProjectID == ProjectID
                      select c).FirstOrDefault();
            if (e1 != null)
            {
                var transaction = e1.TransactionId;
                //Insert the new refund from archieved table

                //Check if Transaction exist in Refund
                var exist = (from c in context.RefundCustomerSuccessfulPDTs
                             where c.TransactionId == transaction
                             select c).FirstOrDefault();
                if (exist == null)
                {
                    //Check if Transaction exist in ArchivedRefund
                    var existarchived = (from c in context.ArchivedRefundCustomerSuccessfulPDTs
                                         where c.TransactionId == transaction
                                         select c).FirstOrDefault();

                    if (existarchived == null)
                    {
                        //Check if Record exist
                        if ((from c in context.RefundCustomerSuccessfulPDTs
                             where PDTID == e1.PDTID
                             select c).FirstOrDefault() == null)
                        {
                            //Insert the new record
                            var archivedrefund = new RefundCustomerSuccessfulPDT
                            {
                                PDTID = e1.PDTID,
                                GrossTotal = e1.GrossTotal,
                                Invoice = e1.Invoice,
                                PaymentStatus = e1.PaymentStatus,
                                FirstName = e1.FirstName,
                                LastName = e1.LastName,
                                PaymentFee = e1.PaymentFee,
                                BusinessEmail = e1.BusinessEmail,
                                TxToken = e1.TxToken,
                                ReceiverEmail = e1.ReceiverEmail,
                                ItemName = e1.ItemName,
                                CurrencyCode = e1.CurrencyCode,
                                TransactionId = e1.TransactionId,
                                Custom = e1.Custom,
                                subscriberId = e1.subscriberId,
                                CustomerID = e1.CustomerID,
                                ProjectID = e1.ProjectID
                            };
                            context.AddToRefundCustomerSuccessfulPDTs(archivedrefund);
                            context.SaveChanges();
                            PDTID = e1.PDTID;
                        }
                    }
                }
            }
        }

        public void RefundProfessional(int professionalId)
        {
            //Check Successful PDT and Check ArchivedSuccessful PDT
            using (var context = new SidejobEntities())
            {
                //Check Successful PDT
                var e = (from c in context.ProfessionalSuccessfulPDTs
                         where c.ProID == professionalId && c.ProjectID == ProjectID
                         select c).FirstOrDefault();
                if (e == null)
                {
                    CheckProfessionalArchivedPDT(professionalId, context);
                }
                else
                {
                    CheckRefundedProfessionalPDT(context, e);
                    context.DeleteObject(e);
                    context.SaveChanges();
                }
            }
        }

        public void CheckRefundedProfessionalPDT(SidejobEntities context, ProfessionalSuccessfulPDT e)
        {
            //Check if Record exist
            if ((from c in context.RefundProfessionalSuccessfulPDTs
                 where PDTID == e.PDTID
                 select c).FirstOrDefault() == null)
            {
                //Insert the new refund from the successful table
                var refund = new RefundProfessionalSuccessfulPDT
                {
                    PDTID = e.PDTID,
                    GrossTotal = e.GrossTotal,
                    Invoice = e.Invoice,
                    PaymentStatus = e.PaymentStatus,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PaymentFee = e.PaymentFee,
                    BusinessEmail = e.BusinessEmail,
                    TxToken = e.TxToken,
                    ReceiverEmail = e.ReceiverEmail,
                    ItemName = e.ItemName,
                    CurrencyCode = e.CurrencyCode,
                    TransactionId = e.TransactionId,
                    Custom = e.Custom,
                    subscriberId = e.subscriberId,
                    ProID = e.ProID,
                    ProjectID = e.ProjectID
                };
                context.AddToRefundProfessionalSuccessfulPDTs(refund);
                context.SaveChanges();
                PDTID = e.PDTID;
            }
        }

        public void CheckProfessionalArchivedPDT(int professionalId, SidejobEntities context)
        {
            //Check ArchivedSuccessful PDT
            var e1 = (from c in context.ArchivedProfessionalSuccessfulPDTs
                      where c.ProID == professionalId && c.ProjectID == ProjectID
                      select c).FirstOrDefault();
            if (e1 != null)
            {
                var transaction = e1.TransactionId;
                //Insert the new refund from archieved table

                //Check if Transaction exist in Refund
                var exist = (from c in context.RefundProfessionalSuccessfulPDTs
                             where c.TransactionId == transaction
                             select c).FirstOrDefault();
                if (exist == null)
                {
                    //Check if Transaction exist in ArchivedRefund
                    var existarchived = (from c in context.ArchivedRefundProfessionalSuccessfulPDTs
                                         where c.TransactionId == transaction
                                         select c).FirstOrDefault();

                    if (existarchived == null)
                    {
                        //Check if Record exist
                        if ((from c in context.RefundProfessionalSuccessfulPDTs
                             where PDTID == e1.PDTID
                             select c).FirstOrDefault() == null)
                        {
                            //Insert the new record
                            var archivedrefund = new RefundProfessionalSuccessfulPDT
                            {
                                PDTID = e1.PDTID,
                                GrossTotal = e1.GrossTotal,
                                Invoice = e1.Invoice,
                                PaymentStatus = e1.PaymentStatus,
                                FirstName = e1.FirstName,
                                LastName = e1.LastName,
                                PaymentFee = e1.PaymentFee,
                                BusinessEmail = e1.BusinessEmail,
                                TxToken = e1.TxToken,
                                ReceiverEmail = e1.ReceiverEmail,
                                ItemName = e1.ItemName,
                                CurrencyCode = e1.CurrencyCode,
                                TransactionId = e1.TransactionId,
                                Custom = e1.Custom,
                                subscriberId = e1.subscriberId,
                                ProID = e1.ProID,
                                ProjectID = e1.ProjectID
                            };
                            context.AddToRefundProfessionalSuccessfulPDTs(archivedrefund);
                            context.SaveChanges();
                            PDTID = e1.PDTID;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Email The Poster about the rejection
        /// </summary>
        /// <param name="messageType"> </param>
        public void EmailPoster(Message messageType)
        {

            switch (messageType)
            {
                //This will be under Manage Site
                case Message.Reject:
                    {
                        if (PosterRole == "CUS")
                        {
                            SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, 
                                "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerReject.aspx", Message.Reject, "CUS");
                        }
                        if (PosterRole == "PRO")
                        {
                            SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, 
                                   "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalReject.aspx", Message.Reject, "PRO");
                        }
                    }
                    break;
            }
        }

        protected void SendEmail(string userEmail, string userName, int lcid, string templateUrl, Message messageType, string role = "", int id = 0)
        {
            string url = "";
            switch (messageType)
            {
                case Message.Reject:
                    if (role == "CUS")
                    {
                        url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&cid=" + PosterID +"&bid=" + BidderID;
                    }
                    if (role == "PRO")
                    {
                        url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&pid=" + PosterID + "&bid=" + BidderID;
                    }
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

        /// <summary>
        /// Delete All References to this project
        /// </summary>
        public void CleanUpProjectReferences(SidejobEntities context)
        {
            //ProjectSecondChance
            //ClosedProject
            //ResponseDelay   
            //Bids

            //// Archives ////
            /////Cancelled////
            //////Project/////
            //// Archives ////
            
            //ProfessionalWinBid

            CleanUpTables(context, (from c in context.ProjectSecondChances where c.ProjectID == ProjectID select c).ToList());
            CleanUpTables(context, (from c in context.ClosedProjects where c.ProjectID == ProjectID select c).ToList());
            CleanUpTables(context, (from c in context.ResponseDelays where c.ProjectID == ProjectID select c).ToList());
            CleanUpTables(context, (from c in context.Bids where c.ProjectID == ProjectID &&  c.BidderID == BidderID select c).ToList());
            ArchiveCancelledProject(context);
            var pwb = (from c in context.ProfessionalWinBids
                           where c.ProID == BidderID
                           select c).FirstOrDefault();
            if (pwb == null) return;
            context.DeleteObject(pwb);
            context.SaveChanges();
        }

        private static void CleanUpTables(SidejobEntities context, ICollection entityList)
        {
            if (entityList.Count <= 0) return;
            foreach (var item in entityList)
            {
                context.DeleteObject(item);
                context.SaveChanges();
            }
        }

        public void ArchiveCancelledProject(SidejobEntities context)
        {
            //Insert into archived
            var currentproject = (from c in context.Projects
                                  where c.ProjectID == ProjectID
                                  select c).FirstOrDefault();
            var currentprojectphoto = (from c in context.ProjectPhotoes
                                       where c.ProjectID == ProjectID
                                       select c).ToList();
            var currentprojectpicture = (from c in context.ProjectPictures
                                         where c.ProjectID == ProjectID
                                         select c).FirstOrDefault();
            var currentprojectrequirement = (from c in context.ProjectRequirements
                                             where c.ProjectID == ProjectID
                                             select c).FirstOrDefault();
            if (currentproject != null)
            {
                if (currentprojectrequirement != null)
                {
                    if ((from c in context.ArchievedCancelledProjects
                         where c.ProjectID == ProjectID
                         select c).FirstOrDefault() == null)
                    {
                        var acp = new ArchievedCancelledProject
                        {
                            ProjectID = (int)currentproject.ProjectID,
                            DateFinished = DateTime.Now.Date,
                            PosterID = PosterID,
                            PosterRole = PosterRole,
                            BidderID = BidderID,
                            BidderRole = BidderRole,
                            HighestBid = currentproject.HighestBid,
                            Currency = currentprojectrequirement.CurrencyID,
                            Status = currentproject.StatusInt
                        };
                        context.AddToArchievedCancelledProjects(acp);
                    }
                }
            }

            //for each record
            foreach (var cp in currentprojectphoto)
            {
                if ((from c in context.ArchievedCancelledProjectPhotoes
                     where c.PhotoID == cp.PhotoID
                     select c).FirstOrDefault() == null)
                {
                    var ac = new ArchievedCancelledProjectPhoto
                    {
                        PhotoID = cp.PhotoID,
                        ProjectID = cp.ProjectID,
                        PhotoPath = cp.PhotoPath,
                        Caption = cp.Caption,
                        AlbumID = cp.AlbumID,
                        PhotoRank = cp.PhotoRank
                    };
                    context.AddToArchievedCancelledProjectPhotoes(ac);
                }
            }

            if (currentprojectpicture != null)
            {
                if ((from c in context.ArchievedCancelledProjectPictures
                     where c.AlbumID == currentprojectpicture.AlbumID
                     select c).FirstOrDefault() == null)
                {
                    var acpi = new ArchievedCancelledProjectPicture
                    {
                        AlbumID = currentprojectpicture.AlbumID,
                        AlbumCaption = currentprojectpicture.AlbumCaption,
                        Numberofimages = currentprojectpicture.Numberofimages,
                        ProjectID = currentprojectpicture.ProjectID
                    };
                    context.AddToArchievedCancelledProjectPictures(acpi);
                }
            }

            if (currentprojectrequirement != null)
            {
                if ((from c in context.ArchievedCancelledProjectRequirements
                     where c.ProjectID == currentprojectpicture.AlbumID
                     select c).FirstOrDefault() == null)
                {
                    var acpr = new ArchievedCancelledProjectRequirement
                    {
                        ProjectID = currentprojectrequirement.ProjectID,
                        LCID = currentprojectrequirement.LCID,
                        CategoryID = currentprojectrequirement.CategoryID,
                        CategoryName = currentprojectrequirement.CategoryName,
                        JobID = currentprojectrequirement.JobID,
                        JobTitle = currentprojectrequirement.JobTitle,
                        ExperienceID = currentprojectrequirement.ExperienceID,
                        CrewNumberID = currentprojectrequirement.CrewNumberID,
                        LicensedID = currentprojectrequirement.LicensedID,
                        InsuredID = currentprojectrequirement.InsuredID,
                        RelocationID = currentprojectrequirement.RelocationID,
                        ProjectTitle = currentprojectrequirement.ProjectTitle,
                        StartDate = currentprojectrequirement.StartDate,
                        EndDate = currentprojectrequirement.EndDate,
                        AmountOffered = currentprojectrequirement.AmountOffered,
                        CurrencyID = currentprojectrequirement.CurrencyID,
                        Description = currentprojectrequirement.Description,
                        SpecialNotes = currentprojectrequirement.SpecialNotes,
                        Address = currentprojectrequirement.Address,
                        CountryID = currentprojectrequirement.CountryID,
                        CountryName = currentprojectrequirement.CountryName,
                        RegionID = currentprojectrequirement.RegionID,
                        RegionName = currentprojectrequirement.RegionName,
                        CityID = currentprojectrequirement.CityID,
                        CityName = currentprojectrequirement.CityName,
                        Zipcode = currentprojectrequirement.Zipcode,
                        DatePosted = currentprojectrequirement.DatePosted,
                        TimeLeft = currentprojectrequirement.TimeLeft
                    };

                    context.AddToArchievedCancelledProjectRequirements(acpr);
                }
            }
            context.SaveChanges();


            //delete from current project 
            if (currentprojectrequirement != null) context.DeleteObject(currentproject);
            if (currentprojectphoto.Count != 0)
            {
                foreach (var cp in currentprojectphoto)
                {
                    context.DeleteObject(cp);
                }

            }
            if (currentprojectpicture != null) context.DeleteObject(currentprojectpicture);
            if (currentprojectrequirement != null) context.DeleteObject(currentprojectrequirement);
            context.SaveChanges();
        }
    }
}