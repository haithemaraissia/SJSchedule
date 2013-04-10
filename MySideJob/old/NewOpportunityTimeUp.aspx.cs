using System;
using System.Collections.Generic;
using System.Linq;
using SidejobModel;

public partial class NewOpportunityTimeUp : System.Web.UI.Page
{
    public int NewProID { get; set; }
    public int ProjectID { get; set; }

    private void SavedToArchivedProjectSecondChance(SidejobEntities context, IEnumerable<ProjectSecondChance> newopportunity)
    {
        //Saved to ArchivedProjectSecondChance
        foreach (ProjectSecondChance o in newopportunity)
        {
            var p1 = new ArchivedProjectSecondChance
                         {
                             AgreerID = o.AgreerID,
                             AgreerRole = o.AgreerRole,
                             AgreerProjectRole = o.AgreerProjectRole,
                             ProjectID = o.ProjectID,
                             DateTime = DateTime.Now.Date
                         };
            if (!string.IsNullOrEmpty(o.AgreerProjectRole = "New Bidder"))
            {
                NewProID = o.AgreerID;
            }
            context.AddToArchivedProjectSecondChances(p1);
            context.SaveChanges();
        }
    }

    private void Updateproject(SidejobEntities context)
    {
        //Delete From ClosedProject
        var closedproject = (from c in context.ClosedProjects
                             where c.ProjectID == ProjectID
                             select c).FirstOrDefault();
        if (closedproject != null)
        {
            context.DeleteObject(closedproject);
        }

        //Update Project Requirement End Date to Today
        var projectrequirement = (from c in context.ProjectsRequirementsMains
                                  where c.ProjectID == ProjectID
                                  select c).FirstOrDefault();
        if (projectrequirement != null)
        {
            projectrequirement.DateFinished = DateTime.Now.Date;
            projectrequirement.AmountOffered = (decimal)GetCurrentNewBid(ProjectID, NewProID).AmountOffered;
            context.SaveChanges();
        }

        //Update HighestBid and BidderID 
        var project = (from c in context.Projects
                       where c.ProjectID == ProjectID
                       select c).FirstOrDefault();
        if (project != null)
        {
            project.HighestBid = GetCurrentNewBid(ProjectID, NewProID).AmountOffered;
            project.HighestBidderID = NewProID;
            project.HighestBidUsername = GetProfessional().UserName;
            project.StatusInt = 0;
            context.SaveChanges();
        }
    }

    private void DeletePreviousBidderWin(SidejobEntities context, int projectId)
    {
        var previous = (from c in context.ClosedProjects
                        where c.ProjectID == projectId
                        select c.PosterID).FirstOrDefault();

        if (previous != 0)
        {
            var bids = (from c in context.Bids
                        where c.ProjectID == projectId && c.BidderID == previous
                        orderby c.AmountOffered
                        select c).FirstOrDefault();

            if (bids != null)
            {
                int previousbidid = bids.BidID;
                var previouswinnerbid = from c in context.ProfessionalWinBids
                                        where c.ProID == previous
                                        && c.BidID == previousbidid
                                        select c;

                context.DeleteObject(bids);
                context.DeleteObject(previouswinnerbid);
            }
        }
    }

    public  void AddNewBidderWin(SidejobEntities context, int projectId)
    {
        var newBid = GetCurrentNewBid(projectId, NewProID);
        var newwin = new ProfessionalWinBid
                         {
                             BidID = newBid.BidID,
                             ProID = NewProID,
                             NumberofBids = GetNumberofBids(context)
                         };
        context.AddToProfessionalWinBids(newwin);
        context.SaveChanges();
    }

    public Bid GetCurrentNewBid(int projectid, int professionalid)
    {
        using (var context = new SidejobEntities())
        {
            var newbid = (from c in context.Bids
                          where c.ProjectID == projectid && c.BidderID == professionalid
                          orderby c.AmountOffered
                          select c).FirstOrDefault();
            return newbid;

        }
    }

    public Professional GetProfessional()
    {
        using (var context = new SidejobEntities())
        {
            return (Professional)(from c in context.ProfessionalGeneral2
                                  where c.ProID == NewProID
                                  select c);
        }
    }

    private int GetNumberofBids(SidejobEntities context)
    {
        var c = (from p in context.ProfessionalWinBids
                 where p.ProID == NewProID
                 select p).Count();
        return c + 1;
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var context = new SidejobEntities())
        {
            var projects = (from pr in context.ClosedProjects
                            select pr).ToList();

            foreach (var p in projects)
            {
                var p1 = p;
                var newopportunity = (from cp in context.ProjectSecondChances
                                      where p1 != null && cp.ProjectID == p1.ProjectID
                                      select cp);
                if (newopportunity.Count() == 2)
                {
                    ProjectID = p.ProjectID;

                    //Save to ArchivedProjectSecondChance
                    SavedToArchivedProjectSecondChance(context, newopportunity);

                    //Delete Previous BidderWin and Bids
                    DeletePreviousBidderWin(context, ProjectID);

                    //Add Current BidderWin
                    AddNewBidderWin(context, ProjectID);

                    //update project 
                    Updateproject(context);

                }
            }
        }
    }
}