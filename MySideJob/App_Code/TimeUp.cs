using System;
using System.Linq;
using SidejobModel;

/// <summary>
/// Summary description for TimeUp
/// </summary>
public class TimeUp
{
    public int BidderLCID { get; set; }
    public string BidderUsername { get; set; }
    public string BidderRole { get; set; }
    public int BidderID { get; set; }
    public int PosterLCID { get; set; }
    public string PosterUsername { get; set; }
    public string PosterRole { get; set; }
    public int PosterID { get; set; }
    public int ProjectLCID { get; set; }
    public int ProjectID { get; set; }
    public string Postermessage { get; set; }
    public string Biddermessage { get; set; }
    public string Phasetitle { get; set; }
    public string BidderEmailAddress { get; set; }
    public string PosterEmailAddress { get; set; }
    public double ChargePercentage { get; set; }
    public const string Singlespace = " ";

    public TimeUp()
    {

    }

    public TimeUp(int projectId, int action, ClosedProject cp)
    {
        //ProcessProjectAction(projectId, action, cp);
        GetBidderPosterProjectProperties(projectId, action, cp);
    }

    public void Exit()
    {

        throw new ArgumentException("ExitNow");
    }

    public void Start()
    {
        UpdateTimeLeft();
        GetFinishedProjects();
    }

    public void UpdateTimeLeft()
    {
        using (var context = new SidejobEntities())
        {
            var t = (from c in context.ProjectRequirements
                     select c).ToList();
            foreach (var t1 in t)
            {
                if (t1.EndDate.Date < DateTime.UtcNow.Date) continue;
                var result = t1.EndDate - DateTime.UtcNow;
                var days = result.Days;
                var day = days < 0 ? 0 : result.Days;
                var hours = result.Hours;
                var hour = hours < 0 ? 0 : result.Hours;
                var minutes = result.Minutes;
                var minute = minutes < 0 ? 0 : result.Minutes;
                var daysleftstring = day + " " + Resources.Resource.Days + ":" + hour + " " + Resources.Resource.Hours +
                                     ":" + minute + " " + Resources.Resource.Minutes;
                t1.TimeLeft = daysleftstring;
                context.SaveChanges();
            }
            UpdatePendingProject(context);
        }
    }

    public void UpdatePendingProject(SidejobEntities context)
    {

        var t = (from c in context.Projects
                 where c.StatusInt == 2
                 select c).ToList();
        foreach (var t1 in t)
        {
            var exist = (from c in context.PendingProjects
                         where c.ProjectID == t1.ProjectID
                         select c).ToList();
            if (exist.Count == 0)
            {
                var existprojectrequirement = (from c in context.ProjectRequirements
                                               where c.ProjectID == t1.ProjectID
                                               select c).FirstOrDefault();
                if (existprojectrequirement != null)
                {
                    var newpendingproject = new PendingProject
                                                {

                                                    ProjectID = existprojectrequirement.ProjectID,
                                                    LCID = existprojectrequirement.LCID,
                                                    JobTitle = existprojectrequirement.JobTitle,
                                                    StartDate = existprojectrequirement.StartDate,
                                                    EndDate = existprojectrequirement.EndDate,
                                                    Description = existprojectrequirement.Description,
                                                    SpecialNotes = existprojectrequirement.SpecialNotes,
                                                    Address = existprojectrequirement.Address,
                                                    DatePosted = existprojectrequirement.DatePosted
                                                };
                    context.AddToPendingProjects(newpendingproject);
                    context.SaveChanges();
                }
            }
        }
    }

    public void GetFinishedProjects()
    {
        //Check to see which projects time is finished and projects are open
        using (var context = new SidejobEntities())
        {
            var projects = (from pr in context.ProjectRequirements
                            join p in context.Projects on
                                pr.ProjectID equals p.ProjectID
                            where (pr.EndDate <= DateTime.UtcNow) && (p.StatusInt == 0)
                            select p).ToList();

            foreach (var p in projects)
            {
                //UpdateProject to Closed
                p.StatusInt = 3;
                context.SaveChanges();
                //Add To ClosedProject
                AddToClosedProject((int)p.ProjectID);

                //Check if you need to change back from var to int
                var projectID = (int)p.ProjectID;

                //Update Winner
                var bidder = (from b in context.Bids
                              where b.ProjectID == projectID
                              select b).FirstOrDefault();
                if (BidderRole == "CUS")
                {
                    CustomerWin(bidder);
                }
                if (BidderRole == "PRO")
                {
                    ProWin(bidder);
                }
            }
        }
    }

    public int GetCustomerNumberofBids(int customerId)
    {
        using (var context = new SidejobEntities())
        {
            var max = (from c in context.CustomerWinBids
                       where c.CustomerID == customerId
                       select c.NumberofBids).FirstOrDefault();

            int numberofBids;
            if (max == 0)
            {
                numberofBids = 0;
            }
            else
            {
                numberofBids = max + 1;
            }
            return numberofBids;
        }
    }

    public int GetProfessionalNumberofBids(int professionalId)
    {
        using (var context = new SidejobEntities())
        {
            var max = (from c in context.ProfessionalWinBids
                       where c.ProID == professionalId
                       select c.NumberofBids).FirstOrDefault();

            int numberofBids;
            if (max == 0)
            {
                numberofBids = 0;
            }
            else
            {
                numberofBids = max + 1;
            }
            return numberofBids;
        }
    }

    public void CustomerWin(Bid bidder)
    {
        var context = new SidejobEntities();
        var customerwinbid = new CustomerWinBid
                                 {
                                     BidID = bidder.BidID,
                                     CustomerID = bidder.BidderID,
                                     NumberofBids = GetCustomerNumberofBids(bidder.BidderID)
                                 };
        context.AddToCustomerWinBids(customerwinbid);
        // context.DeleteObject(bidder);
        context.SaveChanges();
    }

    public void ProWin(Bid bidder)
    {
        var context = new SidejobEntities();
        var professionalwinbid = new ProfessionalWinBid
                                     {
                                         BidID = bidder.BidID,
                                         ProID = bidder.BidderID,
                                         NumberofBids = GetProfessionalNumberofBids(bidder.BidderID)
                                     };
        context.AddToProfessionalWinBids(professionalwinbid);
        // context.DeleteObject(bidder);
        context.SaveChanges();
    }

