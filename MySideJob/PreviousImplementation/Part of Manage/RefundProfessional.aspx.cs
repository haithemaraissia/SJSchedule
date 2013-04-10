using System;
using System.Linq;
using SidejobModel;

public partial class PartOfManageRefundProfessional : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CustomerRefundGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
        //You should have refunded through paypal

        //Now; 
        //Removed from current customerrefund and inserted into archivedcustomerrefund
        ArchiveRefund((int)ProfessionalRefundGridView.SelectedValue);

    }

    protected void ArchiveRefund(int pdtid)
    {
        using (var context = new SidejobEntities())
        {
            var currentrefund = (from c in context.RefundProfessionalSuccessfulPDTs
                                 where c.PDTID == pdtid
                                 select c).FirstOrDefault();
            if (currentrefund != null)
            {
                var archivedrefund = new ArchivedRefundProfessionalSuccessfulPDT
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
                    ProID = currentrefund.ProID,
                    ProjectID = currentrefund.ProjectID
                };
                context.AddToArchivedRefundProfessionalSuccessfulPDTs(archivedrefund);
                context.DeleteObject(currentrefund);
                context.SaveChanges();
                Response.Redirect(Context.Request.Url.ToString());
            }
        }
    }
}