using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Security;
using Geotargeting;
using System.Web.UI.WebControls;
using Roboblob.Utility;
using SidejobModel;

public static class ScheduleUtility
{
    public enum SearchParameterTypes
    {
        FieldParameter,
        TemplateParameter
    }

    public static int GetLCID(string lang)
    {
        switch (lang)
        {

            case "en-US":
                return 1;

                break;
            case "fr":
                return 2;

                break;
            case "es":
                return 3;

                break;
            case "zh-CN":
                return 4;

                break;
            case "ru":
                return 5;

                break;
            case "ar":
                return 6;

                break;
            case "ja":
                return 7;

                break;
            case "de":
                return 8;
                break;

            default:
                return 1;
                break;
        }

    }

    public static string GetLanguage(int langid)
    {
        switch (langid)
        {

            case 1:
                return "en-US";

                break;
            case 2:
                return "fr";

                break;
            case 3:
                return "es";

                break;
            case 4:
                return "zh-CN";

                break;
            case 5:
                return "ru";

                break;
            case 6:
                return "ar";

                break;
            case 7:
                return "ja";

                break;
            case 8:
                return "de";
                break;

            default:
                return "en-US";
                break;
        }

    }

    public static string GetCurrentLCID(string lang)
    {
        if (lang != null | !string.IsNullOrEmpty(lang))
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            return GetLCID(lang).ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            return "1";
        }

    }

    public static int GetTempAdID()
    {
        int adID;
        var context = new  AdDatabaseModel.AdDatabaseEntities();
        var max = context.TempAds.OrderByDescending(s => s.ID).FirstOrDefault();

        if (max == null)
        {
            adID = 0;
        }
        else
        {
            adID = max.ID + 1;
        }
        return adID;
    }

    public static int GetNextPendingID()
    {
        int ID;
        var context = new AdDatabaseModel.AdDatabaseEntities();
        var max = context.PendingAds.OrderByDescending(s => s.ID).FirstOrDefault();

        if (max == null)
        {
            ID = 0;
        }
        else
        {
            ID = max.ID + 1;
        }
        return ID;
    }

    public static int GetAudienceAdID()
    {
        int adID;
        var context = new AdDatabaseModel.AdDatabaseEntities();
        var max = context.AdAudiences.OrderByDescending(s => s.AdID).FirstOrDefault();

        if (max == null)
        {
            adID = 0;
        }
        else
        {
            adID = max.AdID + 1;
        }
        return adID;
    }

    public static string GetFlagPath(int countryID)
    {
        using (var context = new SidejobEntities())
        {
            var result = from c in context.CountriesUpdates
                         where c.CountryId == countryID
                         select c.Path;
            return result.FirstOrDefault();
        }
    }

    public static string GetCountryCode(int countryID)
    {
        using (var context = new SidejobEntities())
        {
            var result = from c in context.CountriesUpdates
                         where c.CountryId == countryID
                         select c.CurrencyCode;
            return result.FirstOrDefault();

        }
    }

        


    public static string GetProfession(ListBox specialityListBox1, ListBox specialityListBox2, ListBox specialityListBox3)
    {
        string selectedProfession = "";
        int i = 0;
        for (i = 0; i <= specialityListBox1.Items.Count - 1; i++)
        {
            if (specialityListBox1.Items[i].Selected == true)
            {
                selectedProfession += specialityListBox1.Items[i].Text + ",";
            }
        }

        for (i = 0; i <= specialityListBox2.Items.Count - 1; i++)
        {
            if (specialityListBox2.Items[i].Selected == true)
            {
                selectedProfession += specialityListBox2.Items[i].Text + ",";
            }
        }

        for (i = 0; i <= specialityListBox3.Items.Count - 1; i++)
        {
            if (specialityListBox3.Items[i].Selected == true)
            {
                selectedProfession += specialityListBox3.Items[i].Text + ",";
            }
        }

        if (selectedProfession == "")
        {
            selectedProfession = specialityListBox1.Items[0].Text;
        }

        return selectedProfession;
    }

    public static string GetProfessionID(ListBox specialityListBox1, ListBox specialityListBox2, ListBox specialityListBox3)
    {
        string selectedProfessionID = "";
        int i = 0;
        for (i = 0; i <= specialityListBox1.Items.Count - 1; i++)
        {
            if (specialityListBox1.Items[i].Selected == true)
            {
                selectedProfessionID += specialityListBox1.Items[i].Value + ",";
            }
        }

        for (i = 0; i <= specialityListBox2.Items.Count - 1; i++)
        {
            if (specialityListBox2.Items[i].Selected == true)
            {
                selectedProfessionID += specialityListBox2.Items[i].Value + ",";
            }
        }

        for (i = 0; i <= specialityListBox3.Items.Count - 1; i++)
        {
            if (specialityListBox3.Items[i].Selected == true)
            {
                selectedProfessionID += specialityListBox3.Items[i].Value + ",";
            }
        }
            
        if  (selectedProfessionID == "")
        {
            selectedProfessionID = specialityListBox1.Items[0].Value;
        }
        return selectedProfessionID;
    }

    public static int GetNumberOfProfession(ListBox specialityListBox1, ListBox specialityListBox2, ListBox specialityListBox3)
    {
        int ProfessionCount = 0;
        int i = 0;
        for (i = 0; i <= specialityListBox1.Items.Count - 1; i++)
        {
            if (specialityListBox1.Items[i].Selected == true)
            {
                ProfessionCount += 1;
            }
        }

        for (i = 0; i <= specialityListBox2.Items.Count - 1; i++)
        {
            if (specialityListBox2.Items[i].Selected == true)
            {
                ProfessionCount += 1;
            }
        }

        for (i = 0; i <= specialityListBox3.Items.Count - 1; i++)
        {
            if (specialityListBox3.Items[i].Selected == true)
            {
                ProfessionCount += 1;
            }
        }

        if (ProfessionCount == 0)
        {
            ProfessionCount = 1;
        }
        return ProfessionCount;
    }

    public static string GetZipcode(int countryID, int regionID, string cityName, string zipcode)
    {
        var selectedZipCode = "";
        using (var context = new SidejobEntities())
        {
            try
            {
                var queryRegion = from r in context.regionsUpdates
                                  where r.CountryId == countryID && r.RegionId == regionID
                                  select r.Code;
                var regioncode = queryRegion.FirstOrDefault();

                if (countryID == 254)
                {
                    var queryZipcode = from c in context.USAZipCodes
                                       where (c.City == cityName && c.PostalCodeID == Convert.ToInt32(zipcode) && c.State == regioncode)
                                       select c.PostalCodeID;
                    try
                    {
                        selectedZipCode = queryZipcode.FirstOrDefault().ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        selectedZipCode = "";
                    }
                   
                    if (selectedZipCode == "")
                    {

                        var defaultqueryzipcode = from c in context.USAZipCodes
                                                  where (c.City == cityName && c.State == regioncode)
                                                  select c.PostalCodeID;

                        selectedZipCode = defaultqueryzipcode.FirstOrDefault().ToString(CultureInfo.InvariantCulture);
                    }
                }
                if (countryID == 43)
                {
                    var queryZipcode = from c in context.CanadaZipCodes
                                       where (c.City == cityName && c.PostalCode == zipcode && c.ProvinceCode == regioncode)
                                       select c.PostalCodeID;
                    try
                    {
                        selectedZipCode = queryZipcode.FirstOrDefault().ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        selectedZipCode = "";
                    }
                    if (selectedZipCode == "")
                    {

                        var defaultqueryzipcode = from c in context.CanadaZipCodes
                                                  where (c.City == cityName && c.ProvinceCode == regioncode)
                                                  select c.PostalCode;

                        selectedZipCode = defaultqueryzipcode.First();
                    }
                }
            }
            catch (Exception )
            {

            }

        }
        return selectedZipCode;
    }

    public static int GetImpression(ListBox specialityListBox1, ListBox specialityListBox2, ListBox specialityListBox3)
    {
        int impressionCount = 1;
        int i = 0;
        for (i = 0; i <= specialityListBox1.Items.Count - 1; i++)
        {
            if (specialityListBox1.Items[i].Selected == true)
            {
                impressionCount += 1;
            }
        }

        for (i = 0; i <= specialityListBox2.Items.Count - 1; i++)
        {
            if (specialityListBox2.Items[i].Selected == true)
            {
                impressionCount += 1;
            }
        }

        for (i = 0; i <= specialityListBox3.Items.Count - 1; i++)
        {
            if (specialityListBox3.Items[i].Selected == true)
            {
                impressionCount += 1;
            }
        }

        return impressionCount * 50;
    }

    public static string GetLanguageNameByID(int id)
    {
        string language;
        switch (id)
        {
            case 1:
                language = Resources.Resource.English;
                break;
            case 2:
                language = Resources.Resource.French;
                break;
            case 3:
                language = Resources.Resource.Spanish;
                break;
            case 4:
                language = Resources.Resource.Chinese;
                break;
            case 5:
                language = Resources.Resource.Russian;
                break;
            case 6:
                language = Resources.Resource.Arabic;
                break;
            case 7:
                language = Resources.Resource.Japanese;
                break;
            case 8:
                language = Resources.Resource.German;
                break;
            default:
                language = Resources.Resource.English;
                break;
        }
        return language;
    }

    public static string GetSiteNameByID(int ID)
    {
        string site;
        switch (ID)
        {
            case 1:
                site = "YourSideJob.com";
                break;
            case 2:
                site = "Forum";
                break;
            case 3:
                site = "AdvertiseSide";
                break;
            default:
                site = "YourSideJob.com";
                break;
        }
        return site;
    }

    public static string GetSiteID()
    {
        return "1";
        /*Documentation:

             * 1: My-SideJob.com
             * 2: Advertise.My-SideJob.com
             * 3: Forum.My-SideJob.com
             
             */ 
    }

    public static string GetSectionNameByID( int id)
    {
        using (var context = new AdDatabaseModel.AdDatabaseEntities())
        {
            return (from c in context.Sections
                    where c.SectionsID == id
                    select c.SectionName).ToString();
        }
    }

    public static string GetPosition( int selectedindex)
    {
        using (var context = new AdDatabaseModel.AdDatabaseEntities())
        {
            return (from c in context.Positions
                    where c.PositionID == selectedindex
                    select c.Position1).ToString();
        }
    }

    public static string GetPositionSelectedIndex(int Position)
    {
        using (var context = new AdDatabaseModel.AdDatabaseEntities())
        {
            return (from c in context.Positions
                    where c.Position1 == Position
                    select c.PositionID).ToString();
        }
    }

    public static string GetNumberofProfession(int id)
    {
        using (var context = new AdDatabaseModel.AdDatabaseEntities())
        {

            var result = from c in context.PendingAds
                         where c.ID == id
                         select c.NumberofProfession;
            var resultcode = result.FirstOrDefault();



            return resultcode.ToString();
        }
    }

    public static int GetNumberofProfessionFromCurrent(int id)
    {
        using (var context = new AdDatabaseModel.AdDatabaseEntities())
        {

            var result = from c in context.PendingAds
                         where c.ID == id
                         select c.Profession;
            var resultQuery = result.FirstOrDefault();
            if (resultQuery != null)
            {
                var resultcode = resultQuery.ToString(CultureInfo.InvariantCulture);

                return resultcode.Count(f => f == ',');
            }
            else
            {
                return 0;
            }
        }
    }

    public static UserMachineLocation GetInfo()
    {

        try
        {
            // Full path to GeoLiteCity.dat file
            string fullDBPath = HttpContext.Current.Server.MapPath("~/App_Data/GeoLiteCity.dat");

            // Visitor's IP address
            string visitorIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            // Create objects needed for geo targeting
            var ls = new Geotargeting.LookupService(fullDBPath, Geotargeting.LookupService.GEOIP_STANDARD);

            Geotargeting.Location visitorLocation = ls.getLocation(visitorIP);
            //testing
            //var visitorLocation = ls.getLocation("68.70.88.2");

            var myWebVisitor = new WebsiteVisitor(HttpContext.Current);

            //YOU Need to account for NULL AND FOR LOWER CASE!!!!!!!!!!!!!!///////////////
            //YOU Need to account for NULL AND FOR LOWER CASE!!!!!!!!!!!!!!///////////////

            int countryID;
            int regionID;
            int cityID;
            using (var context = new SidejobEntities())
            {
                try
                {
                    var selectedCountryID =
                        from c in context.CountriesUpdates
                        where c.ISO2 == visitorLocation.countryCode
                        select c.CountryId;

                    countryID = selectedCountryID.FirstOrDefault();
                    if (countryID == 0)
                    {
                        //Default: US
                        countryID = 254;
                    }

                    var selectedRegionID =
                        from c in context.regionsUpdates
                        where c.Code == visitorLocation.region && c.CountryId == Convert.ToInt32(selectedCountryID)
                        select c.RegionId;
                    regionID = Convert.ToInt32(selectedRegionID);
                    if (regionID == 0)
                    {
                        //Default: NY
                        regionID = 154;
                    }

                    var c1 = from c in context.CitiesUpdates
                             where c.City == visitorLocation.city
                             select c;
                    var city = "";
                    cityID = 10182;
                    var c2 = c1.FirstOrDefault();
                    if (c2 != null)
                    {
                        var selectedCityID = c2.CityId;
                        cityID = Convert.ToInt32(selectedCityID);
                        city = c2.City;
                    }
                    if (cityID == 0)
                    {
                        //Default: NY
                        cityID = 10182;
                    }
                    return new UserMachineLocation(countryID, regionID, cityID, GetZipcode(countryID, regionID,city,visitorLocation.postalCode));
                }
                catch (Exception)
                {
                    //Default: US
                    countryID = 254;

                    //Default: NY
                    regionID = 154;

                    //Default: NY
                    cityID = 10182;

                    //Defauly Zipcode
                    //10185 : NY NY

                    return new UserMachineLocation(254, 154, 10182, "10185");

                }
            }
        }
        catch (Exception)
        {
            return new UserMachineLocation(254, 154, 10182, "10185");
        }
           
    }

    public static string GetIndustryById(int industryId, int lcid)
    {
        var resultcode ="";
        IQueryable<string> result;
        using (var context = new SidejobEntities())
        {
            switch (lcid)
            {
                case 1:
                    result = from c in context.Categories
                             where c.CategoryID == industryId
                             select c.CategoryName;
                    break;

                case 2:
                    result = from c in context.CategoriesFrs
                             where c.CategoryID == industryId
                             select c.CategoryName;
                    break;

                case 3:
                    result = from c in context.CategoriesSps
                             where c.CategoryID == industryId
                             select c.CategoryName;
                    break;
                default:
                    result = from c in context.Categories
                             where c.CategoryID == industryId
                             select c.CategoryName;
                    break;
            }     
            resultcode = result.ToList().FirstOrDefault();
        }

      
        return resultcode;
    }

    public static int GetNextSuccessfulPDTID()
    {
        int ID;
        var context = new AdDatabaseModel.AdDatabaseEntities();
        var max = context.AdvertiserSuccessfulPDTs.OrderByDescending(s => s.PDTID).FirstOrDefault();

        if (max == null)
        {
            ID = 0;
        }
        else
        {
            ID = max.PDTID + 1;
        }
        return ID;
    }

    public static string Login()
    {
        return string.Format("~/Account/Login.aspx?ReturnUrl={0}", HttpContext.Current.Request.Url.AbsoluteUri);
    }

    public static int GetPaymentID()
    {
        int paymentID;
        var context = new AdDatabaseModel.AdDatabaseEntities();
        var max = context.AdvertiserPaymentDues.OrderByDescending(s => s.PaymentDueID).FirstOrDefault();

        if (max == null)
        {
            paymentID = 0;
        }
        else
        {
            paymentID = max.PaymentDueID + 1;
        }
        return paymentID;
    }

    public static void DeleteAd(int AdID)
    {
        var context = new AdDatabaseModel.AdDatabaseEntities();
        context.DeleteCurrentAd(AdID);
        context.SaveChanges();
    }

    public static Guid? GetUserID()
    {
        var user = Membership.GetUser();
        if (user != null)
        {
            var userKey = user.ProviderUserKey;

            if (userKey != null)
            {
                var userID = (Guid)userKey;
                return userID;
            }
            else
            {
                Login();
                return null;
            }
        }
        else
        {
            Login();
            return null;
        }
            
    }

    public static Guid GetGuidUserID()
    {
        return (Guid) (Membership.GetUser().ProviderUserKey);
    }
    //public static Guid GetNotNullableUserID()
    //{
    //    var user = Membership.GetUser();
    //    var userKey = user.ProviderUserKey;
    //    var userID = (Guid)userKey;
    //    return userID;

    // }
    //  <asp:DropDownList ID="SectionDropDownList" runat="server" Width="230px" DataSourceID="SectionObjectDataSource"
    //                                DataTextField="SectionName" DataValueField="SectionsID" >
    //                            </asp:DropDownList>
    //                            <asp:ObjectDataSource ID="SectionObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                SelectMethod="GetSections" TypeName="AdUtilityTableAdapters.SectionsTableAdapter">
    //                                <SelectParameters>
    //                                    <asp:SessionParameter DefaultValue="1" Name="LCID" SessionField="LCID" Type="Int32" />
    //                                </SelectParameters>
    //                            </asp:ObjectDataSource>




    //   <asp:DropDownList ID="PositionDropDownList" runat="server" Width="230px" DataSourceID="PositionObjectDataSource"
    //                                DataTextField="PositionName" DataValueField="PositionID">
    //                            </asp:DropDownList>
    //                            <asp:ObjectDataSource ID="PositionObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                SelectMethod="GetPositions" TypeName="AdUtilityTableAdapters.PositionsTableAdapter">
    //                                <SelectParameters>
    //                                    <asp:SessionParameter DefaultValue="1" Name="LCID" SessionField="LCID" Type="Int32" />
    //                                </SelectParameters>
    //                            </asp:ObjectDataSource>



    //public static string GetFormatNameByID(int ID)
    //{
    //    string Format;
    //    switch (ID)
    //    {
    //        case 1:
    //            Format = Resources:Resource, Image;
    //            break;
    //        case 2:
    //            Format =  Resources:Resource, Flash; 
    //            break;
    //        default:
    //            Format = Resources:Resource, Image;
    //            break;
    //    }
    //    return Format;
    //}
  
    //public static string GetGenderNameByID(int ID)
    //{
    //    string Gender;
    //    switch (ID)
    //    {
    //        case 1:
    //            Gender =Resources.Resource.Male;
    //            break;
    //        case 2:
    //            Gender = Resources.Resource,.Female;
    //            break;
    //        default:
    //            Gender =Resources.Resource.Male;
    //            break;
    //    }
    //    return Gender;
    //}




    //  <asp:DropDownList ID="AgeDropDownList" runat="server" Width="230px" DataSourceID="AgeRangeObjectDataSource"
    //                                DataTextField="AgeRangeValue" DataValueField="AgeRangeID">
    //                            </asp:DropDownList>
    //                            <asp:ObjectDataSource ID="AgeRangeObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                SelectMethod="GetAgeRanges" TypeName="AdUtilityTableAdapters.AgeRangesTableAdapter">
    //                                <SelectParameters>
    //                                    <asp:SessionParameter DefaultValue="1" Name="LCID" SessionField="LCID" Type="Int32" />
    //                                </SelectParameters>
    //                            </asp:ObjectDataSource>




    //         <asp:UpdatePanel ID="CountryUpdatePanel" runat="server">
    //                            <ContentTemplate>
    //                                <asp:DropDownList ID="CountryDropDownList" runat="server" AutoPostBack="True" DataSourceID="CountriesDataSource"
    //                                    DataTextField="Title" DataValueField="CountryId" Font-Size="Small" Width="230px">
    //                                </asp:DropDownList>
    //                                <asp:ObjectDataSource ID="CountriesDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                    SelectMethod="GetCountries" TypeName="LocationDataSetTableAdapters.CountriesUpdateTableAdapter">
    //                                </asp:ObjectDataSource>
    //                            </ContentTemplate>
    //                        </asp:UpdatePanel>




    // <asp:UpdatePanel ID="RegionUpdatePanel" runat="server">
    //                            <ContentTemplate>
    //                                <asp:DropDownList ID="RegionsDropDownList" runat="server" AutoPostBack="True" DataSourceID="RegionDataSource"
    //                                    DataTextField="Region" DataValueField="RegionId" Font-Size="Small" Width="230px">
    //                                </asp:DropDownList>
    //                                <asp:ObjectDataSource ID="RegionDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                    SelectMethod="GetRegion" TypeName="LocationDataSetTableAdapters.regionsUpdateTableAdapter">
    //                                    <SelectParameters>
    //                                        <asp:ControlParameter ControlID="CountryDropDownList" Name="CountryId" PropertyName="SelectedValue"
    //                                            Type="Int32" />
    //                                    </SelectParameters>
    //                                </asp:ObjectDataSource>
    //                            </ContentTemplate>
    //                            <Triggers>
    //                                <asp:AsyncPostBackTrigger ControlID="CountryDropDownList" EventName="SelectedIndexChanged" />
    //                            </Triggers>
    //                        </asp:UpdatePanel>




    //<asp:UpdatePanel ID="CityUpdatePanel" runat="server">
    //                            <ContentTemplate>
    //                                <asp:DropDownList ID="CitiesDropDownList" runat="server" AutoPostBack="True" DataSourceID="CitiesObjectDataSource"
    //                                    DataTextField="City" DataValueField="CityId" Width="230px">
    //                                </asp:DropDownList>
    //                                <asp:ObjectDataSource ID="CitiesObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                    SelectMethod="GetCities" TypeName="LocationDataSetTableAdapters.CitiesUpdateTableAdapter">
    //                                    <SelectParameters>
    //                                        <asp:ControlParameter ControlID="CountryDropDownList" Name="CountryId" PropertyName="SelectedValue"
    //                                            Type="Int32" />
    //                                        <asp:ControlParameter ControlID="RegionsDropDownList" Name="RegionId" PropertyName="SelectedValue"
    //                                            Type="Int32" />
    //                                    </SelectParameters>
    //                                </asp:ObjectDataSource>
    //                            </ContentTemplate>
    //                            <Triggers>
    //                                <asp:AsyncPostBackTrigger ControlID="RegionsDropDownList" EventName="SelectedIndexChanged" />
    //                            </Triggers>
    //                        </asp:UpdatePanel>



    // <asp:DropDownList ID="IndustryDropDownList" runat="server" AutoPostBack="True" DataSourceID="CategoryObjectDataSource"
    //                                DataTextField="CategoryName" DataValueField="CategoryID" Width="260px">
    //                            </asp:DropDownList>
    //                            <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    //                                SelectMethod="GetCategory" TypeName="SpecializationDataSetTableAdapters.GetCategoryTableAdapter">
    //                                <SelectParameters>
    //                                    <asp:SessionParameter DefaultValue="1" Name="LCID" SessionField="LCID" Type="Int32" />
    //                                </SelectParameters>
    //                            </asp:ObjectDataSource>






































    public static string GetRegionByID(int regionid)
    {
        using (var context = new SidejobEntities())
        {
            return (from c in context.regionsUpdates
                    where c.RegionId == regionid
                    select c.Region).ToString();
        }
    }

    public static void UpdateImpression(string adId, int type)
    {
        try
        {
            // Full path to GeoLiteCity.dat file
            string fullDBPath = HttpContext.Current.Server.MapPath("~/App_Data/GeoLiteCity.dat");

            // Visitor's IP address
            string visitorIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            // Create objects needed for geo targeting
            var ls = new Geotargeting.LookupService(fullDBPath, Geotargeting.LookupService.GEOIP_STANDARD);

            //Geotargeting.Location visitorLocation = ls.getLocation(VisitorIP);
            //testing
            var visitorLocation = ls.getLocation("68.70.88.2");

            var myWebVisitor = new WebsiteVisitor(HttpContext.Current);

            //YOU Need to account for NULL AND FOR LOWER CASE!!!!!!!!!!!!!!///////////////
            //YOU Need to account for NULL AND FOR LOWER CASE!!!!!!!!!!!!!!///////////////

            int countryID;
            int regionID;
            int cityID;
            using (var context = new SidejobEntities())
            {
                try
                {
                    var selectedCountryID =
                        from c in context.CountriesUpdates
                        where c.ISO2 == visitorLocation.countryCode
                        select c.CountryId;

                    countryID = selectedCountryID.FirstOrDefault();
                    if (countryID == 0)
                    {
                        //Default: US
                        countryID = 254;
                    }

                    var selectedRegionID =
                        from c in context.regionsUpdates
                        where c.Code == visitorLocation.region && c.CountryId == Convert.ToInt32(selectedCountryID)
                        select c.RegionId;
                    regionID = Convert.ToInt32(selectedRegionID);
                    if (regionID == 0)
                    {
                        //Default: NY
                        regionID = 154;
                    }

                    var selectedCityID =
                        from c in context.CitiesUpdates
                        where c.City == visitorLocation.city
                        select c.CityId;
                    cityID = Convert.ToInt32(selectedCityID);
                    if (cityID == 0)
                    {
                        //Default: NY
                        cityID = 10182;
                    }
                }
                catch (Exception)
                {
                    //Default: US
                    countryID = 254;

                    //Default: NY
                    regionID = 154;

                    //Default: NY
                    cityID = 10182;
                }
            }

            ///////////////
            string refURL = "Exception";
            ///////////////

            ScheduleImpressionUtility.UpdateImpression(Convert.ToInt32(adId),
                                               Convert.ToDateTime(
                                                   DateTime.Today.ToString(
                                                       CultureInfo.InvariantCulture)),
                                               type, visitorIP, visitorLocation.countryCode,
                                               visitorLocation.region,
                                               visitorLocation.postalCode,
                                               visitorLocation.area_code, visitorLocation.dma_code,
                                               visitorLocation.latitude,
                                               visitorLocation.longitude,
                                               refURL, myWebVisitor.BrowserType,
                                               myWebVisitor.BrowserName,
                                               myWebVisitor.BrowserPlatform,
                                               myWebVisitor.UserHostAddress,
                                               myWebVisitor.UserHostName,
                                               myWebVisitor.UserLanguages,
                                               myWebVisitor.MobileDeviceManufacturer,
                                               myWebVisitor.MobileDeviceModel,
                                               myWebVisitor.DeviceType,
                                               100, "PRO", "haithem", "smith", "address", countryID,
                                               visitorLocation.countryCode,
                                               regionID, visitorLocation.region, cityID,
                                               visitorLocation.city,
                                               "91340605", 28, 1,
                                               "emailAddress", "photoPath", 5, 52, 1);


            //// UpdateImpression(int adId, DateTime entryData, int type, string iPAddress, string countryCode, string region, string postalcode,
            //int areaCode, int metroCode, double latiture, double longitude, string refURL, string browserType, string browserName, string browserPlatform,  string userHostAddress,
            //string userHostName,
            //string userLanguage,string mobileDeviceManufacturer,
            // string mobileDeviceModel, string deviceType,  int userId, string userRole, string firstName,
            //string lastName, string address, int countryID, string countryName, int regionID,
            //string regionName, int cityID, string cityName, string phone, int age, int gender,
            //string emailAddress, string photoPath, int industryID, int professionID, int lcid)
        }
        catch (Exception)
        {
        }
    }


    static readonly Dictionary<int, string> CurrencyDictionary = new Dictionary<int, string>()
                                                                     {
                                                                         {1, "AUD"},
                                                                         {2, "CAD"},
                                                                         {3, "EUR"},
                                                                         {4, "GBP"},
                                                                         {5, "JPY"},
                                                                         {6, "USD"},
                                                                         {7, "NZD"},
                                                                         {8, "CHF"},
                                                                         {9, "HKD"},
                                                                         {10, "SGD"},
                                                                         {11, "SEK"},
                                                                         {12, "DKK"},
                                                                         {13, "PLN"},
                                                                         {14, "NOK"},
                                                                         {15, "HUF"},
                                                                         {16, "CZK"},
                                                                         {17, "ILS"},
                                                                         {18, "MXN"},
                                                                         {19, "PHP"},
                                                                         {20, "TWD"},
                                                                         {21, "THB"}
                                                                     };

    public static string GetCurrencyCode(int currencyID)
    {
        return CurrencyDictionary.FirstOrDefault(x => x.Key == currencyID).Value.ToString(CultureInfo.InvariantCulture);
    }

    public static string GetHtmlFrom(string url)
    {
        var wc = new WebClient();
        var resStream = wc.OpenRead(url);
        if (resStream != null)
        {
            var sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
            return sr.ReadToEnd();
        }
        return "null";
    }
}

public class UserMachineLocation
{
    public int CountryID { get; set; }
    public int RegionID { get; set; }
    public int CityID { get; set; }
    public string Zipcode { get; set; }

    public UserMachineLocation(int c, int r, int ci, string z)
    {
        CountryID = c;
        RegionID = r;
        CityID = ci;
        Zipcode = z;
    }
}