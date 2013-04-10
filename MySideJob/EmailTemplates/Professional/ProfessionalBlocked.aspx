<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfessionalBlocked.aspx.cs" Inherits="EmailTemplates.Professional.EmailTemplatesProfessionalProfessionalBlocked" %>

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
                        <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label></strong></span><asp:Label
                            ID="CommaLabel" runat="server" Text="<%$ Resources:Resource, Comma%>"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="ConfirmationEmail" runat="server" Text=""></asp:Label>
                    <asp:Label ID="AccountLockedMessageLabel" runat="server" Text="<%$ Resources:Resource, AccountLockedMessage%>"></asp:Label>  </p><p>
                    <asp:Label ID="WishToUnlockAccountLabel" runat="server" Text="<%$ Resources:Resource, WishToUnlockAccount%>"></asp:Label>&nbsp;<a
                        href="http://www.my-side-job.com">http://www.my-side-job.com</a>
                </p>

                <p>
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