<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutomatedEmailProblem.aspx.cs" Inherits="PartOfManageAutomatedEmailProblem" %>

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

    
        Email and Message that didn't go through<br />
        <asp:GridView ID="AutomatedEmailProblemGridView" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="MessageID" DataSourceID="SqlDataSource1" 
            onselectedindexchanged="AutomatedEmailProblemGridViewSelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="MessageID" HeaderText="MessageID" ReadOnly="True" 
                    SortExpression="MessageID" />
                <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                    SortExpression="EmailAddress" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Message" HeaderText="Message" 
                    SortExpression="Message" />
                <asp:ButtonField Text="Delete" CommandName="select" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" 
            SelectCommand="SELECT * FROM [AutomationEmailProblem]"></asp:SqlDataSource>
        <br />

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
