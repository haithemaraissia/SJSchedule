using System;
using System.Linq;
using SidejobModel;

public partial class AccountRefund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CustomerGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
        if (CustomerGridView.SelectedDataKey == null) return;
        var selected = (int) CustomerGridView.SelectedDataKey.Value;
        //Archive the refund
        //After you did it manually through Paypal
        using (var context = new SidejobEntities())
        {
            var current = (from c in context.RefundCustomerSuccessfulPDTs
                           where c.PDTID == selected
                           select c).FirstOrDefault();
            if (current != null)
            {
                var archive = new ArchivedRefundCustomerSuccessfulPDT
                                  {
                                      PDTID = current.PDTID,
                                      GrossTotal = current.GrossTotal,
                                      Invoice = current.Invoice,
                                      PaymentStatus = current.PaymentStatus,
                                      FirstName = current.FirstName,
                                      LastName = current.LastName,
                                      PaymentFee = current.PaymentFee,
                                      BusinessEmail = current.BusinessEmail,
                                      TxToken = current.TxToken,
                                      ReceiverEmail = current.ReceiverEmail,
                                      ItemName = current.ItemName,
                                      TransactionId = current.TransactionId,
                                      Custom = current.Custom,
                                      subscriberId = current.subscriberId,
                                      CustomerID = current.CustomerID,
                                      ProjectID = current.ProjectID
                                  };
                context.AddToArchivedRefundCustomerSuccessfulPDTs(archive);
                context.SaveChanges();
            }
        }
    }

    protected void ProfessionalGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ProfessionalGridView.SelectedDataKey == null) return;
        var selected = (int) ProfessionalGridView.SelectedDataKey.Value;
        //Archive the refund
        //After you did it manually through Paypal
        using (var context = new SidejobEntities())
        {
            var current = (from c in context.RefundProfessionalSuccessfulPDTs
                           where c.PDTID == selected
                           select c).FirstOrDefault();
            if (current != null)
            {
                var archive = new ArchivedRefundProfessionalSuccessfulPDT
                                  {
                                      PDTID = current.PDTID,
                                      GrossTotal = current.GrossTotal,
                                      Invoice = current.Invoice,
                                      PaymentStatus = current.PaymentStatus,
                                      FirstName = current.FirstName,
                                      LastName = current.LastName,
                                      PaymentFee = current.PaymentFee,
                                      BusinessEmail = current.BusinessEmail,
                                      TxToken = current.TxToken,
                                      ReceiverEmail = current.ReceiverEmail,
                                      ItemName = current.ItemName,
                                      TransactionId = current.TransactionId,
                                      Custom = current.Custom,
                                      subscriberId = current.subscriberId,
                                      ProID = current.ProID,
                                      ProjectID = current.ProjectID
                                  };
                context.AddToArchivedRefundProfessionalSuccessfulPDTs(archive);
                context.SaveChanges();
            }
        }

    }
}