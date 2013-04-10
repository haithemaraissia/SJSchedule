using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using SidejobModel;

public partial class ResponseDelay : Page
{
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
        Notification,
        Warning,
        NewOption,
        NewOpportunity,
        Refund,
        Blocked
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        ChecKTime();
    }

    protected void ChecKTime()
    {

        using (var context = new SidejobEntities())
        {
            //var responseDelay = DateTime.UtcNow.Date.AddHours(72);
            var t = (from c in context.ResponseDelays
                     select c).ToList();
            foreach (SidejobModel.ResponseDelay t1 in t)
            {
                if (t1.DateFinished.Date.AddHours(72) <= DateTime.UtcNow.Date)
                {
                    GetBidderPosterProjectProperties(t1);
                    var status = t1.Status;
                    //Case 0 waiting for Case 2
                    /////////////Project closed and no action from the bidder//////////////
                    BidderDelay(status, context, t1);
                    /////////////Project closed and no action from the bidder//////////////


                    //Case 2 waiting for Case 4
                    ////////////////////////////////Poster no payment////////////////////////////
                    PosterDelay(context, status, t1);
                    ////////////////////////////////Poster no payment////////////////////////////


                    ///////////////////////////////////FOR PDT/IPN  MORE CHANGE SEE THE DOCUMENT 72HOURS///////////////////////////////////
                    context.SaveChanges();
                    ///////////////////////////////////FOR PDT/IPN  MORE CHANGE SEE THE DOCUMENT 72HOURS///////////////////////////////////
                }
            }

        }
    }

    public void PosterDelay(SidejobEntities context, int status, SidejobModel.ResponseDelay t1)
    {
        if (status == 4)
        {
            var reminderLevel = t1.ReminderLevel;
            if (reminderLevel == 0)
            {
                // Email Poster. Email Bidder
                // Warn Poster with reminder  1
                //Update Time Delay to Reminder level  1

                EmailPoster(t1, Message.Warning);
                EmailBidder(t1, Message.Notification);
                t1.ReminderLevel = 1;

            }
            if (reminderLevel == 1)
            {
                // Email Poster. Email Bidder
                // Warn Poster with reminder 2
                //Update Time Delay to Reminder level 2

                EmailPoster(t1, Message.Warning);
                EmailBidder(t1, Message.Notification);
                t1.ReminderLevel = 2;
            }

            if (reminderLevel == 2)
            {
                // Email Poster. Email Bidder
                // Warn Poster with reminder 3
                //Update Time Delay to Reminder level 3

                EmailPoster(t1, Message.Warning);
                EmailBidder(t1, Message.Notification);
                t1.ReminderLevel = 3;
            }
            if (reminderLevel == 3)
            {
                ////////////////////////////////Poster no payment////////////////////////////

                //  Add The Poster to blocked poster
                BlockPoster(context);

                // Email Bidder & Email Poster
                //Throught Button in CustomerRefund or ProfessionalRefund
                //   EmailBidder(t1, Message.Refund);

                EmailPoster(t1, Message.Blocked);

                //Refund Bidder
                RefundBidder();
                //Through PayPalAPI inthe future
                ////////////////////////////////////////
                //Delete from ClosedProject
                var closedproject = (from c in context.ClosedProjects
                                     where c.ProjectID == ProjectID
                                     select c).FirstOrDefault();
                if (closedproject != null)
                {
                    context.DeleteObject(closedproject);
                }
                context.SaveChanges();

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
                t1.ReminderLevel = 4;
                context.SaveChanges();
                ///////////////////////////////Poster no payment////////////////////////////
            }
        }
    }

    public void BidderDelay(int status, SidejobEntities context, SidejobModel.ResponseDelay t1)
    {
        if (status == 3)
        {
            var reminderLevel = t1.ReminderLevel;
            if (reminderLevel == 0)
            {
                // Email Poster. Email Bidder
                // Warn Bidder with reminder  1
                // Update Time Delay to Reminder level  1
                EmailPoster(t1, Message.Notification);
                EmailBidder(t1, Message.Warning);
                t1.ReminderLevel = 1;
            }
            if (reminderLevel == 1)
            {
                // Email Poster. Email Bidder
                // Warn Bidder with reminder  2
                // Update Time Delay to Reminder level  2
                EmailPoster(t1, Message.Notification);
                EmailBidder(t1, Message.Warning);
                t1.ReminderLevel = 2;
            }
            if (reminderLevel == 2)
            {
                // Email Poster. Email Bidder
                // Warn Bidder with reminder  3
                //Update Time Delay to Reminder level  3
                EmailPoster(t1, Message.Notification);
                EmailBidder(t1, Message.Warning);
                t1.ReminderLevel = 3;
            }
            if (reminderLevel == 3)
            {
                // Add The Bidder to blocked poster
                BlockBidder(context);

                // Email Bidder & Email Poster
                //Throught Button in CustomerRefund
                //  EmailPoster(t1, Message.Refund);

               EmailBidder(t1, Message.Blocked);

                // Refund Poster
                RefundPoster();


                //If Exist Next Bidder
                var bids = (from c in context.Bids
                            where c.ProjectID == ProjectID
                                  && c.BidderID != BidderID
                            orderby c.AmountOffered
                            select c).FirstOrDefault();

                if (bids != null)
                {
                    //var nextbidder = bids;
                    // Email Poster
                    EmailPoster(t1, Message.NewOption);
                }
                t1.ReminderLevel = 4;
            }
        }
    }

    public void BlockPoster(SidejobEntities context)
    {
        if (PosterRole == "CUS")
        {
            //Add to BlockedCustomer
            var cg = (from c in context.CustomerGenerals
                      where c.CustomerID == PosterID
                      select c).FirstOrDefault();

            if (cg != null)
            {
                if (((from c in context.LockedCustomers
                      where c.CustomerID == cg.CustomerID
                      select c).FirstOrDefault()) == null)
                {
                    var lockedcustomer = new LockedCustomer
                                             {
                                                 FirstName = cg.FirstName,
                                                 LastName = cg.LastName,
                                                 Country = cg.CountryName,
                                                 Region = cg.RegionName,
                                                 Age = cg.Age,
                                                 Gender = cg.Gender,
                                                 EmailAddress = cg.EmailAddress,
                                                 Reason = "NOT Paying Project" + ProjectID,
                                                 Date = DateTime.Now.Date,
                                                 IP = 0,
                                                 CustomerID = cg.CustomerID,
                                                 ProjectID = ProjectID
                                             };
                    context.AddToLockedCustomers(lockedcustomer);
                }
            }
            context.SaveChanges();
        }
        if (PosterRole == "PRO")
        {
            //Add to BlockedProfessional
            var pg = (from c in context.ProfessionalGenerals
                      where c.ProID == PosterID
                      select c).FirstOrDefault();
            if (pg != null)
            {
                if (((from c in context.LockedProfessionals
                      where c.ProID == pg.ProID
                      select c).FirstOrDefault()) == null)
                {
                    var lockedprofessional = new LockedProfessional
                                                 {
                                                     FirstName = pg.FirstName,
                                                     LastName = pg.LastName,
                                                     Country = pg.CountryName,
                                                     Region = pg.RegionName,
                                                     Age = pg.Age,
                                                     Gender = pg.Gender,
                                                     EmailAddress = pg.EmailAddress,
                                                     Reason = "NOT Paying Project" + ProjectID,
                                                     Date = DateTime.Now.Date,
                                                     IP = 0,
                                                     ProID = pg.ProID,
                                                     ProjectID = ProjectID
                                                 };

                    context.AddToLockedProfessionals(lockedprofessional);
                }
            }

            context.SaveChanges();
        }
    }

    public void BlockBidder(SidejobEntities context)
    {
        if (BidderRole == "CUS")
        {
            //Add to BlockedCustomer
            var cg = (from c in context.CustomerGenerals
                      where c.CustomerID == BidderID
                      select c).FirstOrDefault();

            if (cg != null)
            {
                var lockedcustomer = new LockedCustomer
                {
                    FirstName = cg.FirstName,
                    LastName = cg.LastName,
                    Country = cg.CountryName,
                    Region = cg.RegionName,
                    Age = cg.Age,
                    Gender = cg.Gender,
                    EmailAddress = cg.EmailAddress,
                    Reason = "NOT Paying Project" + ProjectID,
                    Date = DateTime.Now.Date,
                    IP = 0,
                    CustomerID = cg.CustomerID,
                    ProjectID = ProjectID
                };
                context.AddToLockedCustomers(lockedcustomer);
            }
            context.SaveChanges();
        }
        if (BidderRole == "PRO")
        {
            //Add to BlockedProfessional
            var pg = (from c in context.ProfessionalGenerals
                      where c.ProID == BidderID
                      select c).FirstOrDefault();
            if (pg != null)
            {
                var lockedprofessional = new LockedProfessional
                {
                    FirstName = pg.FirstName,
                    LastName = pg.LastName,
                    Country = pg.CountryName,
                    Region = pg.RegionName,
                    Age = pg.Age,
                    Gender = pg.Gender,
                    EmailAddress = pg.EmailAddress,
                    Reason = "NOT Paying Project" + ProjectID,
                    Date = DateTime.Now.Date,
                    IP = 0,
                    ProID = pg.ProID,
                    ProjectID = ProjectID
                };
                context.AddToLockedProfessionals(lockedprofessional);
            }
            context.SaveChanges();
        }
    }

    public void EmailPoster(SidejobModel.ResponseDelay t1, Message messageType, string pdtid = "")
    {

        //Notification:
        //?l=en-US&prid=5&usn=jack&bidder=smith&rem=1
        //Reminder:
        //?l=en-US&usn=jack&rem=1
        //NewOption:(Inside and Outside Website)
        //?l=en-US&prid=5&rol=CUS&cid=28
        //New Opportunity:(Inside and Outside Website)
        //l=en-US&prid=5&pid=25
        //Refund:
        //l=en-US&pdtid=1&prid=5&pid=25
        //Blocked
        //l=fr&prid=5&cid=28

        switch (messageType)
        {
            case Message.Notification:
                {
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                                 "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNotification.aspx", Message.Notification, "CUS");
                    }

                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                                  "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNotification.aspx", Message.Notification, "PRO");
                    }
                }
                break;


            case Message.Warning:
                {
                    // Warn Bidder with reminder 1
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerReminder.aspx", Message.Warning);
                    }
                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalReminder.aspx", Message.Warning);
                    }
                }
                break;

            case Message.NewOption:
                {
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                            "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNewOption.aspx", Message.NewOption, "CUS", PosterID);
                    }
                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNewOption.aspx", Message.NewOption, "PRO", PosterID);
                    }
                }
                break;

            case Message.NewOpportunity:
                {
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                            "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNewOpportunity .aspx", Message.NewOpportunity, "CUS", PosterID);
                    }
                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNewOpportunity.aspx", Message.NewOpportunity, "PRO", PosterID);
                    }
                }
                break;
            //This will be under Manage Site
            case Message.Refund:
                {
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Customer/CustomerRefund.aspx", Message.Refund, "CUS", PosterID, PDTID);
                    }
                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Professional/ProfessionalRefund.aspx", Message.Refund, "PRO", PosterID, PDTID);
                    }
                }
                break;

            case Message.Blocked:
                {
                    if (PosterRole == "CUS")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerBlocked.aspx", Message.Blocked, "CUS", PosterID);
                    }
                    if (PosterRole == "PRO")
                    {
                        SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
                              "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalBlocked.aspx", Message.Blocked, "PRO", PosterID);
                    }
                }
                break;
        }
    }

    public void EmailBidder(SidejobModel.ResponseDelay t1, Message messageType)
    {
        //Notification:
        //?l=en-US&prid=5&usn=jack&bidder=smith&rem=1
        //Reminder:
        //?l=en-US&usn=jack&rem=1
        //NewOption:(Inside and Outside Website)
        //?l=en-US&prid=5&rol=CUS&cid=28
        //New Opportunity:(Inside and Outside Website)
        //l=en-US&prid=5&pid=25
        //Refund:
        //l=en-US&pdtid=1&prid=5&pid=25
        //Blocked
        //l=fr&prid=5&cid=28
        switch (messageType)
        {
            case Message.Notification:
                {
                    if (BidderRole == "CUS")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                            "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNotification.aspx", Message.Notification, "CUS");
                    }

                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                                  "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNotification.aspx", Message.Notification, "PRO");
                    }
                }
                break;


            case Message.Warning:
                {
                    // Warn Bidder with reminder 1
                    if (BidderRole == "CUS")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                                  "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerReminder.aspx", Message.Warning);
                    }
                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                                 "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalReminder.aspx", Message.Warning);
                    }
                }
                break;


            case Message.NewOption:
                {
                    if (BidderRole == "CUS")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                                  "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNewOption.aspx", Message.NewOption, "CUS", BidderID);
                    }
                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                                  "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNewOption.aspx", Message.NewOption, "PRO", BidderID);
                    }
                }

                break;

            case Message.NewOpportunity:
                {
                    if (BidderRole == "CUS")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerNewOpportunity .aspx", Message.NewOpportunity, "CUS", BidderID);
                    }
                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalNewOpportunity.aspx", Message.NewOpportunity, "PRO", BidderID);
                    }
                }
                break;
            //This will be under Manage Site
            case Message.Refund:
                {
                    if (BidderRole == "CUS")
                    {
                        //Send Email To admin to Refund.
                        //Later will be done through API
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Customer/CustomerRefund.aspx", Message.Refund, "CUS", BidderID, PDTID);
                    }
                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Professional/ProfessionalRefund.aspx", Message.Refund, "PRO", BidderID, PDTID);
                    }
                }
                break;

            case Message.Blocked:
                {
                    if (BidderRole == "CUS")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Customer/CustomerBlocked.aspx", Message.Blocked, "CUS", BidderID);
                    }
                    if (BidderRole == "PRO")
                    {
                        SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
                               "http://my-side-job.com/Schedule/MySideJob/EmailTemplates/Professional/ProfessionalBlocked.aspx", Message.Blocked, "PRO", BidderID);
                    }
                }
                break;
        }
    }

    protected void SendEmail(string userEmail, string userName, int lcid, int reminderLevel, string templateUrl,
        Message messageType, string role = "", int id = 0, int pdtid = 0)
    {
        //var url = "http://www.my-side-job.com/Schedule/MySideJob/EmailTemplates/PasswordRecovery.aspx?psw=" + psw +
        //          "&usn=" + username + "&l=" + lang;
        // var lang = Request.QueryString["l"] ?? "en-US";

        //Format//
        // Notification:
        //?l=en-US&prid=5&usn=jack&bidder=smith&rem=1.

        //Reminder: Is Warning
        //?l=en-US&usn=jack&rem=1

        //NewOption:(Inside and Outside Website)
        //?l=en-US&prid=5&rol=CUS&cid=28

        //New Opportunity:(Inside and Outside Website)
        //l=en-US&prid=5&pid=25

        //Refund:
        //l=en-US&pdtid=1&prid=5&cid=25

        //Blocked 
        //l=fr&prid=5&pid=28


        string url = "";
        switch (messageType)
        {
            case Message.Notification:

                url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&usn=" + userName + "&bidder=" + BidderUsername + "&poster=" + PosterUsername
                    + "&rem=" + reminderLevel + "&role=" + role;
                Response.Write(url);
                break;

            case Message.Warning:

                url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&usn=" + userName + "&rem=" + reminderLevel;
                break;

            case Message.NewOption:
                if (role == "CUS")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&rol=" + role + "&cid=" + id;
                }
                if (role == "PRO")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&rol=" + role + "&pid=" + id;
                }
                break;

            case Message.NewOpportunity:
                if (role == "CUS")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&cid=" + id;
                    Response.Write(url);
                }
                if (role == "PRO")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&pid=" + id;
                    Response.Write(url);
                }
                break;

            case Message.Refund:
                if (role == "CUS")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&pdtid=" + pdtid + "&prid=" + ProjectID + "&cid=" + id;
                }
                if (role == "PRO")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&pdtid=" + pdtid + "&prid=" + ProjectID + "&pid=" + id;
                }
                break;

            case Message.Blocked:
                if (role == "CUS")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&cid=" + id;
                }
                if (role == "PRO")
                {
                    url = templateUrl + "?&l=" + ScheduleUtility.GetLanguage(lcid) + "&prid=" + ProjectID + "&pid=" + id;
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

    /////////////////////Get Bidder Poster Project Properties/////////////////////////
    public void GetBidderPosterProjectProperties(SidejobModel.ResponseDelay rd)
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
    ////////////////////////Get Bidder Poster Project Properties//////////////////////////

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

    public void RefundBidder()
    {
        if (BidderRole == "CUS")
        {
            RefundCustomer(BidderID);
        }
        if (BidderRole == "PRO")
        {
            RefundProfessional(BidderID);
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
}


















//public void EmailPosterandBidder(SidejobModel.ResponseDelay t1, Message messageType)
//{

//    switch (messageType)
//    {
//        case Message.Notification:
//            {
//                if (PosterRole == "CUS")
//                {
//                    SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
//                              Server.MapPath("~/EmailTemplates/CustomerNotification.aspx"), Message.Notification);
//                }

//                if (PosterRole == "PRO")
//                {
//                    SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
//                              Server.MapPath("~/EmailTemplates/ProfessionalNotification.aspx"), Message.Notification);
//                }
//            }
//            break;


//        case Message.Warning:
//            {
//                // Warn Bidder with reminder 1
//                if (BidderRole == "CUS")
//                {
//                    SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
//                              Server.MapPath("~/EmailTemplates/CustomerReminder.aspx"), Message.Warning);
//                }
//                if (BidderRole == "PRO")
//                {
//                    SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
//                              Server.MapPath("~/EmailTemplates/ProfessionalReminder.aspx"), Message.Warning);
//                }
//            }
//            break;

//        case Message.NewOption:
//            {
//                if (PosterRole == "CUS")
//                {
//                    SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
//                           Server.MapPath("~/EmailTemplates/CustomerNewOption.aspx"), Message.NewOption);
//                }
//                if (PosterRole == "PRO")
//                {
//                    SendEmail(PosterEmailAddress, PosterUsername, PosterLCID, t1.ReminderLevel,
//                           Server.MapPath("~/EmailTemplates/ProfessionalNewOption.aspx"), Message.NewOption);
//                }
//            }
//            break;

//        case Message.NewOpportunity:
//            {
//                if (BidderRole == "CUS")
//                {
//                    SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
//                           Server.MapPath("~/EmailTemplates/CustomerNewOpportunity .aspx"), Message.NewOpportunity);
//                }
//                if (BidderRole == "PRO")
//                {
//                    SendEmail(BidderEmailAddress, BidderUsername, BidderLCID, t1.ReminderLevel,
//                           Server.MapPath("~/EmailTemplates/ProfessionalNewOpportunity .aspx"), Message.NewOpportunity);
//                }
//            }
//            break;
//    }

//}