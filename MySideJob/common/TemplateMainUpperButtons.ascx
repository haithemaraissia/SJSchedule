<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TemplateMainUpperButtons.ascx.vb"
    Inherits="common.Wip2RightCleanSideJob2008FromInetpubCleanSidejob2008CommonMainUpperButtons2" %>
<link rel="stylesheet" media="all" type="text/css" href="http://www.my-side-job.com/Schedule/MySideJob/_assets/css/menu_style.css" />
<link rel="stylesheet" href="http://www.my-side-job.com/Schedule/MySideJob/_assets/css/MainUpperButtonsTemplateStyleSheet.css"
    type="text/css" media="screen" />
<style type="text/css">
    #H2SideJob
    {
        text-align: center;
        position: relative;
        color: #fff;
        margin: 0 -12px 0px -12px;
        padding: 10px 0;
        text-shadow: 0 1px rgba(0,0,0,.8);
        background: #6C798C;
        background-image: -moz-linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,0));
        background-image: -webkit-linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,0));
        background-image: -o-linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,0));
        background-image: -ms-linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,0));
        background-image: linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,0));
        -moz-box-shadow: 0 2px 0 rgba(0,0,0,.3);
        -webkit-box-shadow: 0 2px 0 rgba(0,0,0,.3);
        box-shadow: 0 2px 0 rgba(0,0,0,.3);
        clear: both;
    }
    #H2SideJob:before, #H2SideJob:after
    {
        border-style: solid;
        border-color: transparent;
        bottom: -10px;
    }
    #H2SideJob:before
    {
        border-width: 0 10px 10px 0;
        border-right-color: #222;
        left: 0;
    }
    #H2SideJob:after
    {
        border-width: 0 0 10px 10px;
        border-left-color: #222;
        right: 0;
    }
    
    #nav-menu ul
    {
        list-style: none;
        padding: 0;
        margin: 0;
        font-family: Andy;
        font-size: large;
        height: 38px;
    }
    
    #nav-menu li
    {
        float: left;
        margin: 0;
        height: 37px;
    }
    
    
    .nav-menu a
    {
        background-image: url('http://www.my-side-job.com/Schedule/MySideJob/Images/MenuList/bg-bubplastic-h-red 2.gif');
        height: 2em;
        line-height: 2em;
        float: left;
        width: 9em;
        display: block;
        border: 0.1em solid #dcdce9;
        color: #FFFFFF;
        text-decoration: none;
        text-align: center;
    }
    
    
    .selected a
    {
        background-image: url('http://www.my-side-job.com/Schedule/MySideJob/Images/MenuList/bg-bubplastic-button2.gif');
        height: 2em;
        line-height: 2em;
        float: left;
        width: 9em;
        display: block;
        border: 0.1em solid #dcdce9;
        color: #0d2474;
        text-decoration: none;
        text-align: center;
    }
    
    .nav-menu a:hover
    {
        background-image: url('http://www.my-side-job.com/Schedule/MySideJob/Images/MenuList/bg-bubplastic-h-gray2.gif');
        height: 2em;
        line-height: 2em;
        float: left;
        width: 9em;
        display: block;
        border: 0.1em solid #dcdce9;
        color: #0d2474;
        text-decoration: none;
        text-align: center;
    }
    
    /* Hide from IE5-Mac \*/
    #nav-menu li a
    {
        float: none;
    }
    /* End hide */
    
    #nav-menu
    {
        width: 58.4em;
        height: 36px;
    }
    #Language li a
    {
    }
</style>
<div style="background-color: #800000; text-align: center">
    <div>
        <table align="center" width="100%">
            <tr>
                <td>
                    <div style="padding-left: 10%; padding-right: 10%">
                    <h2 id="H2HowDoesitWork" align="center">
                        <asp:Label ID="HowDoesitWorkLabel" runat="server" Text="Side Job" Font-Bold="True"
                            Font-Size="Large" Style="font-size: xx-large; font-family: Andy; color: #FFFFFF;"></asp:Label></h2>
                     </div>
                </td>
            </tr>
        </table>
    </div>
</div>
