<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutomatedEmailData.aspx.cs" Inherits="Test_TimeUp_AutomatedEmailData" %>
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
                                Table: AutomatedMessage
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="ClosedProjectGridView" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="MessageID" 
                                    DataSourceID="AutomatedMessageSqlDataSource">
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
                    <br/>
                    <hr/>
                    
                    
                    
  
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                                        <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: AutomationEmailProblem
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="MessageID" DataSourceID="AutomatedEmailProblemSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="MessageID" HeaderText="MessageID" ReadOnly="True" 
                                            SortExpression="MessageID" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                                            SortExpression="EmailAddress" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="Message" HeaderText="Message" 
                                            SortExpression="Message" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="AutomatedEmailProblemSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [AutomationEmailProblem]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br/>
                    <hr/>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                                        <table width="100%" style="background-color: #FFFFFF">
                        <tr>
                            <td>
                                Table: Bids
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="BidID" DataSourceID="BidsSqlDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="BidID" HeaderText="BidID" InsertVisible="False" 
                                            ReadOnly="True" SortExpression="BidID" />
                                        <asp:BoundField DataField="PosterRole" HeaderText="PosterRole" 
                                            SortExpression="PosterRole" />
                                        <asp:BoundField DataField="PosterID" HeaderText="PosterID" 
                                            SortExpression="PosterID" />
                                        <asp:BoundField DataField="PosterUserName" HeaderText="PosterUserName" 
                                            SortExpression="PosterUserName" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                                            SortExpression="ProjectID" />
                                        <asp:BoundField DataField="BidderRole" HeaderText="BidderRole" 
                                            SortExpression="BidderRole" />
                                        <asp:BoundField DataField="BidderUserName" HeaderText="BidderUserName" 
                                            SortExpression="BidderUserName" />
                                        <asp:BoundField DataField="BidderID" HeaderText="BidderID" 
                                            SortExpression="BidderID" />
                                        <asp:BoundField DataField="AmountOffered" HeaderText="AmountOffered" 
                                            SortExpression="AmountOffered" />
                                        <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" 
                                            SortExpression="CurrencyID" />
                                        <asp:BoundField DataField="BidDate" HeaderText="BidDate" 
                                            SortExpression="BidDate" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="BidsSqlDataSource" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
                                    SelectCommand="SELECT * FROM [Bids]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <br/>
                    <hr/>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    

                    
                    

                </td>
                <td width="10%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
