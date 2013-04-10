using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SidejobModel;

public partial class Test_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(DateTime.UtcNow.ToShortDateString());
        Response.Write("<br/>");
        using (var context = new SidejobEntities())
        {
            var projects = (from pr in context.ProjectRequirements
                            join p in context.Projects on
                                pr.ProjectID equals p.ProjectID
                            where (pr.EndDate <= DateTime.UtcNow) && (p.StatusInt == 0)
                            select p).ToList();
            Response.Write(projects.Count);
            Response.Write("Above is the count<br/>");
            Response.Write("<br/>");
            Response.Write("<br/>");
        }


        //var projects = (from pr in context.ProjectRequirements
        //                join p in context.Projects on
        //                    pr.ProjectID equals p.ProjectID
        //                where (pr.EndDate >= DateTime.UtcNow) && (p.StatusInt == 0)
        //                select p).ToList();


        using (var context = new SidejobEntities())
        {
            var projects = (from pr in context.ProjectRequirements
                            join p in context.Projects on
                                pr.ProjectID equals p.ProjectID
                            where (pr.EndDate <= DateTime.UtcNow) && (p.StatusInt == 0)
                            select pr).ToList();

            if (projects.Count != 0)
            {
                foreach (var p in projects)
                {
                    Response.Write(p.ProjectID);
                    Response.Write("<br/>");
                    Response.Write(p.EndDate);
                    Response.Write("<br/>");
                    Response.Write(p.EndDate <= DateTime.UtcNow);
                    Response.Write("<br/>");
                }
            }
           // Response.Write(projects.Count);

           /// Response.Write(projects.EndDate >= DateTime.UtcNow);
          // Response.Write(projects.StatusInt);
        }
    }
}