<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs"
    Inherits="EmailTemplates.AccountEmailTemplatesPasswordRecovery" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="Title1" runat="server" title="<%$ Resources:Resource, ForgotPassword %>">
    </title>
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
                    <asp:Label ID="YourPasswordHasBeenReset" runat="server" Text="<%$ Resources:Resource, YourPasswordHasBeenReset %>"></asp:Label></h2>
                <p>
                    <asp:Label ID="Thisemailconfirmsthatyourpasswordhasbeenchanged" runat="server" Text="<%$ Resources:Resource, Thisemailconfirmsthatyourpasswordhasbeenchanged %>"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="ToLogonTo" runat="server" Text="<%$ Resources:Resource, ToLogonTo %>"></asp:Label>
                    <a href="http://www.my-side-job.com">My-Side-Job</a><asp:Label ID="usethefollowingcredentials"
                        runat="server" Text="<%$ Resources:Resource, usethefollowingcredentials %>"></asp:Label>
                </p>
                <table>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="Username" runat="server" Text="<%$ Resources:Resource, Username1 %>"></asp:Label></b>
                        </td>
                        <td>
                            <asp:Label ID="UsernameLabel" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="Password" runat="server" Text="<%$ Resources:Resource, Password1 %>"></asp:Label></b>
                        </td>
                        <td>
                            <asp:Label ID="PasswordLabel" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Label ID="ThankyouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou %>"></asp:Label>
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
