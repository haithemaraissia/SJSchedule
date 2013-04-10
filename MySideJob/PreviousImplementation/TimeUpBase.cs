using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SidejobModel;

/// <summary>
/// Summary description for TimeUpBase
/// </summary>

public class TimeUpBase 
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

    public TimeUpBase()
    { 
        UpdateTimeLeft();
        GetFinishedPorjects();
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
        }
    }

    public void GetFinishedPorjects()
    {
        //Check to see which projects time is finished and projects are open
        using (var context = new SidejobEntities())
        {
            var projects = (from pr in context.ProjectRequirements
                            join p in context.Projects on
                                pr.ProjectID equals p.ProjectID
                            where (pr.EndDate >= DateTime.UtcNow) && (p.StatusInt == 0)
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
        context.DeleteObject(bidder);
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
        context.DeleteObject(bidder);
        context.SaveChanges();
    }

    public void AddToClosedProject(int projectId)
    {
        int bidderid = 0;
        using (var context = new SidejobEntities())
        {
            var cp = (from c in context.ClosedProjects
                      where c.ProjectID == projectId
                      select c).FirstOrDefault();

            if (cp != null)
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

                var newclosedproject = from p in context.Projects
                                       join b in context.Bids
                                           on p.ProjectID equals b.ProjectID
                                       where p.ProjectID == projectId &&
                                             b.BidderID == bidderid
                                       select new ClosedProject
                                       {
                                           ProjectID = projectId,
                                           DateFinished = DateTime.Now.Date,
                                           PosterID = p.PosterID,
                                           PosterRole = p.PosterRole,
                                           BidderID = p.HighestBidderID,
                                           BidderRole = b.BidderRole,
                                           HighestBid = p.HighestBid,
                                           CurrencyID = b.CurrencyID,
                                           Status = 3
                                       };
                context.AddToClosedProjects((ClosedProject)newclosedproject);
                context.SaveChanges();
                GetBidderPosterProjectProperties(projectId, 0, (ClosedProject)newclosedproject);
            }
        }
    }

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
                              where c.CustomerID == (int)cp.BidderID
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
                              where c.ProID == (int)cp.BidderID
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
        ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////

        ProcessProjectAction(projectId, action, cp);
    }


    public void ProcessProjectAction(int projectId, int action, ClosedProject cp)
    {

        /////////////////////The time of the Project is finished/////////////////
        if (action == 0)
        {
            var context = new SidejobEntities();
            var numberofbids = (from c in context.Bids
                                where c.ProjectID == projectId
                                select c).Count();
            if (numberofbids == 0)
            {
                Postermessage = Resources.Resource.ProjectExpiration + " " + projectId
                    + Resources.Resource.zerobids + Resources.Resource.ProjectExtention;
                //  No One had any bids on the project
                Phasetitle = Resources.Resource.ProjectExpirationPhase;
                //  No One had any bids on the project
            }
            else
            {
                //There is Bids on the project
                Postermessage = Resources.Resource.PosterProjectCongratulation + " " + projectId;
                Postermessage += Resources.Resource.PosterProjectBidReach;

                if (cp.CurrencyID != null)
                    Postermessage += "<b>" + cp.HighestBid +
                    ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
                Postermessage += Resources.Resource.TeamContactBidder;

                Biddermessage = Resources.Resource.BidderCongratulation;
                if (cp.CurrencyID != null)
                    Biddermessage += "<b>" + cp.HighestBid + ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
                Biddermessage += Resources.Resource.ForProject + " " + projectId;
                Biddermessage += Resources.Resource.BidderRequestPayment;

                Phasetitle = Resources.Resource.BidWonPhase;

                switch (action)
                {
                    case 0:
                        //  ---------------------------------Action 0:--------------------------------------
                        //------------------------The time of the Project is finished---------------------

                        if (cp.PosterRole == "CUS")
                        {
                            CustomerPayment(cp);
                        }
                        if (cp.PosterRole == "PRO")
                        {
                            ProfessionalPayment(cp);
                        }
                        break;

                    case 2:
                        // ---------------------------------Action 2:--------------------------------------
                        //----------------------------Bidder Pay Fees-------------------------------------
                        BidderPayFees(cp);
                        break;
                    case 4:
                        // ---------------------------------Action 4:--------------------------------------
                        //----------------------------Poster Pay Fees-------------------------------------
                        PosterPayFees(projectId, cp);
                        GenerateContract(cp);
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
        /////////////////////The time of the Project is finished/////////////////
    }



    public void BidderPayFees(ClosedProject cp)
    {



        //        ----------------------------Bidder Pay Fees-------------------------------------

        var context = new SidejobEntities();
        cp.Status = 4;
        context.SaveChanges();

        Postermessage = Resources.Resource.PosterBidCongratulation;

        if (cp.CurrencyID != null)
            Postermessage += "<b>" + cp.HighestBid + ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Postermessage += Resources.Resource.PosterRequestPayment;

        Biddermessage = Resources.Resource.BidderPaymentConfirmation;

        Phasetitle = Resources.Resource.BidConfirmationPhase;

        //---------------------------------Action 2:--------------------------------------

    }

    public void PosterPayFees(int projectId, ClosedProject cp)
    {



        //        ---------------------------------Action 4:--------------------------------------
        //----------------------------Poster Pay Fees-------------------------------------
        var context = new SidejobEntities();
        cp.Status = 4;
        context.SaveChanges();

        Postermessage = Resources.Resource.PosterProjectCongratulation;
        Postermessage += Resources.Resource.ForProject + " " + projectId;
        Postermessage += Resources.Resource.WithTheBidOf;

        if (cp.CurrencyID != null)
            Postermessage += "<b>" + cp.HighestBid + ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
        Postermessage += Resources.Resource.PosterPrintContract;

        Biddermessage = Resources.Resource.BidderProjectBidReach;
        Biddermessage += Resources.Resource.ForProject + " " + projectId;
        Biddermessage += Resources.Resource.WithTheBidOf;
        Biddermessage += Resources.Resource.BidderCongratulation;

        if (cp.CurrencyID != null)
            Biddermessage += "<b>" + cp.HighestBid + ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID) + "</b>";
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
    }

    public void InsertEvents(int applicantId, int applicationlcid, string applicationType)
    {
        var context = new SidejobEntities();
        int nextEventID = GetNextEventID();
        context.InsertEvent(nextEventID, DateTime.Now.Date, applicationlcid);
        var generalEvent = new Event
        {
            EventID = GetNextEventID(),
            ApplicantType = applicationType,
            ApplicantID = applicantId,
            EventDescription = Phasetitle,
            DateEvent = DateTime.Now.Date,
            LCID = ProjectLCID
        };


        context.AddToEvents(generalEvent);
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
            Date = DateTime.Now.Date,
            Title = eventdescription,
            Description = message,
            Checked = 1
        };
        context.AddToCustomerMessageInboxes(customerMessageInbox);
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
            Date = DateTime.Now.Date,
            Title = eventdescription,
            Description = message,
            Checked = 1
        };
        context.AddToProfessionalMessageInboxes(professionalMessageInbox);
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
            DateMessage = DateTime.Now.Date,
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
            InsertEvents((int)cp.BidderID, BidderLCID, "C");
            InsertMessage((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage, BidderLCID, "C");
            InsertCustomerMessageInbox((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage);
            InsertCustomerEvent((int)cp.BidderID);
        }
    }

    public void ProcessProfessionalBidder(ClosedProject cp)
    {
        if (cp.BidderID != null)
        {
            InsertEvents((int)cp.BidderID, BidderLCID, "P");
            InsertMessage((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage, BidderLCID, "P");
            InsertProfessionalMessageInbox((int)cp.BidderID, BidderUsername, Phasetitle, Biddermessage);
            InsertProfessionalEvent((int)cp.BidderID);
        }
    }

    public void ProcessCustomerPoster(ClosedProject cp)
    {
        if (cp.PosterID != 0)
        {
            InsertEvents(cp.PosterID, BidderLCID, "C");
            InsertMessage(cp.PosterID, BidderUsername, Phasetitle, Biddermessage, BidderLCID, "C");
            InsertCustomerMessageInbox(cp.PosterID, BidderUsername, Phasetitle, Biddermessage);
            InsertCustomerEvent(cp.PosterID);
        }
    }

    public void ProcessProfessionalPoster(ClosedProject cp)
    {
        if (cp.PosterID != 0)
        {
            InsertEvents(cp.PosterID, PosterLCID, "P");
            InsertMessage(cp.PosterID, PosterUsername, Phasetitle, Postermessage, PosterLCID, "P");
            InsertProfessionalMessageInbox(cp.PosterID, PosterUsername, Phasetitle, Postermessage);
            InsertProfessionalEvent(cp.PosterID);
        }
    }

    public void CustomerPayment(ClosedProject cp)
    {
        if (cp.HighestBid != null)
        {
            var projectPaymentDue = Math.Round((double)(GetPercentage((double)cp.HighestBid) * cp.HighestBid), 2);
            var phasemessage = "<b><font color='red'>" + Resources.Resource.Phase2 + ":</font></b><br>'" +
                               Resources.Resource.Project + " = "
                               + cp.HighestBid + "<br/>" + Resources.Resource.Fees + " = " + projectPaymentDue;

            var context = new SidejobEntities();
            if (cp.BidderID != null)
            {
                if (cp.CurrencyID != null)
                {
                    var customerpaymentdue = new CustomerPaymentDue
                    {
                        CustomerID = (int)cp.BidderID,
                        ProjectID = cp.ProjectID,
                        ProjectAmount = (double)cp.HighestBid,
                        Date = DateTime.Now.Date,
                        CurrencyCode = ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID),
                        PaymentDue = projectPaymentDue,
                        PhaseStatus = phasemessage
                    };
                    context.AddToCustomerPaymentDues(customerpaymentdue);
                }
                context.SaveChanges();
            }
        }
    }

    public void ProfessionalPayment(ClosedProject cp)
    {
        if (cp.Status == 3)
        {
            ProcessProfessionalPayment(Resources.Resource.Phase1, cp);
        }

        //IF @ProjectStatus = 4
        //--Another Professional was the Poster--
        //--The current Professional is the Bidder

        if (cp.Status == 4)
        {
            ProcessProfessionalPayment(Resources.Resource.Phase2, cp);
        }
    }

    public void ProcessProfessionalPayment(string phase, ClosedProject cp)
    {
        if (cp.HighestBid != null)
        {
            var projectPaymentDue = Math.Round((double)(GetPercentage((double)cp.HighestBid) * cp.HighestBid), 2);
            var phasemessage = "<b><font color='red'>" + phase + ":</font></b><br>'" +
                               Resources.Resource.Project + " = "
                               + cp.HighestBid + "<br/>" + Resources.Resource.Fees + " = " + projectPaymentDue;

            var context = new SidejobEntities();
            if (cp.BidderID != null)
            {
                if (cp.CurrencyID != null)
                {
                    var professionalpaymentdue = new ProfessionalPaymentDue
                    {
                        ProID = (int)cp.BidderID,
                        ProjectID = cp.ProjectID,
                        ProjectAmount = (double)cp.HighestBid,
                        Date = DateTime.Now.Date,
                        CurrencyCode = ScheduleMySide.Helpers.Utility.GetCurrencyCode((int)cp.CurrencyID),
                        PaymentDue = projectPaymentDue,
                        PhaseStatus = phasemessage
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
        //   ProjectRequirement projectrequirement;
        //Project Requirement:

        using (var context = new SidejobEntities())
        {
            //projectrequirement = (from c in context.ProjectRequirements
            //                         where c.ProjectID == cp.PosterID
            //                         select c).FirstOrDefault();


            CustomerGeneral customer;
            ProfessionalGeneral professional;
            if (cp.BidderRole == "CUS")
            {
                customer = (from c in context.CustomerGenerals
                            where c.CustomerID == cp.BidderID
                            select c).FirstOrDefault();
                professional = (from c in context.ProfessionalGenerals
                                where c.ProID == cp.PosterID
                                select c).FirstOrDefault();
                CustomerContract(customer, professional, cp);
            }
            ProfessionalGeneral professionalbidder;
            if (cp.BidderRole == "PRO")
            {
                professionalbidder = (from c in context.ProfessionalGenerals
                                      where c.ProID == cp.BidderID
                                      select c).FirstOrDefault();
                professional = (from c in context.ProfessionalGenerals
                                where c.ProID == cp.PosterID
                                select c).FirstOrDefault();
                ProfessionalContract(professional, professionalbidder, cp);
            }


            CustomerGeneral customerposter;
            if (cp.PosterRole == "CUS")
            {
                customer = (from c in context.CustomerGenerals
                            where c.CustomerID == cp.BidderID
                            select c).FirstOrDefault();
                customerposter = (from c in context.CustomerGenerals
                                  where c.CustomerID == cp.PosterID
                                  select c).FirstOrDefault();
                CustomerContract(customer, customerposter, cp);
            }
            if (cp.PosterRole == "PRO")
            {
                customer = (from c in context.CustomerGenerals
                            where c.CustomerID == cp.BidderID
                            select c).FirstOrDefault();
                professional = (from c in context.ProfessionalGenerals
                                where c.ProID == cp.PosterID
                                select c).FirstOrDefault();
                ProfessionalContract(customer, professional, cp);
            }


            //INSERT INTO CONTRACT  
            if ((cp.BidderRole != null && cp.BidderRole == "CUS") && (cp.PosterRole != null && cp.PosterRole == "PRO"))
            {
                customer = (from c in context.CustomerGenerals
                            where c.CustomerID == cp.BidderID
                            select c).FirstOrDefault();
                professional = (from c in context.ProfessionalGenerals
                                where c.ProID == cp.PosterID
                                select c).FirstOrDefault();
                Contract(customer, professional, cp);
            }
            if ((cp.BidderRole != null && cp.BidderRole == "CUS") && (cp.PosterRole != null && cp.PosterRole == "CUS"))
            {
                customer = (from c in context.CustomerGenerals
                            where c.CustomerID == cp.BidderID
                            select c).FirstOrDefault();
                customerposter = (from c in context.CustomerGenerals
                                  where c.CustomerID == cp.PosterID
                                  select c).FirstOrDefault();
                Contract(customer, customerposter, cp);
            }
            if ((cp.BidderRole != null && cp.BidderRole == "PRO") && (cp.PosterRole != null && cp.PosterRole == "CUS"))
            {
                professionalbidder = (from c in context.ProfessionalGenerals
                                      where c.ProID == cp.BidderID
                                      select c).FirstOrDefault();
                customerposter = (from c in context.CustomerGenerals
                                  where c.CustomerID == cp.PosterID
                                  select c).FirstOrDefault();
                Contract(professionalbidder, customerposter, cp);
            }
            if ((cp.BidderRole != null && cp.BidderRole == "PRO") && (cp.PosterRole != null && cp.PosterRole == "PRO"))
            {
                professionalbidder = (from c in context.ProfessionalGenerals
                                      where c.ProID == cp.BidderID
                                      select c).FirstOrDefault();
                professional = (from c in context.ProfessionalGenerals
                                where c.ProID == cp.PosterID
                                select c).FirstOrDefault();
                Contract(professionalbidder, professional, cp);
            }
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
            BidderID = customer.CustomerID,
            BidderFirstName = customer.FirstName,
            BidderLastName = customer.LastName,
            BidderUsername = customer.UserName,
            ContractDate = DateTime.Now,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            CustomerID = customer.CustomerID,
            HighestBid = (double)cp.HighestBid,
            PosterID = professional.ProID,
            PosterUsername = professional.UserName,
            PosterFirstName = professional.FirstName,
            PosterLastName = professional.LastName
        };
        context.AddToCustomerContracts(customercontract);
    }

    public static void CustomerContract(CustomerGeneral customer, CustomerGeneral customerposter, ClosedProject cp)
    {
        if (customer == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var customercontract = new CustomerContract
        {
            ContractID = GetNextContractID(),
            BidderID = customer.CustomerID,
            BidderFirstName = customer.FirstName,
            BidderLastName = customer.LastName,
            BidderUsername = customer.UserName,
            ContractDate = DateTime.Now,
            ProjectID = cp.ProjectID,
            CurrencyID = (int)cp.CurrencyID,
            CustomerID = customer.CustomerID,
            HighestBid = (double)cp.HighestBid,
            PosterID = customerposter.CustomerID,
            PosterUsername = customerposter.UserName,
            PosterFirstName = customerposter.FirstName,
            PosterLastName = customerposter.LastName
        };
        context.AddToCustomerContracts(customercontract);
    }

    public static void ProfessionalContract(CustomerGeneral customer, ProfessionalGeneral professional, ClosedProject cp)
    {
        if (customer == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var professionalcontract = new ProfessionalContract
        {
            BidderID = customer.CustomerID,
            BidderFirstName = customer.FirstName,
            BidderLastName = customer.LastName,
            BidderUsername = customer.UserName,
            ContractDate = DateTime.Now,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            HighestBid = (double)cp.HighestBid,
            PosterID = professional.ProID,
            PosterUsername = professional.UserName,
            PosterFirstName = professional.FirstName,
            PosterLastName = professional.LastName
        };
        context.AddToProfessionalContracts(professionalcontract);
    }

    public static void ProfessionalContract(ProfessionalGeneral professionalposter, ProfessionalGeneral professionalbidder, ClosedProject cp)
    {
        if (professionalbidder == null) return;
        if (cp.HighestBid == null) return;
        var context = new SidejobEntities();
        if (cp.CurrencyID == null) return;
        var professionalcontract = new ProfessionalContract
        {
            BidderID = professionalbidder.ProID,
            BidderFirstName = professionalbidder.FirstName,
            BidderLastName = professionalbidder.LastName,
            BidderUsername = professionalbidder.UserName,
            ContractDate = DateTime.Now,
            ProjectID = cp.ProjectID,
            ContractID = GetNextContractID(),
            CurrencyID = (int)cp.CurrencyID,
            HighestBid = (double)cp.HighestBid,
            PosterID = professionalposter.ProID,
            PosterUsername = professionalposter.UserName,
            PosterFirstName = professionalposter.FirstName,
            PosterLastName = professionalposter.LastName
        };
        context.AddToProfessionalContracts(professionalcontract);
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
                ContractDate = DateTime.Now.Date,
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
                ContractDate = DateTime.Now.Date,
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
        }

    }

    public static void Contract(ProfessionalGeneral professionalbidder, ProfessionalGeneral professional, ClosedProject cp)
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
                ContractDate = DateTime.Now.Date,
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
                ContractDate = DateTime.Now.Date,
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
        }

    }
}
