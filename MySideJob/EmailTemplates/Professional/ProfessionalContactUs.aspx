<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfessionalContactUs.aspx.cs" Inherits="EmailTemplates.Professional.EmailTemplatesProfessionalProfessionalContactUs" %>

<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="Title1" runat="server" title="<%$ Resources:Resource, Notification %>">
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
                    <asp:Label ID="ProfessionalIDLabel" runat="server" Text="<%$ Resources:Resource, ProfessionalID %>"
                        Style="color: #800000"></asp:Label>
                    &nbsp;
                    <asp:Label ID="ProfessionalID" runat="server" Text=""></asp:Label>
                </h2>
                <p>
                    <asp:Label ID="Message" runat="server" Text=""></asp:Label>
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