using System;
using System.Collections;
using System.Data.Objects;
using System.Linq;
using SidejobModel;

namespace Test
{
    public partial class TestGenericTest : System.Web.UI.Page
    {
        public int ProjectID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
                      using (var context = new SidejobEntities())
                      {
                          ProjectID = 20;
                          CleanUpProjectReferences(context);
                      }
        }

        /// <summary>
        /// Delete All References to this project
        /// </summary>
        public void CleanUpProjectReferences(SidejobEntities context)
        {
            //Delete project, bids, response , close, project requiremtn , second chance and any table that refer to this project.

            GetValue(context, (from c in context.ProjectSecondChances where c.ProjectID == ProjectID  select c).ToList());


            //List<object> projectsecondchance = List<object>(from c in context.ProjectSecondChances
            //                           where c.ProjectID == ProjectID
            //                           select c).ToList();
            //if (projectsecondchance.Count != 0)
            //{
            //    foreach (var psc in projectsecondchance)
            //    {
            //        context.DeleteObject(psc);
            //        context.SaveChanges();
            //    }
            //}

            //GetValue2(context,  ProjectSecondChance);
        }

        //private void GetValue(SidejobEntities context)
        //{
        //    var projectsecondchance = (from c in context.ProjectSecondChances
        //                               where c.ProjectID == ProjectID
        //                               select c).ToList();
        //    if (projectsecondchance.Count != 0)
        //    {
        //        foreach (var psc in projectsecondchance)
        //        {
        //            context.DeleteObject(psc);
        //            context.SaveChanges();
        //        }
        //    }
        //}


        private void GetValue(SidejobEntities context, ICollection somelist)
        {
            if (somelist.Count <= 0) return;
            foreach (var item in somelist)
            {
                context.DeleteObject(item);
                context.SaveChanges();
            }
        }


        //private void GetValue2(SidejobEntities context, IEnumerable<T> table)
        //{
           
        //    //if (somelist.Count <= 0) return;
        //    //foreach (var item in somelist)
        //    //{
        //    //    context.DeleteObject(item);
        //    //    context.SaveChanges();
        //    //}
        //}
    }
}