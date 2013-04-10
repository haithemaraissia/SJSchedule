<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GivenData.aspx.cs" Inherits="Test_TimeUp_GivenData" %>

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
                                <asp:GridView ID="GridView15" runat="server" AutoGenerateColumns="False"
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
                                Table: Projects
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView18" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
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
                                Table: ProjectRequirements
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                    DataSourceID="ProjectRequirementSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                        <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID" />
                                        <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" SortExpression="CategoryName" />
                                        <asp:BoundField DataField="JobID" HeaderText="JobID" SortExpression="JobID" />
                                        <asp:BoundField DataField="JobTitle" HeaderText="JobTitle" SortExpression="JobTitle" />
                                        <asp:BoundField DataField="ExperienceID" HeaderText="ExperienceID" SortExpression="ExperienceID" />
                                        <asp:BoundField DataField="CrewNumberID" HeaderText="CrewNumberID" SortExpression="CrewNumberID" />
                                        <asp:BoundField DataField="LicensedID" HeaderText="LicensedID" SortExpression="LicensedID" />
                                        <asp:BoundField DataField="InsuredID" HeaderText="InsuredID" SortExpression="InsuredID" />
                                        <asp:BoundField DataField="RelocationID" HeaderText="RelocationID" SortExpression="RelocationID" />
                                        <asp:BoundField DataField="ProjectTitle" HeaderText="ProjectTitle" SortExpression="ProjectTitle" />
                                        <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                                        <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                                        <asp:BoundField DataField="AmountOffered" HeaderText="AmountOffered" SortExpression="AmountOffered" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" SortExpression="CurrencyID" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:BoundField DataField="SpecialNotes" HeaderText="SpecialNotes" SortExpression="SpecialNotes" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                        <asp:BoundField DataField="CountryID" HeaderText="CountryID" SortExpression="CountryID" />
                                        <asp:BoundField DataField="CountryName" HeaderText="CountryName" SortExpression="CountryName" />
                                        <asp:BoundField DataField="RegionID" HeaderText="RegionID" SortExpression="RegionID" />
                                        <asp:BoundField DataField="RegionName" HeaderText="RegionName" SortExpression="RegionName" />
                                        <asp:BoundField DataField="CityID" HeaderText="CityID" SortExpression="CityID" />
                                        <asp:BoundField DataField="CityName" HeaderText="CityName" SortExpression="CityName" />
                                        <asp:BoundField DataField="Zipcode" HeaderText="Zipcode" SortExpression="Zipcode" />
                                        <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" />
                                        <asp:BoundField DataField="TimeLeft" HeaderText="TimeLeft" SortExpression="TimeLeft" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProjectRequirementSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ProjectRequirements]"></asp:SqlDataSource>
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
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="BidID"
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
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerWinBids
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="BidID" DataSourceID="CustomerWinBidsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                            SortExpression="CustomerID" />
                                        <asp:BoundField DataField="BidID" HeaderText="BidID" ReadOnly="True" 
                                            SortExpression="BidID" />
                                        <asp:BoundField DataField="NumberofBids" HeaderText="NumberofBids" 
                                            SortExpression="NumberofBids" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerWinBidsSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [CustomerWinBid]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalWinBids
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="BidID" DataSourceID="ProfessionalWinBidsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="BidID" HeaderText="BidID" ReadOnly="True" 
                                            SortExpression="BidID" />
                                        <asp:BoundField DataField="NumberofBids" HeaderText="NumberofBids" 
                                            SortExpression="NumberofBids" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalWinBidsSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [ProfessionalWinBid]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerEvent
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="EventID" DataSourceID="CustomerEventSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="EventID" HeaderText="EventID" InsertVisible="False" 
                                            ReadOnly="True" SortExpression="EventID" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                            SortExpression="CustomerID" />
                                        <asp:BoundField DataField="NumberofEvents" HeaderText="NumberofEvents" 
                                            SortExpression="NumberofEvents" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerEventSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [CustomerEvent]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalEvents
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="EventID" DataSourceID="ProfessionalEventSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="EventID" HeaderText="EventID" ReadOnly="True" 
                                            SortExpression="EventID" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="NumberofEvents" HeaderText="NumberofEvents" 
                                            SortExpression="NumberofEvents" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalEventSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [ProfessionalEvent]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Events
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="EventID" DataSourceID="EventSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="EventID" HeaderText="EventID" InsertVisible="False" 
                                            ReadOnly="True" SortExpression="EventID" />
                                        <asp:BoundField DataField="ApplicantType" HeaderText="ApplicantType" 
                                            SortExpression="ApplicantType" />
                                        <asp:BoundField DataField="ApplicantID" HeaderText="ApplicantID" 
                                            SortExpression="ApplicantID" />
                                        <asp:BoundField DataField="EventDescription" HeaderText="EventDescription" 
                                            SortExpression="EventDescription" />
                                        <asp:BoundField DataField="DateEvent" HeaderText="DateEvent" 
                                            SortExpression="DateEvent" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="EventSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [Events]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerMessageInbox
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="MessageID" DataSourceID="CustomerMessageInboxSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="MessageID" HeaderText="MessageID" ReadOnly="True" 
                                            SortExpression="MessageID" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                            SortExpression="CustomerID" />
                                        <asp:BoundField DataField="SenderName" HeaderText="SenderName" 
                                            SortExpression="SenderName" />
                                        <asp:BoundField DataField="ReceiverName" HeaderText="ReceiverName" 
                                            SortExpression="ReceiverName" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" 
                                            SortExpression="Description" />
                                        <asp:BoundField DataField="Checked" HeaderText="Checked" 
                                            SortExpression="Checked" />
                                        <asp:BoundField DataField="Response" HeaderText="Response" 
                                            SortExpression="Response" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerMessageInboxSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [CustomerMessageInbox]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalMessageInbox
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="MessageID" DataSourceID="ProfessionalMessageInboxSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="MessageID" HeaderText="MessageID" ReadOnly="True" 
                                            SortExpression="MessageID" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="SenderName" HeaderText="SenderName" 
                                            SortExpression="SenderName" />
                                        <asp:BoundField DataField="ReceiverName" HeaderText="ReceiverName" 
                                            SortExpression="ReceiverName" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" 
                                            SortExpression="Description" />
                                        <asp:BoundField DataField="Checked" HeaderText="Checked" 
                                            SortExpression="Checked" />
                                        <asp:BoundField DataField="Response" HeaderText="Response" 
                                            SortExpression="Response" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalMessageInboxSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [ProfessionalMessageInbox]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Message
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView16" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="MessageID" DataSourceID="MessageSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="MessageID" HeaderText="MessageID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="MessageID" />
                                        <asp:BoundField DataField="SenderType" HeaderText="SenderType" 
                                            SortExpression="SenderType" />
                                        <asp:BoundField DataField="SenderID" HeaderText="SenderID" 
                                            SortExpression="SenderID" />
                                        <asp:BoundField DataField="SenderName" HeaderText="SenderName" 
                                            SortExpression="SenderName" />
                                        <asp:BoundField DataField="ReceiverType" HeaderText="ReceiverType" 
                                            SortExpression="ReceiverType" />
                                        <asp:BoundField DataField="ReceiverID" HeaderText="ReceiverID" 
                                            SortExpression="ReceiverID" />
                                        <asp:BoundField DataField="ReceiverName" HeaderText="ReceiverName" 
                                            SortExpression="ReceiverName" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" 
                                            SortExpression="Description" />
                                        <asp:BoundField DataField="DateMessage" HeaderText="DateMessage" 
                                            SortExpression="DateMessage" />
                                        <asp:BoundField DataField="NumberofSenderInobx" 
                                            HeaderText="NumberofSenderInobx" SortExpression="NumberofSenderInobx" />
                                        <asp:BoundField DataField="NumberofSenderOutbox" 
                                            HeaderText="NumberofSenderOutbox" SortExpression="NumberofSenderOutbox" />
                                        <asp:BoundField DataField="NumberofSenderSaved" 
                                            HeaderText="NumberofSenderSaved" SortExpression="NumberofSenderSaved" />
                                        <asp:BoundField DataField="NumberofReceiverInbox" 
                                            HeaderText="NumberofReceiverInbox" SortExpression="NumberofReceiverInbox" />
                                        <asp:BoundField DataField="NumberofReceiverOutbox" 
                                            HeaderText="NumberofReceiverOutbox" SortExpression="NumberofReceiverOutbox" />
                                        <asp:BoundField DataField="NmberofReceiverSaved" 
                                            HeaderText="NmberofReceiverSaved" SortExpression="NmberofReceiverSaved" />
                                        <asp:BoundField DataField="NumberofSenderTotal" 
                                            HeaderText="NumberofSenderTotal" SortExpression="NumberofSenderTotal" />
                                        <asp:BoundField DataField="NumberofReceiverTotal" 
                                            HeaderText="NumberofReceiverTotal" SortExpression="NumberofReceiverTotal" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="MessageSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [Messages]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Contracts
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="ContractID" DataSourceID="ContractSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ContractID" HeaderText="ContractID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="ContractID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" 
                                            SortExpression="ContractDate" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" 
                                            SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" 
                                            SortExpression="BidderRole" />
                                        <asp:BoundField DataField="BidderUsername" HeaderText="BidderUsername" 
                                            SortExpression="BidderUsername" />
                                        <asp:BoundField DataField="BidderFirstName" HeaderText="BidderFirstName" 
                                            SortExpression="BidderFirstName" />
                                        <asp:BoundField DataField="BidderLastName" HeaderText="BidderLastName" 
                                            SortExpression="BidderLastName" />
                                        <asp:BoundField DataField="BidderAddress" HeaderText="BidderAddress" 
                                            SortExpression="BidderAddress" />
                                        <asp:BoundField DataField="BidderCountryID" HeaderText="BidderCountryID" 
                                            SortExpression="BidderCountryID" />
                                        <asp:BoundField DataField="BidderCountryName" HeaderText="BidderCountryName" 
                                            SortExpression="BidderCountryName" />
                                        <asp:BoundField DataField="BidderRegionID" HeaderText="BidderRegionID" 
                                            SortExpression="BidderRegionID" />
                                        <asp:BoundField DataField="BidderRegionName" HeaderText="BidderRegionName" 
                                            SortExpression="BidderRegionName" />
                                        <asp:BoundField DataField="BidderCityID" HeaderText="BidderCityID" 
                                            SortExpression="BidderCityID" />
                                        <asp:BoundField DataField="BidderCityName" HeaderText="BidderCityName" 
                                            SortExpression="BidderCityName" />
                                        <asp:BoundField DataField="BidderZipcode" HeaderText="BidderZipcode" 
                                            SortExpression="BidderZipcode" />
                                        <asp:BoundField DataField="BidderHomePhoneNumber" 
                                            HeaderText="BidderHomePhoneNumber" SortExpression="BidderHomePhoneNumber" />
                                        <asp:BoundField DataField="BidderMobilePhoneNumber" 
                                            HeaderText="BidderMobilePhoneNumber" SortExpression="BidderMobilePhoneNumber" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" 
                                            SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" 
                                            SortExpression="PosterRole" />
                                        <asp:BoundField DataField="PosterUsername" HeaderText="PosterUsername" 
                                            SortExpression="PosterUsername" />
                                        <asp:BoundField DataField="PosterFirstName" HeaderText="PosterFirstName" 
                                            SortExpression="PosterFirstName" />
                                        <asp:BoundField DataField="PosterLastName" HeaderText="PosterLastName" 
                                            SortExpression="PosterLastName" />
                                        <asp:BoundField DataField="PosterAddress" HeaderText="PosterAddress" 
                                            SortExpression="PosterAddress" />
                                        <asp:BoundField DataField="PosterCountryID" HeaderText="PosterCountryID" 
                                            SortExpression="PosterCountryID" />
                                        <asp:BoundField DataField="PosterCountryName" HeaderText="PosterCountryName" 
                                            SortExpression="PosterCountryName" />
                                        <asp:BoundField DataField="PosterRegionID" HeaderText="PosterRegionID" 
                                            SortExpression="PosterRegionID" />
                                        <asp:BoundField DataField="PosterRegionName" HeaderText="PosterRegionName" 
                                            SortExpression="PosterRegionName" />
                                        <asp:BoundField DataField="PosterCityID" HeaderText="PosterCityID" 
                                            SortExpression="PosterCityID" />
                                        <asp:BoundField DataField="PosterCityName" HeaderText="PosterCityName" 
                                            SortExpression="PosterCityName" />
                                        <asp:BoundField DataField="PosterZipcode" HeaderText="PosterZipcode" 
                                            SortExpression="PosterZipcode" />
                                        <asp:BoundField DataField="PosterHomePhoneNumber" 
                                            HeaderText="PosterHomePhoneNumber" SortExpression="PosterHomePhoneNumber" />
                                        <asp:BoundField DataField="PosterMobilePhoneNumber" 
                                            HeaderText="PosterMobilePhoneNumber" SortExpression="PosterMobilePhoneNumber" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                        <asp:BoundField DataField="ProjectCategoryID" HeaderText="ProjectCategoryID" 
                                            SortExpression="ProjectCategoryID" />
                                        <asp:BoundField DataField="ProjectCategoryName" 
                                            HeaderText="ProjectCategoryName" SortExpression="ProjectCategoryName" />
                                        <asp:BoundField DataField="ProjectJobID" HeaderText="ProjectJobID" 
                                            SortExpression="ProjectJobID" />
                                        <asp:BoundField DataField="ProjectJobTitle" HeaderText="ProjectJobTitle" 
                                            SortExpression="ProjectJobTitle" />
                                        <asp:BoundField DataField="ProjectExperienceID" 
                                            HeaderText="ProjectExperienceID" SortExpression="ProjectExperienceID" />
                                        <asp:BoundField DataField="ProjectCrewNumberID" 
                                            HeaderText="ProjectCrewNumberID" SortExpression="ProjectCrewNumberID" />
                                        <asp:BoundField DataField="ProjectLicensedID" HeaderText="ProjectLicensedID" 
                                            SortExpression="ProjectLicensedID" />
                                        <asp:BoundField DataField="ProjectInsuredID" HeaderText="ProjectInsuredID" 
                                            SortExpression="ProjectInsuredID" />
                                        <asp:BoundField DataField="ProjectRelocationID" 
                                            HeaderText="ProjectRelocationID" SortExpression="ProjectRelocationID" />
                                        <asp:BoundField DataField="ProjectTitle" HeaderText="ProjectTitle" 
                                            SortExpression="ProjectTitle" />
                                        <asp:BoundField DataField="ProjectStartDate" HeaderText="ProjectStartDate" 
                                            SortExpression="ProjectStartDate" />
                                        <asp:BoundField DataField="ProjectEndDate" HeaderText="ProjectEndDate" 
                                            SortExpression="ProjectEndDate" />
                                        <asp:BoundField DataField="ProjectDescription" HeaderText="ProjectDescription" 
                                            SortExpression="ProjectDescription" />
                                        <asp:BoundField DataField="ProjectSpecialNotes" 
                                            HeaderText="ProjectSpecialNotes" SortExpression="ProjectSpecialNotes" />
                                        <asp:BoundField DataField="ProjectAddress" HeaderText="ProjectAddress" 
                                            SortExpression="ProjectAddress" />
                                        <asp:BoundField DataField="ProjectCountryID" HeaderText="ProjectCountryID" 
                                            SortExpression="ProjectCountryID" />
                                        <asp:BoundField DataField="ProjectCountryName" HeaderText="ProjectCountryName" 
                                            SortExpression="ProjectCountryName" />
                                        <asp:BoundField DataField="ProjectRegionID" HeaderText="ProjectRegionID" 
                                            SortExpression="ProjectRegionID" />
                                        <asp:BoundField DataField="ProjectRegionName" HeaderText="ProjectRegionName" 
                                            SortExpression="ProjectRegionName" />
                                        <asp:BoundField DataField="ProjectCityID" HeaderText="ProjectCityID" 
                                            SortExpression="ProjectCityID" />
                                        <asp:BoundField DataField="ProjectCityName" HeaderText="ProjectCityName" 
                                            SortExpression="ProjectCityName" />
                                        <asp:BoundField DataField="ProjectZipcode" HeaderText="ProjectZipcode" 
                                            SortExpression="ProjectZipcode" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ContractSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [Contract]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerContract
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="ContractID" DataSourceID="CustomerContractSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ContractID" HeaderText="ContractID" ReadOnly="True" 
                                            SortExpression="ContractID" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                            SortExpression="CustomerID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" 
                                            SortExpression="ContractDate" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" 
                                            SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderUsername" HeaderText="BidderUsername" 
                                            SortExpression="BidderUsername" />
                                        <asp:BoundField DataField="BidderFirstName" HeaderText="BidderFirstName" 
                                            SortExpression="BidderFirstName" />
                                        <asp:BoundField DataField="BidderLastName" HeaderText="BidderLastName" 
                                            SortExpression="BidderLastName" />
                                        <asp:BoundField DataField="PosterUsername" HeaderText="PosterUsername" 
                                            SortExpression="PosterUsername" />
                                        <asp:BoundField DataField="PosterFirstName" HeaderText="PosterFirstName" 
                                            SortExpression="PosterFirstName" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" 
                                            SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterLastName" HeaderText="PosterLastName" 
                                            SortExpression="PosterLastName" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" 
                                            SortExpression="HighestBid" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" 
                                            SortExpression="CurrencyID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerContractSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [CustomerContract]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalContract
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView12" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="ContractID" DataSourceID="ProfessionalContractSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ContractID" HeaderText="ContractID" ReadOnly="True" 
                                            SortExpression="ContractID" />
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" 
                                            SortExpression="ContractDate" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" 
                                            SortExpression="BidderID" />
                                        <asp:BoundField DataField="BidderUsername" HeaderText="BidderUsername" 
                                            SortExpression="BidderUsername" />
                                        <asp:BoundField DataField="BidderFirstName" HeaderText="BidderFirstName" 
                                            SortExpression="BidderFirstName" />
                                        <asp:BoundField DataField="BidderLastName" HeaderText="BidderLastName" 
                                            SortExpression="BidderLastName" />
                                        <asp:BoundField DataField="PosterUsername" HeaderText="PosterUsername" 
                                            SortExpression="PosterUsername" />
                                        <asp:BoundField DataField="PosterFirstName" HeaderText="PosterFirstName" 
                                            SortExpression="PosterFirstName" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" 
                                            SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterLastName" HeaderText="PosterLastName" 
                                            SortExpression="PosterLastName" />
                                        <asp:BoundField DataField="HighestBid" HeaderText="HighestBid" 
                                            SortExpression="HighestBid" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" 
                                            SortExpression="CurrencyID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalContractSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [ProfessionalContract]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: CustomerPaymentDue
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView13" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="PaymentDueID" DataSourceID="CustomerPaymentDueSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                                            SortExpression="CustomerID" />
                                        <asp:BoundField DataField="PaymentDueID" HeaderText="PaymentDueID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="PaymentDueID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="ProjectAmount" HeaderText="ProjectAmount" 
                                            SortExpression="ProjectAmount" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="StatusID" HeaderText="StatusID" 
                                            SortExpression="StatusID" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                                            SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="PaymentDue" HeaderText="PaymentDue" 
                                            SortExpression="PaymentDue" />
                                        <asp:BoundField DataField="PhaseStatus" HeaderText="PhaseStatus" 
                                            SortExpression="PhaseStatus" />
                                        <asp:CheckBoxField DataField="PaymentProcess" HeaderText="PaymentProcess" 
                                            SortExpression="PaymentProcess" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" 
                                            SortExpression="Status" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerPaymentDueSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [CustomerPaymentDue]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ProfessionalPaymentDue
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView14" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="PaymentDueID" DataSourceID="ProfessionalPaymentDueSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="PaymentDueID" HeaderText="PaymentDueID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="PaymentDueID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="ProjectAmount" HeaderText="ProjectAmount" 
                                            SortExpression="ProjectAmount" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="StatusID" HeaderText="StatusID" 
                                            SortExpression="StatusID" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" 
                                            SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="PaymentDue" HeaderText="PaymentDue" 
                                            SortExpression="PaymentDue" />
                                        <asp:BoundField DataField="PhaseStatus" HeaderText="PhaseStatus" 
                                            SortExpression="PhaseStatus" />
                                        <asp:CheckBoxField DataField="PaymentProcess" HeaderText="PaymentProcess" 
                                            SortExpression="PaymentProcess" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" 
                                            SortExpression="Status" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalPaymentDueSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [ProfessionalPaymentDue]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: AutomatedMessage
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView17" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="MessageID" DataSourceID="AutomatedMessageSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="MessageID" HeaderText="MessageID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="MessageID" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                                            SortExpression="EmailAddress" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Message" HeaderText="Message" 
                                            SortExpression="Message" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="AutomatedMessageSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [AutomatedMessage]"></asp:SqlDataSource>
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
