using System;
using System.Web.UI;
using System.Linq;
using SidejobModel;

namespace AddToSuccessfulPayment
{
    public partial class UpdateFromProfessionalSuccesfulPayement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Upon Payment Success;
            //PDT or IPN
            //  CustomerUpdatePaymentForTimeUpandResponseDelayRountine();
           ProfessionalUpdatePaymentForTimeUpandResponseDelayRountine();
        //    TestPosterBidderMessage();
        }
        private void CustomerUpdatePaymentForTimeUpandResponseDelayRountine()
        {
            const int projectid = 102;
            const int payerid = 82;
            const string payerole = "CUS";
            PaymentProcessUpdate.PaymentUpdateProcedure(projectid, payerid, payerole);
        }

        private void ProfessionalUpdatePaymentForTimeUpandResponseDelayRountine()
        {
            const int projectid = 105;
            const int payerid = 55;
            const string payerole = "PRO";
            PaymentProcessUpdate.PaymentUpdateProcedure(projectid, payerid, payerole);
        }


        private void TestPosterBidderMessage()
        {
            using (var context = new SidejobEntities())
            {
                var cp = (from c in context.ClosedProjects
                          where c.ProjectID == 105 
                          select c).FirstOrDefault();

                var t = new TimeUp();
                if (cp != null) t.TestPosterPayFees(105,cp);
            }


        }


    }
}