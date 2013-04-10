<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfessionalNewOpportunity.aspx.cs"
    Inherits="EmailTemplates.Professional.InsideWebsite.ProfessionalNewOpportunity" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="Message">
            This will requires UI desing In WIP2
            Also add custom page so that login will redirect to this page ( only the login);
        </div>
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
            <asp:Label ID="YouaretheSecondBidder" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Label ID="Posterwouldlikeyoutoaccept" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Label ID="WouldYouLikeToConfirmAggreementLabel" runat="server" 
             Text="<%$ Resources:Resource, WouldLikeConfirmAggrement%>">
            </asp:Label>
        </p>
        <asp:Label ID="WeOfferFollowingLabel" runat="server" Text="<%$ Resources:Resource, WeOfferFollowing%>"></asp:Label>
        <p>
            <asp:RadioButtonList ID="SelectionRadioButtonList" runat="server">
                <asp:ListItem Text="<%$ Resources:Resource, NewAgreementProject%>" Value="1"></asp:ListItem>
                <asp:ListItem Text="<%$ Resources:Resource, RejecttheOffer%>" Value="2"></asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p>
            <asp:Button ID="ConfirmYourChoiceButton" runat="server" Text="<%$ Resources:Resource, PleaseConfirmYourChoice%>"
                OnClick="ConfirmYourChoiceButtonClick"></asp:Button>
        </p>
        <p>
            <asp:Label ID="ThankYouLabel" runat="server" Text="<%$ Resources:Resource, ThankYou%>"></asp:Label><br />
        </p>
        <p>
            <asp:Panel ID="ThankYouPanel" runat="server" Height="150px" Style="display: block;
                left: 91px; position: relative; top: 308px; z-index: 103; text-align: center;"
                Width="473px">
                <ajaxToolkit:ModalPopupExtender ID="ThankYouPanelModalPopupExtender" runat="server"
                    TargetControlID="ThankYouTargetedLabel" PopupControlID="ThankYouPanel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:RoundedCornersExtender ID="ThankYouRoundedCornersExtender" runat="server"
                    BorderColor="64, 0, 0" Color="64, 0, 0" Enabled="True" TargetControlID="ThankYouMessage">
                </ajaxToolkit:RoundedCornersExtender>
                <asp:Panel ID="ThankYouMessage" runat="server" BackColor="White">
                    <p>
                        <br />
                    </p>
                    <p>
                        <asp:Label ID="ThankYouTargetedLabel" runat="server" Style="position: relative" Width="153px"
                            Text="<%$ Resources:Resource, ThankyouForConfirmingYourSelection%>"></asp:Label>
                    </p>
                    <p>
                        <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:Resource, OK%>" OnClick="OkButtonClick" />
                    </p>
                </asp:Panel>
            </asp:Panel>
        </p>
    </div>
    </form>
</body>
</html>
