<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerNotification.aspx.cs"
    Inherits="EmailTemplates.Customer.EmailTemplatesCustomerResponseDelay" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title runat="server" title="<%$ Resources:Resource, Notification %>"></title>
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
                    <asp:Label ID="ConfirmationRegardingProjectLabel" runat="server" Text="<%$ Resources:Resource, ConfirmationRegardingProject%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="ProjectLabel" runat="server" Text=""></asp:Label></strong></span>
                    <asp:Label ID="PeriodLabel" runat="server" Text="<%$ Resources:Resource, Period%>"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="WehaveSentLabel" runat="server" Text="<%$ Resources:Resource, WehaveSent%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="ReminderLabel" runat="server" Text=""></asp:Label>
                    </strong></span>
                    <asp:Label ID="RemindertoBidderLabel" runat="server" Text="<%$ Resources:Resource, RemindertoBidder%>"></asp:Label>
                    <span class="DarkRed"><strong>
                        <asp:Label ID="BidderLabel" runat="server" Text=""></asp:Label></strong></span>
                    <asp:Label ID="ForAgreementOnProjectLabel" runat="server" Text="<%$ Resources:Resource, ForAgreementOnProject%>"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="ContactUponReceivingConfirmationLabel" runat="server" Text="<%$ Resources:Resource, ContactUponReceivingConfirmation%>"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="ThankYouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou%>"></asp:Label><br />
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
