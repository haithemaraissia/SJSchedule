
Partial Class WIP2_RightCleanSideJOB2008FromInetpub_CleanSIDEJOB2008_common_MainUpperButtons2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        copyrightlabel.InnerText = Resources.Resource.Copyright.ToString + " © " + Date.Now.Year.ToString
    End Sub
End Class
