<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArchievedRefundedCustomer.aspx.cs" Inherits="ArchievedRefundedCustomer" %>

<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<%@ Register TagPrefix="LowerNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainLowerButtons.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="Title1" runat="server" title="<%$ Resources:Resource, HomeTitle %>"></title>
    <link rel="stylesheet" href="../_assets/css/TemplateStyleSheet.css" type="text/css"
        media="screen" />
    <meta property="og:url" content="https://www.my-side-job.com" />
    <link rel="canonical" href="https://www.my-side-job.com" />
</head>
<body>
    <form id="form2" runat="server">
    <UpperNavigationButtons:NavigationButtons ID="MainUpperNavigationButtons" runat="server">
    </UpperNavigationButtons:NavigationButtons>

    <div id="wrapper" style="padding: 25px">
        <table align="center" id="TemplateGlobalTable">
            <tr>
                <td>
    <div>
    
        Archived<br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PDTID" 
            DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="PDTID" HeaderText="PDTID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="PDTID" />
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
                <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" 
                    SortExpression="ReceiverEmail" />
                <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                    SortExpression="CurrencyCode" />
                <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" 
                    SortExpression="TransactionId" />
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                    SortExpression="CustomerID" />
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                    SortExpression="ProjectID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
            SelectCommand="SELECT * FROM [ArchivedRefundCustomerSuccessfulPDT]">
        </asp:SqlDataSource>
    
    </div>
      </td>
            </tr>
        </table>
    </div>
    <div class="cleaner">
    </div>
    <LowerNavigationButtons:NavigationButtons ID="MainLowerNavigationButtons" runat="server" />
    </form>
</body>
</html>