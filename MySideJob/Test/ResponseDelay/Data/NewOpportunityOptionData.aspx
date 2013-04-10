<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewOpportunityOptionData.aspx.cs"
    Inherits="Test_ResponseDelay_NewOpportunityData" %>

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
                                Table: ResponseDelay
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                    DataSourceID="ResponseDelaySqlDataSource">
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
                                Table: ProjectSecondChances
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="ProjectSecondChancesSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="AgreerID" HeaderText="AgreerID" SortExpression="AgreerID" />
                                        <asp:BoundField DataField="AgreerRole" HeaderText="AgreerRole" SortExpression="AgreerRole" />
                                        <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" />
                                        <asp:BoundField DataField="AgreerProjectRole" HeaderText="AgreerProjectRole" SortExpression="AgreerProjectRole" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProjectSecondChancesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ProjectSecondChance]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: ArchivedProjectSecondChance
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="ArchivedProjectSecondChancesSqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ID" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="AgreerID" HeaderText="AgreerID" SortExpression="AgreerID" />
                                        <asp:BoundField DataField="AgreerRole" HeaderText="AgreerRole" SortExpression="AgreerRole" />
                                        <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" />
                                        <asp:BoundField DataField="AgreerProjectRole" HeaderText="AgreerProjectRole" SortExpression="AgreerProjectRole" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ArchivedProjectSecondChancesSqlDataSource1" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" SelectCommand="SELECT * FROM [ArchivedProjectSecondChance]">
                                </asp:SqlDataSource>
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
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataKeyNames="BidID"
                                    DataSourceID="ProfessionalWinBidsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                        <asp:BoundField DataField="BidID" HeaderText="BidID" ReadOnly="True" SortExpression="BidID" />
                                        <asp:BoundField DataField="NumberofBids" HeaderText="NumberofBids" SortExpression="NumberofBids" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalWinBidsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ProfessionalWinBid]"></asp:SqlDataSource>
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
