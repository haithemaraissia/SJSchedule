using System.Linq;
using SidejobModel;

/// <summary>
/// Summary description for PaymentProcessUpdate
/// </summary>
public static class PaymentProcessUpdate
{

    /// <summary>
    /// Common to both bidder and poster to invoke the routine
    /// </summary>
    /// <param name="projectid"></param>
    /// <param name="payerid"></param>
    /// <param name="payerole"></param>
    public static void PaymentUpdateProcedure(int projectid, int payerid, string payerole)
    {
        //Determine the role in ClosedProject
        using (var context = new SidejobEntities())
        {
            var closed = (from c in context.ClosedProjects
                          where c.ProjectID == projectid
                          select c).FirstOrDefault();
            if (closed != null)
            {
                if (closed.PosterID == payerid && closed.PosterRole == payerole)
                {
                    //poster
                    PosterSuccessPayment(projectid, 4, payerole, payerid);
                }
                if (closed.BidderID == payerid && closed.BidderRole == payerole)
                {
                    //bidder
                    BidderSuccessPayment(projectid, 2, payerole);
                }
            }
        }
    }

    /// <summary>
    /// Bidder Update for both scheduling procedure:
    /// TimeUp
    /// ResponseDelay
    /// </summary>
    /// <param name="projectid"></param>
    /// <param name="actionid"></param>
    /// <param name="payerole"> </param>
    public static void BidderSuccessPayment(int projectid, int actionid, string payerole)
    {
        BidderTimeUpUpdate(projectid, actionid);
        BidderResponseDelayUpdate(projectid);
        AddToBidderCompletedProjectSucessPayment(projectid, projectid, payerole);
    }

    public static void BidderTimeUpUpdate(int projectid, int actionid)
    {
        using (var context = new SidejobEntities())
        {
            var cp = (from c in context.ClosedProjects
                      where c.ProjectID == projectid
                      select c).FirstOrDefault();
            if (cp != null)
            {
                var timeupUpdate = new TimeUp(projectid, actionid, cp);
            }
        }
    }

    public static void BidderResponseDelayUpdate(int projectid)
    {
        using (var context = new SidejobEntities())
        {
            var rd = (from c in context.ResponseDelays
                      where c.ProjectID == projectid
                      select c).FirstOrDefault();
            if (rd != null)
            {
                rd.Status = 4;
                context.SaveChanges();
            }
        }
    }

    public static void AddToBidderCompletedProjectSucessPayment(int projectid, int payerid, string payerole)
    {
        var context = new SidejobEntities();
        var biddercompletedprojectsucesspayment = new CompletedProjectSucessPayment
                                                      {
                                                          PayerID = payerid,
                                                          PayerProjectRole = "BIDDER",
                                                          PayerRole = payerole,
                                                          ProjectID = projectid
                                                      };
        context.AddToCompletedProjectSucessPayment(biddercompletedprojectsucesspayment);
        context.SaveChanges();

        //check to see if the poster has already made the payment:
        var posterpayment = (from c in context.CompletedProjectSucessPayment
                             where c.ProjectID == projectid && c.PayerProjectRole == "POSTER"
                             select c).FirstOrDefault();
        if (posterpayment != null)
        {
            //poster
            PosterSuccessPayment(projectid, 4, posterpayment.PayerRole, posterpayment.PayerID);
        }
    }

    /// <summary>
    /// Poster Update for both scheduling procedure:
    /// TimeUp
    /// ResponseDelay
    /// </summary>
    /// <param name="projectid"></param>
    /// <param name="actionid"></param>
    /// <param name="payerole"> </param>
    /// <param name="payerid"> </param>
    public static void PosterSuccessPayment(int projectid, int actionid, string payerole, int payerid)
    {
        AddToPosterCompletedProjectSucessPayment(projectid, payerid, payerole);
    }

    public static void PosterTimeUpUpdate(int projectid, int actionid)
    {
        using (var context = new SidejobEntities())
        {
            var cp = (from c in context.ClosedProjects
                      where c.ProjectID == projectid
                      select c).FirstOrDefault();
            if (cp != null)
            {
                var timeupUpdate = new TimeUp(projectid, actionid, cp);
            }

        }
    }

    public static void PosterResponseDelayUpdate(int projectid)
    {
        using (var context = new SidejobEntities())
        {
            var rd = (from c in context.ResponseDelays
                      where c.ProjectID == projectid
                      select c).FirstOrDefault();
            if ( rd != null)
            {
                context.DeleteObject(rd);
                context.SaveChanges();
            }            
        }
    }

    public static void AddToPosterCompletedProjectSucessPayment(int projectid, int payerid, string payerole)
    {
        var context = new SidejobEntities();
        var postercompletedprojectsucesspayment = new CompletedProjectSucessPayment
        {
            PayerID = payerid,
            PayerProjectRole = "POSTER",
            PayerRole = payerole,
            ProjectID = projectid
        };
        context.AddToCompletedProjectSucessPayment(postercompletedprojectsucesspayment);
        context.SaveChanges();

        //check to see if the bidder hass already made the payment:
        var bidderpayment = (from c in context.CompletedProjectSucessPayment
                             where c.ProjectID == projectid && c.PayerProjectRole == "BIDDER"
                             select c).FirstOrDefault();
        if (bidderpayment != null)
        {
            //bidder
            PosterTimeUpUpdate(projectid, 4);
            PosterResponseDelayUpdate(projectid);
        }

    }
}



