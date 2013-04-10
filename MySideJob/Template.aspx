<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Template.aspx.vb" Inherits="NotAuthenticated_Template"  Culture="auto:en-us" UICulture="auto:en-us" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="common/TemplateMainUpperButtons.ascx" %>
<%@ Register TagPrefix="LowerNavigationButtons" TagName="NavigationButtons" Src="common/TemplateMainLowerButtons.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="Title1" runat="server" title="<%$ Resources:Resource, HomeTitle %>"></title>
    <link rel="stylesheet" href="_assets/css/TemplateStyleSheet.css" type="text/css"
        media="screen" />
    <meta property="og:url" content="https://www.my-side-job.com" />
    <link rel="canonical" href="https://www.my-side-job.com" />
</head>
<body>
    <form id="form1" runat="server">
    <UpperNavigationButtons:NavigationButtons ID="MainUpperNavigationButtons" runat="server">
    </UpperNavigationButtons:NavigationButtons>

    <div id="wrapper" style="padding: 25px">
        <table align="center" id="TemplateGlobalTable">
            <tr>
                <td>
                    &nbsp;
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