    /////////////////////ADD TO CLOSED PROJECT/////////////////////////
    public void AddToClosedProject(int projectId)
    {
        int bidderid = 0;
        using (var context = new SidejobEntities())
        {
            var cp = (from c in context.ClosedProjects
                      where c.ProjectID == projectId
                      select c).FirstOrDefault();

            if (cp == null)
            {
                var bidder = (from p in context.Projects
                              where p.ProjectID == projectId
                              select p).FirstOrDefault();
                if (bidder != null)
                {
                    if (bidder.HighestBidderID != null)
                    {
                        bidderid = (int)bidder.HighestBidderID;
                    }
                }
                var chosenproject = (from p in context.Projects
                                     where p.ProjectID == projectId
                                     select p).FirstOrDefault();
                var chosenbid = (from p in context.Bids
                                 where p.BidderID == bidderid
                                 select p).FirstOrDefault();
                if (chosenproject != null && chosenbid != null)
                {
                    var newcp = new ClosedProject
                    {
                        ProjectID = projectId,
                        DateFinished = DateTime.UtcNow.Date,
                        PosterID = chosenproject.PosterID,
                        PosterRole = chosenproject.PosterRole,
                        BidderID = chosenproject.HighestBidderID,
                        BidderRole = chosenbid.BidderRole,
                        HighestBid = chosenproject.HighestBid,
                        CurrencyID = chosenbid.CurrencyID,
                        Status = 3
                    };
                    context.AddToClosedProjects(newcp);
                    context.SaveChanges();
                    GetBidderPosterProjectProperties(projectId, 0, newcp);
                }
            }
        }
    }

    /////////////////////ADD TO CLOSED PROJECT/////////////////////////

