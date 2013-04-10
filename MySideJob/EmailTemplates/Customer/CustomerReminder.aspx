<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReminder.aspx.cs"
    Inherits="EmailTemplates.Customer.EmailTemplatesCustomerCustomerReminder" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title runat="server" title="<%$ Resources:Resource, Reminder %>"></title>
    <style type="text/css">
        .DarkRed
        {
            color: #800000;
        }
    </style>
</head>
<body>
    <UpperNavigationButtons:NavigationButtons ID="MainUpperNavigationButtons" runat="server">
    </UpperNavigationButtons:NavigationButtons>
    <table id="TemplateGlobalTable" style="width: 100%">
        <tr>
            <td width="10%">
            </td>
            <td width="80%;" style="background-color: #FFFFFF;">
                <p>
                    <br />
                </p>
                <h2>
                    <asp:Label ID="ProjectNotification" runat="server" Text=""></asp:Label>
                </h2>
                <p>
                    <asp:Label ID="DearLabel" runat="server" Text="<%$ Resources:Resource, Dear%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="UsernameLabel" runat="server" Text=""></asp:Label></strong></span><asp:Label
                            ID="CommaLabel" runat="server" Text="<%$ Resources:Resource, Comma%>"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="ThisisTheLabel" runat="server" Text="<%$ Resources:Resource, ThisIsThe%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="ReminderLabel" runat="server" Text=""></asp:Label>
                    </strong></span>
                    <asp:Label ID="ReminderCompleteProjectLabel" runat="server" Text="<%$ Resources:Resource, ReminderCompleteProject%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="ProjectLabel" runat="server" Text=""></asp:Label></strong></span>
                    <asp:Label ID="ReminderFees" runat="server" Text=""></asp:Label>
                    <br />
                    <span class="DarkRed"><strong>
                        <asp:Label ID="BidderLabel" runat="server" Text=""></asp:Label></strong></span>
                    <a href="http://www.my-side-job.com">My-Side-Job</a>
                </p>
                <p>
                    <asp:Label ID="ActionViolationLabel" runat="server" Text="<%$ Resources:Resource, ActionViolation%>"></asp:Label><br />
                    <asp:Label ID="ObligatedToBlockLabel" runat="server" Text="<%$ Resources:Resource, ObligatedToBlock%>"></asp:Label><br />
                    <br />
                    <asp:Label ID="PleasePayFeesLabel" runat="server" Text="<%$ Resources:Resource, PleasePayFees%>"></asp:Label><br />
                    <asp:Label ID="ThankYouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou%>"></asp:Label>
                </p>
                <p>
                    <br />
                </p>
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
</body>
</html>
