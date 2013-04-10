<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerProfessionalData.aspx.cs"
    Inherits="Test_ResponseDelay_CustomerProfessionalData" %>

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
                                Table: Customer
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomerID"
                                    DataSourceID="CustomerSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" InsertVisible="False"
                                            ReadOnly="True" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [Customer]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Customer General
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="ClosedProjectGridView" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="CustomerID" DataSourceID="CustomerGeneralsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                        <asp:BoundField DataField="CountryID" HeaderText="CountryID" SortExpression="CountryID" />
                                        <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" SortExpression="CountryCode" />
                                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                                        <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" SortExpression="RegionCode" />
                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                        <asp:BoundField DataField="Zipcode" HeaderText="Zipcode" SortExpression="Zipcode" />
                                        <asp:BoundField DataField="HomePhoneNumber" HeaderText="HomePhoneNumber" SortExpression="HomePhoneNumber" />
                                        <asp:BoundField DataField="MobilePhoneNumber" HeaderText="MobilePhoneNumber" SortExpression="MobilePhoneNumber" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="CustomerGeneralsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [CustomerGeneral2]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Professional
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProID"
                                    DataSourceID="ProfessionalSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="ProID" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
                                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [Professional]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Professional General
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ProID"
                                    DataSourceID="ProfessionalGeneral2SqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="ProID" HeaderText="ProID" ReadOnly="True" SortExpression="ProID" />
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                        <asp:BoundField DataField="CountryID" HeaderText="CountryID" SortExpression="CountryID" />
                                        <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" SortExpression="CountryCode" />
                                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                                        <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" SortExpression="RegionCode" />
                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                        <asp:BoundField DataField="Zipcode" HeaderText="Zipcode" SortExpression="Zipcode" />
                                        <asp:BoundField DataField="HomePhoneNumber" HeaderText="HomePhoneNumber" SortExpression="HomePhoneNumber" />
                                        <asp:BoundField DataField="MobilePhoneNumber" HeaderText="MobilePhoneNumber" SortExpression="MobilePhoneNumber" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="ProfessionalGeneral2SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                    SelectCommand="SELECT * FROM [ProfessionalGeneral2]"></asp:SqlDataSource>
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
