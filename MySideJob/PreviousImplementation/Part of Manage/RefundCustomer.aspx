<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundCustomer.aspx.cs" Inherits="RefundCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Current:<br />
        <asp:GridView ID="CustomerRefundGridView" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PDTID" 
            DataSourceID="SqlDataSource1" 
            onselectedindexchanged="CustomerRefundGridViewSelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" 
                    SortExpression="PDTID" />
                <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" 
                    SortExpression="GrossTotal" />
                <asp:BoundField DataField="Invoice" HeaderText="Invoice" 
                    SortExpression="Invoice" />
                <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" 
                    SortExpression="PaymentStatus" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                    SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" 
                    SortExpression="LastName" />
                <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" 
                    SortExpression="PaymentFee" />
                <asp:BoundField DataField="TxToken" HeaderText="TxToken" 
                    SortExpression="TxToken" />
                <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" 
                    SortExpression="ReceiverEmail" />
                <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                    SortExpression="CurrencyCode" />
                <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" 
                    SortExpression="TransactionId" />
                <asp:BoundField DataField="Custom" HeaderText="Custom" 
                    SortExpression="Custom" />
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                    SortExpression="CustomerID" />
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                    SortExpression="ProjectID" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="RefundButton" runat="server" Text="Refund" CommandName="select" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
            SelectCommand="SELECT * FROM [RefundCustomerSuccessfulPDT]">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
