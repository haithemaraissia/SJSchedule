<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GivenData.aspx.cs" Inherits="Test_Refund_GivenData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table id="TemplateGlobalTable" style="width: 100%">
        <tr>
            <td width="10%">
            </td>
            <td width="80%; background-color: #FFFFFF;">
                <table width="100%" style="background-color: #FFFFFF">
                    <tr>
                        <td>
                            Table: ArchivedRefundCustomerSuccessfulPDTs
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="ArchivedRefundCustomerSuccessfulPDTsGridView" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="PDTID" 
                                DataSourceID="ArchivedRefundCustomerSuccessfulPDTsSqlDataSource">
                                <Columns>
                                    <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" 
                                        SortExpression="PDTID" InsertVisible="False" />
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
                                    <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" 
                                        SortExpression="BusinessEmail" />
                                    <asp:BoundField DataField="TxToken" HeaderText="TxToken" 
                                        SortExpression="TxToken" />
                                    <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" 
                                        SortExpression="ReceiverEmail" />
                                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" 
                                        SortExpression="ItemName" />
                                    <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                                        SortExpression="CurrencyCode" />
                                    <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" 
                                        SortExpression="TransactionId" />
                                    <asp:BoundField DataField="Custom" HeaderText="Custom" 
                                        SortExpression="Custom" />
                                    <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" 
                                        SortExpression="subscriberId" />
                                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                        SortExpression="CustomerID" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                        SortExpression="ProjectID" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="ArchivedRefundCustomerSuccessfulPDTsSqlDataSource" 
                                runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                SelectCommand="SELECT * FROM [ArchivedRefundCustomerSuccessfulPDT]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <table width="100%" style="background-color: #FFFFFF">
                    <tr>
                        <td>
                            Table: ArchivedRefundProfessionalSuccessfulPDTs
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="ArchivedRefundProfessionalSuccessfulPDTsGridView" 
                                runat="server" AutoGenerateColumns="False"
                                DataKeyNames="PDTID" 
                                DataSourceID="ArchivedRefundProfessionalSuccessfulPDTsSqlDataSource">
                                <Columns>
                                    <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" 
                                        SortExpression="PDTID" InsertVisible="False" />
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
                                    <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" 
                                        SortExpression="BusinessEmail" />
                                    <asp:BoundField DataField="TxToken" HeaderText="TxToken" 
                                        SortExpression="TxToken" />
                                    <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" 
                                        SortExpression="ReceiverEmail" />
                                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" 
                                        SortExpression="ItemName" />
                                    <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                                        SortExpression="CurrencyCode" />
                                    <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" 
                                        SortExpression="TransactionId" />
                                    <asp:BoundField DataField="Custom" HeaderText="Custom" 
                                        SortExpression="Custom" />
                                    <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" 
                                        SortExpression="subscriberId" />
                                    <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                        SortExpression="ProjectID" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="ArchivedRefundProfessionalSuccessfulPDTsSqlDataSource" 
                                runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                SelectCommand="SELECT * FROM [ArchivedRefundProfessionalSuccessfulPDT]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
    <td width="10%">
    </td>
    </tr> </table>
    </form>
</body>
</html>