    /////////////////////Get Bidder Poster Project Properties/////////////////////////
    public void GetBidderPosterProjectProperties(int projectId, int action, ClosedProject cp)
    {
        if (cp.BidderID != null) BidderID = (int)cp.BidderID;
        PosterID = cp.PosterID;
        ProjectID = cp.ProjectID;
        ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////
        using (var context = new SidejobEntities())
        {
            if (cp.BidderRole == "CUS")
            {
                BidderRole = "CUS";
                var c1 = (from c in context.Customers
                          where c.CustomerID == (int)cp.BidderID
                          select c).FirstOrDefault();
                if (c1 != null)
                {
                    BidderLCID = c1.LCID;
                    BidderUsername = c1.UserName;
                    var cg = (from c in context.CustomerGenerals
                              where c.CustomerID == (int)cp.BidderID
                              select c).FirstOrDefault();
                    if (cg != null)
                    {
                        BidderEmailAddress = cg.EmailAddress;
                    }
                }
            }

            if (cp.BidderRole == "PRO")
            {
                BidderRole = "PRO";
                var c1 = (from c in context.Professionals
                          where c.ProID == (int)cp.BidderID
                          select c).FirstOrDefault();
                if (c1 != null)
                {
                    BidderLCID = c1.LCID;
                    BidderUsername = c1.UserName;
                    var pg = (from c in context.ProfessionalGenerals
                              where c.ProID == (int)cp.BidderID
                              select c).FirstOrDefault();
                    if (pg != null)
                    {
                        BidderEmailAddress = pg.EmailAddress;
                    }
                }
            }

            if (cp.PosterRole == "CUS")
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
                              where c.CustomerID == cp.PosterID
                              select c).FirstOrDefault();
                    if (cg != null)
                    {
                        PosterEmailAddress = cg.EmailAddress;
                    }
                }
            }

            if (cp.PosterRole == "PRO")
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
                              where c.ProID == cp.PosterID
                              select c).FirstOrDefault();
                    if (pg != null)
                    {
                        PosterEmailAddress = pg.EmailAddress;
                    }
                }
            }

            var p1 = (from c in context.ProjectRequirements
                      where c.ProjectID == ProjectID
                      select c).FirstOrDefault();
            if (p1 != null) ProjectLCID = p1.LCID;
        }
        ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////

        ProcessProjectAction(projectId, action, cp);
    }

    ////////////////////////Get Bidder Poster Project Properties//////////////////////////

    ////////////////////////////Process Action//////////////////////////////////
    public void ProcessProjectAction(int projectId, int action, ClosedProject cp)
    {
        var context = new SidejobEntities();
        var numberofbids = (from c in context.Bids
                            where c.ProjectID == projectId
                            select c).Count();
        if (numberofbids == 0)
        {
            Postermessage = Resources.Resource.ProjectExpiration + Singlespace + projectId
                            + Resources.Resource.zerobids + Resources.Resource.ProjectExtention;
            //  No One had any bids on the project
            Phasetitle = Resources.Resource.ProjectExpirationPhase;
            //  No One had any bids on the project
        }
        else
        {
            //There is Bids on the project
            Postermessage = Resources.Resource.PosterProjectCongratulation + Singlespace + projectId + Singlespace;
            Postermessage += Resources.Resource.PosterProjectBidReach;

            if (cp.CurrencyID != null)
                Postermessage += "<b>" + cp.HighestBid +
                                 ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
            Postermessage += Resources.Resource.TeamContactBidder;

            Biddermessage = Resources.Resource.BidderCongratulation;
            if (cp.CurrencyID != null)
                Biddermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
            Biddermessage += Singlespace + Resources.Resource.ForProject + Singlespace + projectId + "<br/>";
            Biddermessage += Resources.Resource.BidderRequestPayment;

            Phasetitle = Resources.Resource.BidWonPhase;

            switch (action)
            {
                case 0:
                    //  ---------------------------------Action 0:--------------------------------------
                    // Project Status 3
                    //------------------------The time of the Project is finished---------------------

                    if (cp.PosterRole == "CUS")
                    {
                        CustomerPayment(cp,"Poster");
                    }
                    if (cp.PosterRole == "PRO")
                    {
                        ProfessionalPayment(cp, "Poster");
                    }

                      if (cp.BidderRole == "CUS")
                    {
                        CustomerPayment(cp,"Bidder");
                    }
                      if (cp.BidderRole == "PRO")
                    {
                        ProfessionalPayment(cp, "Bidder");
                    }
                    UpdateResponseDelay(1, cp, context);
                    break;

                case 2:
                    // ---------------------------------Action 2:--------------------------------------
                    // Project Status 4
                    //----------------------------Bidder Pay Fees-------------------------------------
                    BidderPayFees(cp);
                    UpdateResponseDelay(2, cp, context);
                    break;
                case 4:
                    // ---------------------------------Action 4:--------------------------------------
                    // Project Status 5
                    //----------------------------Poster Pay Fees-------------------------------------
                    PosterPayFees(projectId, cp);
                    GenerateContract(cp);
                    UpdateResponseDelay(3, cp, context);
                    break;


            }

            if (action == 0 || action == 2 || action == 4)
            {
                ProcessAction(cp);
                AutomatedMessage();
            }

            //There is Bids on the project
        }
    }

    ////////////////////////////Process Action//////////////////////////////////

    public void BidderPayFees(ClosedProject cp)
    {
        //        ----------------------------Bidder Pay Fees-------------------------------------

        var context = new SidejobEntities();
        cp.Status = 4;
        context.SaveChanges();

        Postermessage = Resources.Resource.PosterBidCongratulation;
        if (cp.CurrencyID != null)
            Postermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Postermessage += Singlespace + Resources.Resource.PosterRequestPayment;
        Biddermessage = Resources.Resource.BidderPaymentConfirmation;
        Phasetitle = Resources.Resource.BidConfirmationPhase;

        //---------------------------------Action 2:--------------------------------------

    }

    public void PosterPayFees(int projectId, ClosedProject cp)
    {

        //        ---------------------------------Action 4:--------------------------------------
        //----------------------------Poster Pay Fees-------------------------------------
        var context = new SidejobEntities();
        cp.Status = 5;
        context.SaveChanges();
        Postermessage = "";
        Postermessage = Resources.Resource.PosterProjectCongratulation + Singlespace + projectId;
        //Postermessage += Resources.Resource.ForProject + Singlespace + projectId;
        Postermessage += Singlespace + Resources.Resource.WithTheBidOf + Singlespace;

        if (cp.CurrencyID != null)
            Postermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Postermessage += Singlespace + Resources.Resource.PosterPrintContract;

        Biddermessage = "";
        Biddermessage = Resources.Resource.BidderProjectBidReach + Singlespace + projectId;
        // Biddermessage += Resources.Resource.ForProject + Singlespace + projectId;
        Biddermessage += Singlespace + Resources.Resource.WithTheBidOf;
        //  Biddermessage += Resources.Resource.BidderCongratulation;

        if (cp.CurrencyID != null)
            Biddermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Biddermessage += Resources.Resource.BidderPrintContract;
        Phasetitle = Resources.Resource.ProjectContractPhase;


        //---------------------------------Action 4:--------------------------------------
        //----------------------------Poster Pay Fees-------------------------------------
    }

    public void TestPosterPayFees(int projectId, ClosedProject cp)
    {

        //        ---------------------------------Action 4:--------------------------------------
        //----------------------------Poster Pay Fees-------------------------------------
        var context = new SidejobEntities();
        cp.Status = 5;
        context.SaveChanges();
        Postermessage = "";
        Postermessage = Resources.Resource.PosterProjectCongratulation + Singlespace + projectId;
        //Postermessage += Resources.Resource.ForProject + Singlespace + projectId;
        Postermessage += Singlespace + Resources.Resource.WithTheBidOf + Singlespace;

        if (cp.CurrencyID != null)
            Postermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Postermessage += Singlespace + Resources.Resource.PosterPrintContract;

        Biddermessage = "";
        Biddermessage = Resources.Resource.BidderProjectBidReach + Singlespace + projectId;
        // Biddermessage += Resources.Resource.ForProject + Singlespace + projectId;
        Biddermessage += Singlespace + Resources.Resource.WithTheBidOf;
      //  Biddermessage += Resources.Resource.BidderCongratulation;

        if (cp.CurrencyID != null)
            Biddermessage += "<b>" + cp.HighestBid + ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Biddermessage += Resources.Resource.BidderPrintContract;
        Phasetitle = Resources.Resource.ProjectContractPhase;

       
        //---------------------------------Action 4:--------------------------------------
        //----------------------------Poster Pay Fees-------------------------------------
    }

    public void ProcessAction(ClosedProject cp)
    {
        if (cp.BidderRole == "CUS")
        {
            ProcessCustomerBidder(cp);
        }
        if (cp.BidderRole == "PRO")
        {
            ProcessProfessionalBidder(cp);
        }
        if (cp.PosterRole == "CUS")
        {
            ProcessCustomerPoster(cp);
        }
        if (cp.PosterRole == "PRO")
        {
            ProcessProfessionalPoster(cp);
        }
    }

    public void AutomatedMessage()
    {
        var context = new SidejobEntities();
        var bidderautomatedmessage = new AutomatedMessage
                                         {
                                             EmailAddress = BidderEmailAddress,
                                             Title = Phasetitle,
                                             Message = Biddermessage
                                         };
        var posterautomatedmessage = new AutomatedMessage
                                         {
                                             EmailAddress = PosterEmailAddress,
                                             Title = Phasetitle,
                                             Message = Postermessage
                                         };
        context.AddToAutomatedMessages(bidderautomatedmessage);
        context.AddToAutomatedMessages(posterautomatedmessage);
        context.SaveChanges();
    }

    public void InsertCustomerEvent(int applicantId)
    {
        var context = new SidejobEntities();
        var customerEvent = new CustomerEvent
                                {
                                    CustomerID = applicantId,
                                    EventID = GetNextEventID(),
                                    NumberofEvents = 1,
                                    Type = 1
                                };

        context.AddToCustomerEvents(customerEvent);
        context.SaveChanges();
    }

    public void InsertProfessionalEvent(int applicantId)
    {
        var context = new SidejobEntities();
        var professionalEvent = new ProfessionalEvent
                                    {
                                        ProID = applicantId,
                                        EventID = GetNextEventID(),
                                        NumberofEvents = 1,
                                        Type = 1
                                    };
        context.AddToProfessionalEvents(professionalEvent);
        context.SaveChanges();
    }

    public void InsertEvents(int applicantId, int applicationlcid, string applicationType, int eventid)
    {
        var context = new SidejobEntities();
        //int nextEventID = GetNextEventID();
        //context.InsertEvent(nextEventID, DateTime.Now.Date, applicationlcid);
        var generalEvent = new Event
                               {
                                   EventID = eventid,
                                   ApplicantType = applicationType,
                                   ApplicantID = applicantId,
                                   EventDescription = Phasetitle,
                                   DateEvent = DateTime.UtcNow.Date,
                                   LCID = ProjectLCID
                               };


        context.AddToEvents(generalEvent);
        context.SaveChanges();
    }

    public void InsertCustomerMessageInbox(int applicantId, string applicationusername, string eventdescription,
                                           string message)
    {
        var context = new SidejobEntities();
        var customerMessageInbox = new CustomerMessageInbox
                                       {
                                           MessageID = GetNextMessageID(),
                                           CustomerID = applicantId,
                                           SenderName = "My-Side-Job",
                                           ReceiverName = applicationusername,
                                           Date = DateTime.UtcNow.Date,
                                           Title = eventdescription,
                                           Description = message,
                                           Checked = 1,
                                           Response = 0
                                       };
        context.AddToCustomerMessageInboxes(customerMessageInbox);
        context.SaveChanges();
    }

    public void InsertProfessionalMessageInbox(int applicantId, string applicationusername, string eventdescription,
                                               string message)
    {
        var context = new SidejobEntities();
        var professionalMessageInbox = new ProfessionalMessageInbox
                                           {
                                               MessageID = GetNextMessageID(),
                                               ProID = applicantId,
                                               SenderName = "My-Side-Job",
                                               ReceiverName = applicationusername,
                                               Date = DateTime.UtcNow.Date,
                                               Title = eventdescription,
                                               Description = message,
                                               Checked = 1,
                                               Response = 0
                                           };
        context.AddToProfessionalMessageInboxes(professionalMessageInbox);
        context.SaveChanges();
    }

    public void InsertMessage(int applicantId, string applicationusername, string eventdescription, string message,
                              int applicationlcid, string applicationType)
    {
        var context = new SidejobEntities();
        var generalmessage = new Message
                                 {
                                     MessageID = GetNextMessageID(),
                                     SenderType = "A",
                                     SenderID = 0,
                                     SenderName = "My-SIDE-JOB",
                                     ReceiverType = applicationType,
                                     ReceiverID = applicantId,
                                     ReceiverName = applicationusername,
                                     Title = eventdescription,
                                     Description = message,
                                     DateMessage = DateTime.UtcNow.Date,
                                     NumberofSenderInobx = 0,
                                     NumberofSenderOutbox = 0,
                                     NumberofSenderSaved = 0,
                                     NumberofReceiverInbox = 1,
                                     NumberofReceiverOutbox = 0,
                                     NmberofReceiverSaved = 0,
                                     NumberofSenderTotal = 0,
                                     NumberofReceiverTotal = 1,
                                     LCID = applicationlcid
                                 };
        context.AddToMessages(generalmessage);
        context.SaveChanges();
    }

    public static int GetNextEventID()
    {
        int id;
        var context = new SidejobEntities();
        var max = context.Events.OrderByDescending(s => s.EventID).FirstOrDefault();

        if (max == null)
        {
            id = 0;
        }
        else
        {
            id = max.EventID + 1;
        }
        return id;
    }

    public static int GetNextMessageID()
    {
        int id;
        var context = new SidejobEntities();
        var max = context.Messages.OrderByDescending(s => s.MessageID).FirstOrDefault();

        if (max == null)
        {
            id = 0;
        }
        else
        {
            id = max.MessageID + 1;
        }
        return id;
    }

    public void ProcessCustomerBidder(ClosedProject cp)
    {
        if (cp.BidderID != null)
        {
            InsertEvents((int)cp.BidderID, BidderLCID, "C", GetNextEventID());
            InsertMessage((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage, BidderLCID, "C");
            InsertCustomerMessageInbox((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage);
            InsertCustomerEvent((int)cp.BidderID);
        }
    }

    public void ProcessProfessionalBidder(ClosedProject cp)
    {
        if (cp.BidderID != null)
        {
            InsertEvents((int)cp.BidderID, BidderLCID, "P", GetNextEventID() + 1);
            InsertMessage((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage, BidderLCID, "P");
            InsertProfessionalMessageInbox((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage);
            InsertProfessionalEvent((int)cp.BidderID);
        }
    }

    public void ProcessCustomerPoster(ClosedProject cp)
    {
        if (cp.PosterID != 0)
        {
            InsertEvents(cp.PosterID, PosterLCID, "C", GetNextEventID() + 2);
            InsertMessage(cp.PosterID, PosterUsername, Phasetitle, Postermessage, PosterLCID, "C");
            InsertCustomerMessageInbox(cp.PosterID, PosterUsername, Phasetitle, Postermessage);
            InsertCustomerEvent(cp.PosterID);
        }
    }

    public void ProcessProfessionalPoster(ClosedProject cp)
    {
        if (cp.PosterID != 0)
        {
            InsertEvents(cp.PosterID, PosterLCID, "P", GetNextEventID() + 3);
            InsertMessage(cp.PosterID, PosterUsername, Phasetitle, Postermessage, PosterLCID, "P");
            InsertProfessionalMessageInbox(cp.PosterID, PosterUsername, Phasetitle, Postermessage);
            InsertProfessionalEvent(cp.PosterID);
        }
    }

    public void CustomerPayment(ClosedProject cp, string role)
    {
        var phase = "";
        int payerId = 0;
        if(role == "Poster")
        {
            payerId = PosterID;
            phase = Resources.Resource.Phase2;
        }
        if(role == "Bidder")
        {
            payerId = BidderID;
            phase = Resources.Resource.Phase1;
        }
        if (cp.HighestBid != null)
        {
            var projectPaymentDue = Math.Round((double)(GetPercentage((double)cp.HighestBid) * cp.HighestBid), 2);
            var phasemessage = "<b><font color='red'>" + phase+ ":</font></b><br>" +
                               Resources.Resource.Project + " = "
                               + cp.ProjectID + "<br/>" + Resources.Resource.PaymentDue + " = " + projectPaymentDue;

            var context = new SidejobEntities();
            if (cp.BidderID != null)
            {
                if (cp.CurrencyID != null)
                {
                    var customerpaymentdue = new CustomerPaymentDue
                                                 {
                                                     CustomerID = payerId,
                                                     ProjectID = cp.ProjectID,
                                                     ProjectAmount = (double)cp.HighestBid,
                                                     Date = DateTime.UtcNow.Date,
                                                     CurrencyCode = ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID),
                                                     PaymentDue = projectPaymentDue,
                                                     PhaseStatus = phasemessage,
                                                     PaymentProcess = false
                                                 };
                    context.AddToCustomerPaymentDues(customerpaymentdue);
                }
                context.SaveChanges();
            }
        }
    }

    public void ProfessionalPayment(ClosedProject cp, string role)
    {
        int payerId = 0;
        var phase = "";
        if (role == "Poster")
        {
            payerId = PosterID;
            phase = Resources.Resource.Phase2;
        }
        if (role == "Bidder")
        {
            payerId = BidderID;
            phase = Resources.Resource.Phase1;
        }

        if (cp.HighestBid != null)
        {
            var projectPaymentDue = Math.Round((double)(GetPercentage((double)cp.HighestBid) * cp.HighestBid), 2);
            var phasemessage = "<b><font color='red'>" + phase + ":</font></b><br>" +
                               Resources.Resource.Project + " = "
                               + cp.ProjectID + "<br/>" + Resources.Resource.PaymentDue + " = " + projectPaymentDue;

            var context = new SidejobEntities();
            if (cp.BidderID != null)
            {
                if (cp.CurrencyID != null)
                {
                    var professionalpaymentdue = new ProfessionalPaymentDue
                                                     {
                                                         ProID = payerId,
                                                         ProjectID = cp.ProjectID,
                                                         ProjectAmount = (double)cp.HighestBid,
                                                         Date = DateTime.UtcNow.Date,
                                                         CurrencyCode =
                                                             ScheduleUtility.GetCurrencyCode((int)cp.CurrencyID),
                                                         PaymentDue = projectPaymentDue,
                                                         PhaseStatus = phasemessage,
                                                         PaymentProcess = false
                                                     };
                    context.AddToProfessionalPaymentDues(professionalpaymentdue);
                }
                context.SaveChanges();
            }
        }
    }

    public double GetPercentage(double highestbid)
    {

        if (highestbid >= 0.00 && highestbid <= 20.00)
        {
            ChargePercentage = 0.35;
        }
        else if (highestbid >= 20.00 && highestbid <= 50.00)
        {
            ChargePercentage = 0.30;
        }
        else if (highestbid >= 50.00 && highestbid <= 100.00)
        {
            ChargePercentage = 0.25;
        }
        else if (highestbid >= 100.00 && highestbid <= 300.00)
        {
            ChargePercentage = 0.20;
        }
        else if (highestbid >= 300.00 && highestbid <= 500.00)
        {
            ChargePercentage = 0.15;
        }
        else if (highestbid >= 500.00 && highestbid <= 1000.00)
        {
            ChargePercentage = 0.10;
        }
        else if (highestbid > 1000.00)
        {
            ChargePercentage = 0.05;
        }

        return ChargePercentage;
    }

    public static int GetNextContractID()
    {
        int id;
        var context = new SidejobEntities();
        var max = context.Contracts.OrderByDescending(s => s.ContractID).FirstOrDefault();

        if (max == null)
        {
            id = 0;
        }
        else
        {
            id = max.ContractID + 1;
        }
        return id;
    }

    public void GenerateContract(ClosedProject cp)
    {
        using (var context = new SidejobEntities())
        {
            BidderPosterContract(cp, context);
        }

    }

    public static void BidderPosterContract(ClosedProject cp, SidejobEntities context)
    {
        ProfessionalGeneral professional;
        if (cp.PosterRole == "CUS" & cp.BidderRole == "PRO")
        {
            var customer = (from c in context.CustomerGenerals
                                        where c.CustomerID == cp.PosterID
                                        select c).FirstOrDefault();
            professional = (from c in context.ProfessionalGenerals
                            where c.ProID == cp.BidderID
                            select c).FirstOrDefault();
            CustomerContract(customer, professional, cp);
            ProfessionalContract(customer, professional, cp);
            Contract(customer, professional, cp);
        }

        if (cp.PosterRole == "PRO" & cp.BidderRole == "PRO")
        {
            var professionalbidder = (from c in context.ProfessionalGenerals
                                      where c.ProID == cp.BidderID
                                      select c).FirstOrDefault();
            professional = (from c in context.ProfessionalGenerals
                            where c.ProID == cp.PosterID
                            select c).FirstOrDefault();
            //both contract to Professionals
            ProfessionalContract(professional, professionalbidder, cp);
            Contract(professional, professionalbidder, cp);
        }
    }

    public static void CustomerContract(CustomerGeneral customer, ProfessionalGeneral professional, ClosedProject cp)
    {
        if (customer == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var customercontract = new CustomerContract
        {
            BidderID = professional.ProID,
            BidderFirstName = professional.FirstName,
            BidderLastName = professional.LastName,
            BidderUsername = professional.UserName,
            ContractDate = DateTime.UtcNow,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            CustomerID = customer.CustomerID,
            HighestBid = (double)cp.HighestBid,
            PosterID = customer.CustomerID,
            PosterUsername = customer.UserName,
            PosterFirstName = customer.FirstName,
            PosterLastName = customer.LastName
        };
        context.AddToCustomerContracts(customercontract);
        context.SaveChanges();
    }

    public static void ProfessionalContract(CustomerGeneral customer, ProfessionalGeneral professional, ClosedProject cp)
    {
        if (customer == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var professionalcontract = new ProfessionalContract
        {
            BidderID = professional.ProID,
            BidderFirstName = professional.FirstName,
            BidderLastName = professional.LastName,
            BidderUsername = professional.UserName,
            ContractDate = DateTime.UtcNow,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            ProID = professional.ProID,
            HighestBid = (double)cp.HighestBid,
            PosterID = customer.CustomerID,
            PosterUsername = customer.UserName,
            PosterFirstName = customer.FirstName,
            PosterLastName = customer.LastName
        };
        context.AddToProfessionalContracts(professionalcontract);
        context.SaveChanges();
    }

    public static void ProfessionalContract(ProfessionalGeneral professionalposter,ProfessionalGeneral professionalbidder, ClosedProject cp)
    {
        if (professionalposter == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var professionalbiddercontract = new ProfessionalContract
        {
            BidderID = professionalbidder.ProID,
            BidderFirstName = professionalbidder.FirstName,
            BidderLastName = professionalbidder.LastName,
            BidderUsername = professionalbidder.UserName,
            ContractDate = DateTime.UtcNow,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            ProID = professionalbidder.ProID,
            HighestBid = (double)cp.HighestBid,
            PosterID = professionalposter.ProID,
            PosterUsername = professionalposter.UserName,
            PosterFirstName = professionalposter.FirstName,
            PosterLastName = professionalposter.LastName
        };
        var professionalpostercontract = new ProfessionalContract
        {
            BidderID = professionalbidder.ProID,
            BidderFirstName = professionalbidder.FirstName,
            BidderLastName = professionalbidder.LastName,
            BidderUsername = professionalbidder.UserName,
            ContractDate = DateTime.UtcNow,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID()+ 1,
            CurrencyID = (int)cp.CurrencyID,
            ProID = professionalposter.ProID,
            HighestBid = (double)cp.HighestBid,
            PosterID = professionalposter.ProID,
            PosterUsername = professionalposter.UserName,
            PosterFirstName = professionalposter.FirstName,
            PosterLastName = professionalposter.LastName
        };
        context.AddToProfessionalContracts(professionalpostercontract);
        context.AddToProfessionalContracts(professionalbiddercontract);
        context.SaveChanges();
    }

    public static void Contract(CustomerGeneral customer, ProfessionalGeneral professional, ClosedProject cp)
    {
        var context = new SidejobEntities();
        var project = (from c in context.ProjectRequirements
                       where c.ProjectID == cp.ProjectID
                       select c).FirstOrDefault();
        if (project != null)
        {
            var contract = new Contract
                               {
                                   ContractID = GetNextContractID(),
                                   ProjectID = cp.ProjectID,
                                   ContractDate = DateTime.UtcNow.Date,
                                   BidderID = professional.ProID,
                                   BidderRole = "PRO",
                                   BidderUsername = professional.UserName,
                                   BidderFirstName = professional.FirstName,
                                   BidderLastName = professional.LastName,
                                   BidderAddress = professional.Address,
                                   BidderCountryID = professional.CountryID,
                                   BidderCountryName = professional.CountryName,
                                   BidderRegionID = professional.RegionID,
                                   BidderRegionName = professional.RegionName,
                                   BidderHomePhoneNumber = professional.HomePhoneNumber,
                                   PosterID = customer.CustomerID,
                                   PosterRole = "CUS",
                                   PosterUsername = customer.UserName,
                                   PosterFirstName = customer.FirstName,
                                   PosterLastName = customer.LastName,
                                   PosterAddress = customer.Address,
                                   PosterCountryID = customer.CountryID,
                                   PosterCountryName = customer.CountryName,
                                   PosterRegionID = customer.RegionID,
                                   PosterRegionName = customer.RegionName,
                                   PosterZipcode = customer.Zipcode,
                                   LCID = project.LCID,
                                   ProjectCategoryID = project.CategoryID,
                                   ProjectCategoryName = project.CategoryName,
                                   ProjectJobID = project.JobID,
                                   ProjectExperienceID = project.ExperienceID,
                                   ProjectCrewNumberID = project.CrewNumberID,
                                   ProjectLicensedID = project.LicensedID,
                                   ProjectInsuredID = project.InsuredID,
                                   ProjectRelocationID = project.RelocationID,
                                   ProjectStartDate = project.StartDate,
                                   ProjectEndDate = project.EndDate,
                                   ProjectAddress = project.Address,
                                   ProjectCountryID = project.CountryID,
                                   ProjectCountryName = project.CountryName,
                                   ProjectRegionID = project.RegionID,
                                   ProjectRegionName = project.RegionName,
                               };
            context.AddToContracts(contract);
            context.SaveChanges();
        }
    }

    public static void Contract(CustomerGeneral customer, CustomerGeneral customerposter, ClosedProject cp)
    {
        var context = new SidejobEntities();
        var project = (from c in context.ProjectRequirements
                       where c.ProjectID == cp.ProjectID
                       select c).FirstOrDefault();

        if (project != null)
        {
            var contract = new Contract
                               {
                                   ContractID = GetNextContractID(),
                                   ProjectID = cp.ProjectID,
                                   ContractDate = DateTime.UtcNow.Date,
                                   BidderID = customerposter.CustomerID,
                                   BidderRole = "CUS",
                                   BidderUsername = customerposter.UserName,
                                   BidderFirstName = customerposter.FirstName,
                                   BidderLastName = customerposter.LastName,
                                   BidderAddress = customerposter.Address,
                                   BidderCountryID = customerposter.CountryID,
                                   BidderCountryName = customerposter.CountryName,
                                   BidderRegionID = customerposter.RegionID,
                                   BidderRegionName = customerposter.RegionName,
                                   BidderHomePhoneNumber = customerposter.HomePhoneNumber,
                                   PosterID = customer.CustomerID,
                                   PosterRole = "CUS",
                                   PosterUsername = customer.UserName,
                                   PosterFirstName = customer.FirstName,
                                   PosterLastName = customer.LastName,
                                   PosterAddress = customer.Address,
                                   PosterCountryID = customer.CountryID,
                                   PosterCountryName = customer.CountryName,
                                   PosterRegionID = customer.RegionID,
                                   PosterRegionName = customer.RegionName,
                                   PosterZipcode = customer.Zipcode,
                                   LCID = project.LCID,
                                   ProjectCategoryID = project.CategoryID,
                                   ProjectCategoryName = project.CategoryName,
                                   ProjectJobID = project.JobID,
                                   ProjectExperienceID = project.ExperienceID,
                                   ProjectCrewNumberID = project.CrewNumberID,
                                   ProjectLicensedID = project.LicensedID,
                                   ProjectInsuredID = project.InsuredID,
                                   ProjectRelocationID = project.RelocationID,
                                   ProjectStartDate = project.StartDate,
                                   ProjectEndDate = project.EndDate,
                                   ProjectAddress = project.Address,
                                   ProjectCountryID = project.CountryID,
                                   ProjectCountryName = project.CountryName,
                                   ProjectRegionID = project.RegionID,
                                   ProjectRegionName = project.RegionName,
                               };
            context.AddToContracts(contract);
            context.SaveChanges();
        }
    }

    public static void Contract(ProfessionalGeneral professionalbidder, ProfessionalGeneral professional,
                                ClosedProject cp)
    {
        var context = new SidejobEntities();
        var project = (from c in context.ProjectRequirements
                       where c.ProjectID == cp.ProjectID
                       select c).FirstOrDefault();

        if (project != null)
        {
            var contract = new Contract
                               {
                                   ContractID = GetNextContractID(),
                                   ProjectID = cp.ProjectID,
                                   ContractDate = DateTime.UtcNow.Date,
                                   BidderID = professional.ProID,
                                   BidderRole = "PRO",
                                   BidderUsername = professional.UserName,
                                   BidderFirstName = professional.FirstName,
                                   BidderLastName = professional.LastName,
                                   BidderAddress = professional.Address,
                                   BidderCountryID = professional.CountryID,
                                   BidderCountryName = professional.CountryName,
                                   BidderRegionID = professional.RegionID,
                                   BidderRegionName = professional.RegionName,
                                   BidderHomePhoneNumber = professional.HomePhoneNumber,
                                   PosterID = professionalbidder.ProID,
                                   PosterRole = "PRO",
                                   PosterUsername = professionalbidder.UserName,
                                   PosterFirstName = professionalbidder.FirstName,
                                   PosterLastName = professionalbidder.LastName,
                                   PosterAddress = professionalbidder.Address,
                                   PosterCountryID = professionalbidder.CountryID,
                                   PosterCountryName = professionalbidder.CountryName,
                                   PosterRegionID = professionalbidder.RegionID,
                                   PosterRegionName = professionalbidder.RegionName,
                                   PosterZipcode = professionalbidder.Zipcode,
                                   LCID = project.LCID,
                                   ProjectCategoryID = project.CategoryID,
                                   ProjectCategoryName = project.CategoryName,
                                   ProjectJobID = project.JobID,
                                   ProjectExperienceID = project.ExperienceID,
                                   ProjectCrewNumberID = project.CrewNumberID,
                                   ProjectLicensedID = project.LicensedID,
                                   ProjectInsuredID = project.InsuredID,
                                   ProjectRelocationID = project.RelocationID,
                                   ProjectStartDate = project.StartDate,
                                   ProjectEndDate = project.EndDate,
                                   ProjectAddress = project.Address,
                                   ProjectCountryID = project.CountryID,
                                   ProjectCountryName = project.CountryName,
                                   ProjectRegionID = project.RegionID,
                                   ProjectRegionName = project.RegionName,
                               };
            context.AddToContracts(contract);
            context.SaveChanges();
        }
    }

    public static void Contract(ProfessionalGeneral professionalbidder, CustomerGeneral customerposter, ClosedProject cp)
    {
        var context = new SidejobEntities();
        var project = (from c in context.ProjectRequirements
                       where c.ProjectID == cp.ProjectID
                       select c).FirstOrDefault();

        if (project != null)
        {
            var contract = new Contract
                               {
                                   ContractID = GetNextContractID(),
                                   ProjectID = cp.ProjectID,
                                   ContractDate = DateTime.UtcNow.Date,
                                   BidderID = customerposter.CustomerID,
                                   BidderRole = "CUS",
                                   BidderUsername = customerposter.UserName,
                                   BidderFirstName = customerposter.FirstName,
                                   BidderLastName = customerposter.LastName,
                                   BidderAddress = customerposter.Address,
                                   BidderCountryID = customerposter.CountryID,
                                   BidderCountryName = customerposter.CountryName,
                                   BidderRegionID = customerposter.RegionID,
                                   BidderRegionName = customerposter.RegionName,
                                   BidderHomePhoneNumber = customerposter.HomePhoneNumber,
                                   PosterID = professionalbidder.ProID,
                                   PosterRole = "PRO",
                                   PosterUsername = professionalbidder.UserName,
                                   PosterFirstName = professionalbidder.FirstName,
                                   PosterLastName = professionalbidder.LastName,
                                   PosterAddress = professionalbidder.Address,
                                   PosterCountryID = professionalbidder.CountryID,
                                   PosterCountryName = professionalbidder.CountryName,
                                   PosterRegionID = professionalbidder.RegionID,
                                   PosterRegionName = professionalbidder.RegionName,
                                   PosterZipcode = professionalbidder.Zipcode,
                                   LCID = project.LCID,
                                   ProjectCategoryID = project.CategoryID,
                                   ProjectCategoryName = project.CategoryName,
                                   ProjectJobID = project.JobID,
                                   ProjectExperienceID = project.ExperienceID,
                                   ProjectCrewNumberID = project.CrewNumberID,
                                   ProjectLicensedID = project.LicensedID,
                                   ProjectInsuredID = project.InsuredID,
                                   ProjectRelocationID = project.RelocationID,
                                   ProjectStartDate = project.StartDate,
                                   ProjectEndDate = project.EndDate,
                                   ProjectAddress = project.Address,
                                   ProjectCountryID = project.CountryID,
                                   ProjectCountryName = project.CountryName,
                                   ProjectRegionID = project.RegionID,
                                   ProjectRegionName = project.RegionName,
                               };
            context.AddToContracts(contract);
            context.SaveChanges();
        }
    }

    public void UpdateResponseDelay(int action, ClosedProject cp, SidejobEntities context)
    {
        if (action == 0) return;
        switch (action)
        {
            case 1:
                var rd = new ResponseDelay
                             {
                                 ProjectID = ProjectID,
                                 BidderID = BidderID,
                                 CurrencyID = cp.CurrencyID,
                                 BidderRole = BidderRole,
                                 DateFinished = DateTime.UtcNow.Date,
                                 HighestBid = cp.HighestBid,
                                 PosterID = PosterID,
                                 PosterRole = PosterRole,
                                 ReminderLevel = 0,
                                 Status = 3
                             };
                context.AddToResponseDelays(rd);
                context.SaveChanges();
                break;

            case 2:
                var rd2 = (from c in context.ResponseDelays
                           where c.BidderID == BidderID
                                 && c.PosterID == PosterID && c.Status == 3
                           select c).FirstOrDefault();
                if (rd2 != null)
                {
                    rd2.Status = 4;
                    rd2.ReminderLevel = 0;
                    context.SaveChanges();
                }
                break;

            case 3:
                ArchiveProject(context);
                var rd3 = (from c in context.ResponseDelays
                           where c.BidderID == BidderID
                                 && c.PosterID == PosterID
                           select c).FirstOrDefault();
                if (rd3 != null)
                {
                    context.DeleteObject(rd3);
                    context.DeleteObject(cp);
                    var p = (from c in context.Projects
                             where c.ProjectID == ProjectID
                             select c).FirstOrDefault();
                    if (p != null)
                    {
                        p.StatusInt = 6;
                        context.SaveChanges();
                    }

                }
                break;
        }
    }

    public void ArchiveProject(SidejobEntities context)
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
                var acp = new ArchievedCompletedProject
                              {
                                  ProjectID = (int)currentproject.ProjectID,
                                  DateFinished = DateTime.UtcNow.Date,
                                  PosterID = PosterID,
                                  PosterRole = PosterRole,
                                  BidderID = BidderID,
                                  BidderRole = BidderRole,
                                  HighestBid = currentproject.HighestBid,
                                  Currency = currentprojectrequirement.CurrencyID,
                                  Status = currentproject.StatusInt
                              };
                context.AddToArchievedCompletedProjects(acp);
                context.SaveChanges();
            }
        }
        foreach (var cp in currentprojectphoto)
        {
            var ac = new ArchievedCompletedProjectPhoto
                         {
                             PhotoID = cp.PhotoID,
                             ProjectID = cp.ProjectID,
                             PhotoPath = cp.PhotoPath,
                             Caption = cp.Caption,
                             AlbumID = cp.AlbumID,
                             PhotoRank = cp.PhotoRank
                         };
            context.AddToArchievedCompletedProjectPhotoes(ac);
            context.SaveChanges();
        }

        if (currentprojectpicture != null)
        {
            var acpi = new ArchievedCompletedProjectPicture
                           {
                               AlbumID = currentprojectpicture.AlbumID,
                               AlbumCaption = currentprojectpicture.AlbumCaption,
                               Numberofimages = currentprojectpicture.Numberofimages,
                               ProjectID = currentprojectpicture.ProjectID
                           };
            context.AddToArchievedCompletedProjectPictures(acpi);
            context.SaveChanges();
        }

        if (currentprojectrequirement != null)
        {
            var acpr = new ArchievedCompletedProjectRequirement
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

            context.AddToArchievedCompletedProjectRequirements(acpr);
            context.SaveChanges();
        }
    
       
        foreach (var ccp in currentprojectphoto)
        {
            context.DeleteObject(ccp);
        }
       
        context.DeleteObject(currentprojectrequirement);  
        context.DeleteObject(currentprojectpicture);  
        context.DeleteObject(currentproject);
        context.SaveChanges();
        CleanProjectData(context);
        ArchiveSuccessfulIPN(context);
    }

    public void CleanProjectData(SidejobEntities context)
    {
        var cp = (from c in context.ClosedProjects
                  where c.ProjectID == ProjectID
                  select c).ToList();
        if (cp.Count != 0)
        {
            foreach (var ccp in cp)
            {
                context.DeleteObject(ccp);
            }
        }
        context.SaveChanges();


        var rd = (from c in context.ResponseDelays
                  where c.ProjectID == ProjectID
                  select c).ToList();
        if (rd.Count != 0)
        {
            foreach (var crd in rd)
            {
                context.DeleteObject(crd);
            }
        }
        context.SaveChanges();

    }

    public void ArchiveSuccessfulIPN(SidejobEntities context)
    {
        var customersuccessfulIPN = (from c in context.CustomerSuccesfulIPNs
                                     where c.ProjectID == ProjectID
                                     select c).ToList();

        if (customersuccessfulIPN.Count != 0)
        {
            foreach (var acsipn in customersuccessfulIPN.Select(csipn => new ArchivedCustomerSuccesfulIPN
                                                                             {
                                                                                 IPNID = csipn.IPNID,
                                                                                 GrossTotal = csipn.GrossTotal,
                                                                                 Invoice = csipn.Invoice,
                                                                                 PaymentStatus = csipn.PaymentStatus,
                                                                                 FirstName = csipn.FirstName,
                                                                                 LastName = csipn.LastName,
                                                                                 PaymentFee = csipn.PaymentFee,
                                                                                 BusinessEmail = csipn.BusinessEmail,
                                                                                 ReceiverEmail = csipn.ReceiverEmail,
                                                                                 ItemName = csipn.ItemName,
                                                                                 CurrencyCode = csipn.CurrencyCode,
                                                                                 TransactionId = csipn.TransactionId,
                                                                                 Custom = csipn.Custom,
                                                                                 SubscriberId = csipn.SubscriberId,
                                                                                 CustomerID = csipn.CustomerID,
                                                                                 ProjectID = csipn.ProjectID,
                                                                                 TxType = csipn.TxType,
                                                                                 PendingReason = csipn.PendingReason,
                                                                                 PaymentDate = csipn.PaymentDate,
                                                                                 Address = csipn.Address,
                                                                                 City = csipn.City,
                                                                                 State = csipn.State,
                                                                                 Zip = csipn.Zip,
                                                                                 Country = csipn.Country,
                                                                                 CountryCode = csipn.CountryCode,
                                                                                 AddressStatus = csipn.AddressStatus,
                                                                                 PayerStatus = csipn.PayerStatus,
                                                                                 PayerID = csipn.PayerID,
                                                                                 PaymentType = csipn.PaymentType,
                                                                                 NotifyVersion = csipn.NotifyVersion,
                                                                                 PayerPhone = csipn.PayerPhone,
                                                                                 Tax = csipn.Tax,
                                                                                 PayerBusinessName = csipn.PayerBusinessName
                                                                             }))
            {
                context.AddToArchivedCustomerSuccesfulIPNs(acsipn);
                context.SaveChanges();
            }
        }

        var professionalsuccessfulIPN = (from c in context.ProfessionalSuccesfulIPNs
                                         where c.ProjectID == ProjectID
                                         select c).ToList();
        if (professionalsuccessfulIPN.Count != 0)
        {
            foreach (var apsipn in professionalsuccessfulIPN.Select(csipn => new ArchivedProfessionalSuccesfulIPN
                                                                                 {
                                                                                     IPNID = csipn.IPNID,
                                                                                     GrossTotal = csipn.GrossTotal,
                                                                                     Invoice = csipn.Invoice,
                                                                                     PaymentStatus = csipn.PaymentStatus,
                                                                                     FirstName = csipn.FirstName,
                                                                                     LastName = csipn.LastName,
                                                                                     PaymentFee = csipn.PaymentFee,
                                                                                     BusinessEmail = csipn.BusinessEmail,
                                                                                     ReceiverEmail = csipn.ReceiverEmail,
                                                                                     ItemName = csipn.ItemName,
                                                                                     CurrencyCode = csipn.CurrencyCode,
                                                                                     TransactionId = csipn.TransactionId,
                                                                                     Custom = csipn.Custom,
                                                                                     SubscriberId = csipn.SubscriberId,
                                                                                     ProID = csipn.ProID,
                                                                                     ProjectID = csipn.ProjectID,
                                                                                     TxType = csipn.TxType,
                                                                                     PendingReason = csipn.PendingReason,
                                                                                     PaymentDate = csipn.PaymentDate,
                                                                                     Address = csipn.Address,
                                                                                     City = csipn.City,
                                                                                     State = csipn.State,
                                                                                     Zip = csipn.Zip,
                                                                                     Country = csipn.Country,
                                                                                     CountryCode = csipn.CountryCode,
                                                                                     AddressStatus = csipn.AddressStatus,
                                                                                     PayerStatus = csipn.PayerStatus,
                                                                                     PayerID = csipn.PayerID,
                                                                                     PaymentType = csipn.PaymentType,
                                                                                     NotifyVersion = csipn.NotifyVersion,
                                                                                     PayerPhone = csipn.PayerPhone,
                                                                                     Tax = csipn.Tax,
                                                                                     PayerBusinessName = csipn.PayerBusinessName
                                                                                 }))
            {
                context.AddToArchivedProfessionalSuccesfulIPNs(apsipn);
                context.SaveChanges();
            }
        }

        var customersuccessfulpdt = (from c in context.CustomerSuccessfulPDTs
                                     where c.ProjectID == ProjectID
                                     select c).ToList();
        if (customersuccessfulpdt.Count != 0)
        {
            foreach (var acspdt in customersuccessfulpdt.Select(cspdt => new ArchivedCustomerSuccessfulPDT
                                                                             {
                                                                                 PDTID = cspdt.PDTID,
                                                                                 GrossTotal = cspdt.GrossTotal,
                                                                                 Invoice = cspdt.Invoice,
                                                                                 PaymentStatus = cspdt.PaymentStatus,
                                                                                 FirstName = cspdt.FirstName,
                                                                                 LastName = cspdt.LastName,
                                                                                 PaymentFee = cspdt.PaymentFee,
                                                                                 BusinessEmail = cspdt.BusinessEmail,
                                                                                 TxToken = cspdt.TxToken,
                                                                                 ReceiverEmail = cspdt.ReceiverEmail,
                                                                                 ItemName = cspdt.ItemName,
                                                                                 CurrencyCode = cspdt.CurrencyCode,
                                                                                 TransactionId = cspdt.TransactionId,
                                                                                 Custom = cspdt.Custom,
                                                                                 subscriberId = cspdt.subscriberId,
                                                                                 CustomerID = cspdt.CustomerID,
                                                                                 ProjectID = cspdt.ProjectID
                                                                             }))
            {
                context.AddToArchivedCustomerSuccessfulPDTs(acspdt);
                context.SaveChanges();
            }

            var professionalsuccessfulpdt = (from c in context.ProfessionalSuccessfulPDTs
                                             where c.ProjectID == ProjectID
                                             select c).ToList();
            if (professionalsuccessfulpdt.Count != 0)
            {
                foreach (var apspdt in professionalsuccessfulpdt.Select(cspdt => new ArchivedProfessionalSuccessfulPDT
                                                                                     {

                                                                                         PDTID = cspdt.PDTID,
                                                                                         GrossTotal = cspdt.GrossTotal,
                                                                                         Invoice = cspdt.Invoice,
                                                                                         PaymentStatus = cspdt.PaymentStatus,
                                                                                         FirstName = cspdt.FirstName,
                                                                                         LastName = cspdt.LastName,
                                                                                         PaymentFee = cspdt.PaymentFee,
                                                                                         BusinessEmail = cspdt.BusinessEmail,
                                                                                         TxToken = cspdt.TxToken,
                                                                                         ReceiverEmail = cspdt.ReceiverEmail,
                                                                                         ItemName = cspdt.ItemName,
                                                                                         CurrencyCode = cspdt.CurrencyCode,
                                                                                         TransactionId = cspdt.TransactionId,
                                                                                         Custom = cspdt.Custom,
                                                                                         subscriberId = cspdt.subscriberId,
                                                                                         ProID = cspdt.ProID,
                                                                                         ProjectID = cspdt.ProjectID
                                                                                     }))
                {
                    context.AddToArchivedProfessionalSuccessfulPDTs(apspdt);
                    context.SaveChanges();
                }
            }
        }
    }
}