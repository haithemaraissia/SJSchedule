using System;
using System.Linq;
using System.Web.UI;
using SidejobModel;

public partial class RefundCustomer : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CustomerRefundGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
        //You should have refunded through paypal
        
        //Now; 
        //Removed from current customerrefund and inserted into archivedcustomerrefund
        ArchiveRefund((int) CustomerRefundGridView.SelectedValue);

    }

    protected void ArchiveRefund(int pdtid)
    {
        using (var context = new SidejobEntities())
        {
            var currentrefund = (from c in context.RefundCustomerSuccessfulPDTs
                                 where c.PDTID == pdtid
                                 select c).FirstOrDefault();
            if(currentrefund != null)
            {
                var archivedrefund = new ArchivedRefundCustomerSuccessfulPDT
                                         {
                                             PDTID = currentrefund.PDTID,
                                             GrossTotal = currentrefund.GrossTotal,
                                             Invoice = currentrefund.Invoice,
                                             PaymentStatus = currentrefund.PaymentStatus,
                                             FirstName = currentrefund.FirstName,
                                             LastName = currentrefund.LastName,
                                             PaymentFee = currentrefund.PaymentFee,
                                             BusinessEmail = currentrefund.BusinessEmail,
                                             TxToken = currentrefund.TxToken,
                                             ReceiverEmail = currentrefund.ReceiverEmail,
                                             ItemName = currentrefund.ItemName,
                                             CurrencyCode = currentrefund.CurrencyCode,
                                             TransactionId = currentrefund.TransactionId,
                                             Custom = currentrefund.Custom,
                                             subscriberId = currentrefund.subscriberId,
                                             CustomerID = currentrefund.CustomerID,
                                             ProjectID = currentrefund.ProjectID
                                         };
                context.AddToArchivedRefundCustomerSuccessfulPDTs(archivedrefund);
                context.DeleteObject(currentrefund);
                context.SaveChanges();
                Response.Redirect(Context.Request.Url.ToString());
            }
        }
    }

}