using System;
using System.Linq;
using SidejobModel;

public partial class PartOfManageAutomatedEmailProblem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AutomatedEmailProblemGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
        //Issue is fixed, then delete it
        if (AutomatedEmailProblemGridView.SelectedDataKey == null) return;
        var selected = (int)AutomatedEmailProblemGridView.SelectedDataKey.Value;

        using (var context = new SidejobEntities())
        {
            var current = (from c in context.AutomationEmailProblems
                           where c.MessageID == selected
                           select c).FirstOrDefault();
            if (current != null)
            {
                context.DeleteObject(current);
                context.SaveChanges();
            }

        }
    }
}