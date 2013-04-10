<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GivenData.aspx.cs" Inherits="Test_ResponseDelay_GivenData" %>

<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="~/common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <UpperNavigationButtons:NavigationButtons ID="MainUpperNavigationButtons" runat="server">
        </UpperNavigationButtons:NavigationButtons>
        <table id="TemplateGlobalTable" style="width: 100%">
            <tr>
                <td width="10%">
                </td>
                <td width="80%; background-color: #FFFFFF;">
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ClosedProject
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="ClosedProjectGridView" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ProjectID" DataSourceID="ClosedProjectSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="DateFinished" HeaderText="DateFinished" SortExpression="DateFinished" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" SortExpression="PosterRole" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" SortExpression="BidderRole" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" SortExpression="HighestBid" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" SortExpression="CurrencyID" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ClosedProjectSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ClosedProject]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ResponseDelay
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="ResponseDelayGridView" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ProjectID" DataSourceID="ResponseDelaySqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="DateFinished" HeaderText="DateFinished" SortExpression="DateFinished" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" SortExpression="PosterRole" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" SortExpression="BidderRole" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" SortExpression="HighestBid" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" SortExpression="CurrencyID" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:BoundField DataField="ReminderLevel" HeaderText="ReminderLevel" SortExpression="ReminderLevel" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ResponseDelaySqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ResponseDelay]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Projects
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                    DataSourceID="ProjectsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" InsertVisible="False"
                                            ReadOnly="True" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" SortExpression="PosterRole" />
                                        <asp:BoundField DataField="PosterUserName" HeaderText="PosterUserName" SortExpression="PosterUserName" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" SortExpression="HighestBid" />
                                        <asp:BoundField DataField="HighestBidderID" HeaderText="HighestBidderID" SortExpression="HighestBidderID" />
                                        <asp:BoundField DataField="HighestBidUsername" HeaderText="HighestBidUsername" SortExpression="HighestBidUsername" />
                                        <asp:BoundField DataField="NumberofBids" HeaderText="NumberofBids" SortExpression="NumberofBids" />
                                        <asp:BoundField DataField="Posted" HeaderText="Posted" SortExpression="Posted" />
                                        <asp:BoundField DataField="StatusInt" HeaderText="StatusInt" SortExpression="StatusInt" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProjectsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [Projects]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ArchievedCancelledProject
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                    DataSourceID="ArchivedCancelledProjectSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="DateFinished" HeaderText="DateFinished" SortExpression="DateFinished" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" SortExpression="PosterRole" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" SortExpression="BidderRole" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" SortExpression="HighestBid" />
                                        <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ArchivedCancelledProjectSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ArchievedCancelledProject]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: lockedcustomer
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="LockedCustomerSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ID" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                        <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="LockedCustomerSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [LockedCustomer]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: lockedprofessional
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="LockedProfessionalSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ID" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                        <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="LockedProfessionalSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [LockedProfessional]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerSuccessfulPDTs
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataKeyNames="PDTID"
                                    DataSourceID="CustomerSuccesfullPDTSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="PDTID" HeaderText="PDTID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="PDTID" />
                                        <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                        <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" SortExpression="BusinessEmail" />
                                        <asp:BoundField DataField="TxToken" HeaderText="TxToken" SortExpression="TxToken" />
                                        <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" SortExpression="ReceiverEmail" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                        <asp:BoundField DataField="Custom" HeaderText="Custom" SortExpression="Custom" />
                                        <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" SortExpression="subscriberId" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerSuccesfullPDTSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [CustomerSuccessfulPDT]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: RefundCustomerSuccessfulPDTs
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" DataKeyNames="PDTID"
                                    DataSourceID="RefundCustomerSuccessfulPDTSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" SortExpression="PDTID" />
                                        <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                        <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" SortExpression="BusinessEmail" />
                                        <asp:BoundField DataField="TxToken" HeaderText="TxToken" SortExpression="TxToken" />
                                        <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" SortExpression="ReceiverEmail" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                        <asp:BoundField DataField="Custom" HeaderText="Custom" SortExpression="Custom" />
                                        <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" SortExpression="subscriberId" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="RefundCustomerSuccessfulPDTSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [RefundCustomerSuccessfulPDT]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalSuccessfulPDTs
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PDTID"
                                    DataSourceID="ProfessionalSuccessfulPDTSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="PDTID" HeaderText="PDTID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="PDTID" />
                                        <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                        <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" SortExpression="BusinessEmail" />
                                        <asp:BoundField DataField="TxToken" HeaderText="TxToken" SortExpression="TxToken" />
                                        <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" SortExpression="ReceiverEmail" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                        <asp:BoundField DataField="Custom" HeaderText="Custom" SortExpression="Custom" />
                                        <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" SortExpression="subscriberId" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalSuccessfulPDTSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ProfessionalSuccessfulPDT]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: RefundProfessionalSuccessfulPDTs
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataKeyNames="PDTID"
                                    DataSourceID="RefundProfessionalSuccessfulPDTSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="PDTID" HeaderText="PDTID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="PDTID" />
                                        <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                                        <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                        <asp:BoundField DataField="BusinessEmail" HeaderText="BusinessEmail" SortExpression="BusinessEmail" />
                                        <asp:BoundField DataField="TxToken" HeaderText="TxToken" SortExpression="TxToken" />
                                        <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" SortExpression="ReceiverEmail" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                        <asp:BoundField DataField="Custom" HeaderText="Custom" SortExpression="Custom" />
                                        <asp:BoundField DataField="subscriberId" HeaderText="subscriberId" SortExpression="subscriberId" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="RefundProfessionalSuccessfulPDTSqlDataSource" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" SelectCommand="SELECT * FROM [RefundProfessionalSuccessfulPDT]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Bids
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" DataKeyNames="BidID"
                                    DataSourceID="BidsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="BidID" HeaderText="BidID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="BidID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" SortExpression="PosterRole" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterUserName" HeaderText="PosterUserName" SortExpression="PosterUserName" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" SortExpression="BidderRole" />
                                        <asp:BoundField DataField="BidderUserName" HeaderText="BidderUserName" SortExpression="BidderUserName" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" SortExpression="BidderID" />
                                        <asp:BoundField DataField="AmountOffered" HeaderText="AmountOffered" SortExpression="AmountOffered" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" SortExpression="CurrencyID" />
                                        <asp:BoundField DataField="BidDate" HeaderText="BidDate" SortExpression="BidDate" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="BidsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [Bids]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                </td>
                <td width="10%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
