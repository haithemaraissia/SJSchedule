<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Design.aspx.cs" Inherits="EmailTemplates_Design" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="~/common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
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
                            <asp:Label ID="EmailTitle" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="EmailMessage" runat="server" Text=""></asp:Label>
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
