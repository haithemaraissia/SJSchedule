<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutomatedMessage.aspx.cs"
    Inherits="EmailTemplates.EmailTemplatesAutomatedMessage" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../common/TemplateMainUpperButtons.ascx" %>
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
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
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="EmailTitle" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="EmailMessage" runat="server" Text=""></asp:Label>
                            <br />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <a href="http://www.my-side-job.com">My-Side-Job</a>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
</body>
</html>
