<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfessionalNewOption.aspx.cs"
    Inherits="EmailTemplates.Professional.EmailTemplatesProfessionalProfessionalNewOption" %>

<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title runat="server" title="<%$ Resources:Resource, Notification %>"></title>
    <style type="text/css">
        .DarkRed
        {
            color: #800000;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
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
                        <asp:Label ID="OurRecordsIndicateLabel" runat="server" Text="<%$ Resources:Resource, OurRecordsIndicate%>"></asp:Label>
                        <asp:Label ID="BidderLabel" runat="server" Text="<%$ Resources:Resource, Bidder%>"></asp:Label>
                        <span class="DarkRed"><strong>
                            <asp:Label ID="Bidder" runat="server" Text=""></asp:Label></strong></span>
                        <asp:Label ID="HasSecondbidLabel" runat="server" Text="<%$ Resources:Resource, HasSecondbid%>"></asp:Label><asp:Label
                            ID="TwoOlumn" runat="server" Text="<%$ Resources:Resource, TwoComma%>"></asp:Label>
                        <span class="DarkRed"><strong>
                            <asp:Label ID="NewBidAmount" runat="server" Text="" />
                            <asp:Label ID="Currency" runat="server" Text=""></asp:Label></strong></span><br />
                        <br />
                        <asp:Label ID="WeOfferFollowingLabel" runat="server" Text="<%$ Resources:Resource, WeOfferFollowing%>"></asp:Label>
                        <p>
                            <asp:RadioButtonList ID="SelectionRadioButtonList" runat="server">
                                <asp:ListItem Text="<%$ Resources:Resource, NewAgreementProject%>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$ Resources:Resource, ExtendProjectDuration%>" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </p>
                        <p>
                            <asp:HyperLink ID="ConfirmYourChoiceButton" runat="server" Text="<%$ Resources:Resource, PleaseConfirmYourChoice%>"
                                NavigateUrl=""></asp:HyperLink>
                        </p>
                        <p>
                            <asp:Label ID="ThankYouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou%>"></asp:Label><br />
                        </p>
                    </p>
                </td>
                <td width="10%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
