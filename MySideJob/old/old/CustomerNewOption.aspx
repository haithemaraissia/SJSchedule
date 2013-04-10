<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerNewOption.aspx.cs"
    Inherits="EmailTemplates.Customer.EmailTemplatesCustomerCustomerNewOption" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
        .DarkRed
        {
            color: #800000;
        }
    </style>
</head>
<body>
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
        <asp:Label ID="PosterLabel" runat="server" Text="<%$ Resources:Resource, Poster%>"></asp:Label>
        <span class="DarkRed"><strong>
            <asp:Label ID="PosterUserName" runat="server" Text=""></asp:Label></strong></span>
        <asp:Label ID="RefusedToAgreementLabel" runat="server" Text="<%$ Resources:Resource, RefusedToAgreement%>"></asp:Label><br />
        <asp:Label ID="BeenBlockLabel" runat="server" Text="<%$ Resources:Resource, BeenBlocked%>"></asp:Label><br />
        <asp:Label ID="OurRecordsIndicateLabel" runat="server" Text="<%$ Resources:Resource, OurRecordsIndicate%>"></asp:Label>
        <asp:Label ID="BidderLabel" runat="server" Text="<%$ Resources:Resource, Bidder%>"></asp:Label>
        <span class="DarkRed"><strong>
            <asp:Label ID="Bidder" runat="server" Text=""></asp:Label></strong></span>
        <asp:Label ID="WasSecondbidLabel" runat="server" Text="<%$ Resources:Resource, hasSecondbid%>"></asp:Label><br />
        <asp:Label ID="WeOfferFollowingLabel" runat="server" Text="<%$ Resources:Resource, WeOfferFollowing%>"></asp:Label>
        <asp:Label ID="TwoColoumnLabel" runat="server" Text="<%$ Resources:Resource, TwoComma%>"></asp:Label><br />
        <asp:Label ID="NewAgreementProject" runat="server" Text="<%$ Resources:Resource, NewAgreementProject%>"></asp:Label>
        <asp:Label ID="NewBid" runat="server" Text=""></asp:Label><asp:Label ID="PeriodLabel"
            runat="server" Text="<%$ Resources:Resource, Period%>"></asp:Label>
        <asp:Label ID="WeWillContactBidderLabel" runat="server" Text="<%$ Resources:Resource, WeWillContactBidder%>"></asp:Label><span
            class="DarkRed"><strong><asp:Label ID="Bidder2" runat="server" Text=""></asp:Label></strong></span><asp:Label
                ID="IfYouAgreeLabel" runat="server" Text="<%$ Resources:Resource, IfyouAgree%>"></asp:Label><br />
        <br />
        <asp:Label ID="ExtendProjectDuration" runat="server" Text="<%$ Resources:Resource, ExtendProjectDuration%>"></asp:Label><br />
          <asp:Label ID="ConfirmYourChoiceLabel" runat="server" Text="<%$ Resources:Resource, PleaseConfirmYourChoice%>"></asp:Label>
          <a href="http://www.my-side-job.com">My-Side-Job</a>
    </p>
    <p>
        <asp:Label ID="ThankYouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou%>"></asp:Label><br />
    </p>
</body>
</html>
