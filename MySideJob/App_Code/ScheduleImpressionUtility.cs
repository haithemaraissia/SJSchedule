using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using Roboblob.Utility;
using SidejobModel;

public class ScheduleImpressionUtility
{
    public static void UpdateImpression(int adId, DateTime entryData, int type, string iPAddress, string countryCode, string region, string postalcode,
                                        int areaCode, int metroCode, double latitude, double longitude, string refURL, string browserType, string browserName, string browserPlatform, string userHostAddress,
                                        string userHostName,
                                        string userLanguage, string mobileDeviceManufacturer,
                                        string mobileDeviceModel, string deviceType, int userId, string userRole, string firstName,
                                        string lastName, string address, int countryID, string countryName, int regionID,
                                        string regionName, int cityID, string cityName, string phone, int age, int gender,
                                        string emailAddress, string photoPath, int industryID, int professionID, int lcid)
    {
        using (var context =
            new AdDatabaseModel.AdDatabaseEntities())
            context.UpdateAdStatistic(adId, entryData, type, iPAddress, countryCode, region, postalcode,
                                      areaCode, metroCode, latitude, longitude, refURL, browserType, browserName,
                                      browserPlatform, userHostAddress,
                                      userHostName,
                                      userLanguage, mobileDeviceManufacturer,
                                      mobileDeviceModel, deviceType, userId, userRole, firstName,
                                      lastName, address, countryID, countryName, regionID,
                                      regionName, cityID, cityName, phone, age, gender,
                                      emailAddress, photoPath, industryID, professionID, lcid);
    }


    public static void UpdateClickTrack(int adId, DateTime entryData, int type, string iPAddress, string countryCode, string region, string postalcode,
                                        int areaCode, int metroCode, double latiture, double longitude, string refURL, string browserType, string browserName, string browserPlatform, string userHostAddress,
                                        string userHostName,
                                        string userLanguage, string mobileDeviceManufacturer,
                                        string mobileDeviceModel, string deviceType, int userId, string userRole, string firstName,
                                        string lastName, string address, int countryID, string countryName, int regionID,
                                        string regionName, int cityID, string cityName, string phone, int age, int gender,
                                        string emailAddress, string photoPath, int industryID, int professionID, int lcid)
    {
        using (var context =
            new AdDatabaseModel.
                AdDatabaseEntities())

            context.UpdateAdStatistic(adId, entryData, type, iPAddress, countryCode, region, postalcode,
                                      areaCode, metroCode, latiture, longitude, refURL, browserType, browserName,
                                      browserPlatform, userHostAddress,
                                      userHostName,
                                      userLanguage, mobileDeviceManufacturer,
                                      mobileDeviceModel, deviceType, userId, userRole, firstName,
                                      lastName, address, countryID, countryName, regionID,
                                      regionName, cityID, cityName, phone, age, gender,
                                      emailAddress, photoPath, industryID, professionID, lcid);

    }



    public static void SimplifiedUpdateImpression(string adId, int type, string fullDBPath, string visitorIP, string currentURL, int userId, string userRole, string firstName, string lastName, string address, string phone, int age, int gender, string emailAddress, string photoPath, int industryID, int professionID, int lcid)
    {
        try
        {
            // Full path to GeoLiteCity.dat file
            //fullDBPath = Server.MapPath("~/App_Data/GeoLiteCity.dat");

            // Visitor's IP address
            //string visitorIP = Request.ServerVariables["REMOTE_ADDR"];

            // Create objects needed for geo targeting
            var ls = new Geotargeting.LookupService(fullDBPath, Geotargeting.LookupService.GEOIP_STANDARD);

            //Geotargeting.Location visitorLocation = ls.getLocation(VisitorIP);
            //testing
            //var visitorLocation = ls.getLocation("68.70.88.2");
            var visitorLocation = ls.getLocation(visitorIP);

            var myWebVisitor = new WebsiteVisitor(HttpContext.Current);

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

                    int cid = countryID;
                    var selectedRegionID =
                        from c in context.regionsUpdates
                        where c.Code == visitorLocation.region && c.CountryId == cid
                        select c.RegionId;
                    regionID = selectedRegionID.FirstOrDefault();
                    if (regionID == 0)
                    {
                        //Default: NY
                        regionID = 154;
                    }

                    var selectedCityID =
                        from c in context.CitiesUpdates
                        where c.City == visitorLocation.city
                        select c.CityId;
                    cityID = selectedCityID.FirstOrDefault();
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

            UpdateImpression(Convert.ToInt32(adId),
                             Convert.ToDateTime(DateTime.Today.ToString(CultureInfo.InvariantCulture)),
                             type, visitorIP, visitorLocation.countryCode,
                             visitorLocation.region,
                             visitorLocation.postalCode,
                             visitorLocation.area_code, visitorLocation.dma_code,
                             visitorLocation.latitude,
                             visitorLocation.longitude,
                             currentURL, myWebVisitor.BrowserType,
                             myWebVisitor.BrowserName,
                             myWebVisitor.BrowserPlatform,
                             myWebVisitor.UserHostAddress,
                             myWebVisitor.UserHostName,
                             myWebVisitor.UserLanguages,
                             myWebVisitor.MobileDeviceManufacturer,
                             myWebVisitor.MobileDeviceModel,
                             myWebVisitor.DeviceType,
                             userId, userRole, firstName, lastName, address, countryID,
                             visitorLocation.countryCode,
                             regionID, visitorLocation.region, cityID,
                             visitorLocation.city,
                             phone, age, gender,
                             emailAddress, photoPath, industryID, professionID, lcid);
        }
        catch (Exception)
        {

        }
    }


    public static void SimplifiedUpdateClik(string adId, int type, string fullDBPath, string visitorIP, string currentURL, int userId, string userRole, string firstName, string lastName, string address, string phone, int age, int gender, string emailAddress, string photoPath, int industryID, int professionID, int lcid)
    {
        try
        {
            // Full path to GeoLiteCity.dat file
            //fullDBPath = Server.MapPath("~/App_Data/GeoLiteCity.dat");

            // Visitor's IP address
            //string visitorIP = Request.ServerVariables["REMOTE_ADDR"];

            // Create objects needed for geo targeting
            var ls = new Geotargeting.LookupService(fullDBPath, Geotargeting.LookupService.GEOIP_STANDARD);

            //Geotargeting.Location visitorLocation = ls.getLocation(VisitorIP);
            //testing
            //var visitorLocation = ls.getLocation("68.70.88.2");
            var visitorLocation = ls.getLocation(visitorIP);

            var myWebVisitor = new WebsiteVisitor(HttpContext.Current);

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

                    int cid = countryID;
                    var selectedRegionID =
                        from c in context.regionsUpdates
                        where c.Code == visitorLocation.region && c.CountryId == cid
                        select c.RegionId;
                    regionID = selectedRegionID.FirstOrDefault();
                    if (regionID == 0)
                    {
                        //Default: NY
                        regionID = 154;
                    }

                    var selectedCityID =
                        from c in context.CitiesUpdates
                        where c.City == visitorLocation.city
                        select c.CityId;
                    cityID = selectedCityID.FirstOrDefault();
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

            UpdateImpression(Convert.ToInt32(adId),
                             Convert.ToDateTime(DateTime.Today.ToString(CultureInfo.InvariantCulture)),
                             type, visitorIP, visitorLocation.countryCode,
                             visitorLocation.region,
                             visitorLocation.postalCode,
                             visitorLocation.area_code, visitorLocation.dma_code,
                             visitorLocation.latitude,
                             visitorLocation.longitude,
                             currentURL, myWebVisitor.BrowserType,
                             myWebVisitor.BrowserName,
                             myWebVisitor.BrowserPlatform,
                             myWebVisitor.UserHostAddress,
                             myWebVisitor.UserHostName,
                             myWebVisitor.UserLanguages,
                             myWebVisitor.MobileDeviceManufacturer,
                             myWebVisitor.MobileDeviceModel,
                             myWebVisitor.DeviceType,
                             userId, userRole, firstName, lastName, address, countryID,
                             visitorLocation.countryCode,
                             regionID, visitorLocation.region, cityID,
                             visitorLocation.city,
                             phone, age, gender,
                             emailAddress, photoPath, industryID, professionID, lcid);
        }
        catch (Exception)
        {

        }
    }

    public static void UpdateImpression(string adId, int type, string fullDBPath, string visitorIP, string url)
    {
        try
        {
            SimplifiedUpdateImpression(adId, type, fullDBPath, visitorIP, url, UserImpression.ImpressionUserID,
                                       UserImpression.ImpressionUserRole, UserImpression.ImpressionFirstName,
                                       UserImpression.ImpressionLastName, UserImpression.ImpressionAddress,
                                       UserImpression.ImpressionPhone, UserImpression.ImpressionAge,
                                       UserImpression.ImpressionGender, UserImpression.ImpressionEmailAddress,
                                       UserImpression.ImpressionPhotoPath, UserImpression.ImpressionIndustryID,
                                       UserImpression.ImpressionProfession, UserImpression.ImpressionLCID);
        }
        catch (Exception e)
        {
            Debug.WriteLine("Problem with SimplifiedUpdateImpressionFunction");
        }
    }

    




}

public static class UserImpression
{
    public static int ImpressionUserID { get; set; }
    public static string ImpressionUserRole { get; set; }
    public static string ImpressionFirstName { get; set; }
    public static string ImpressionLastName { get; set; }
    public static string ImpressionAddress { get; set; }
    public static string ImpressionPhone { get; set; }
    public static int ImpressionGender { get; set; }
    public static int ImpressionAge { get; set; }
    public static string ImpressionEmailAddress { get; set; }
    public static string ImpressionPhotoPath { get; set; }
    public static string ImpressionCountry { get; set; }
    public static string ImpressionRegion { get; set; }
    public static string ImpressionCity { get; set; }
    public static string ImpressionZipcode { get; set; }
    public static int ImpressionIndustryID { get; set; }
    public static int ImpressionProfession { get; set; }
    public static int ImpressionLCID { get; set; }


    public static void NonAuthenticatedUserImpression()
    {
        ImpressionUserID = 0;
        ImpressionUserRole = Resources.Resource.Unkown;
        ImpressionFirstName = Resources.Resource.Unkown;
        ImpressionLastName = Resources.Resource.Unkown;
        ImpressionAddress = Resources.Resource.Unkown;
        ImpressionPhone = Resources.Resource.Unkown;
        ImpressionAge = 25;
        ImpressionGender = 1;
        ImpressionEmailAddress = Resources.Resource.Unkown;
        ImpressionPhotoPath = "http://www.haithem-araissia.com/WIP2/RightCleanSideJOB2008FromInetpub/CleanSIDEJOB2008/Images/Profile/unknowuser.png";
        ImpressionCountry = "254";
        ImpressionRegion = "128";
        ImpressionCity = "210";
        ImpressionZipcode = "66203";
        ImpressionIndustryID = 1;
        ImpressionProfession = 1;
        ImpressionLCID = 1;
    }


    //For future if we neeed to 
    public static void NonAuthenticatedUserImpressionROS()
    {
        ImpressionUserID = 0;
        ImpressionUserRole = "Admin";
        ImpressionFirstName = "Jack";
        ImpressionLastName = "Smith";
        ImpressionAddress = "Address";
        ImpressionPhone = "Phone";
        ImpressionAge = 25;
        ImpressionGender = 1;
        ImpressionEmailAddress = "EmailAddress";
        ImpressionPhotoPath = "http://www.haithem-araissia.com/WIP2/RightCleanSideJOB2008FromInetpub/CleanSIDEJOB2008/Images/Profile/unknowuser.png";
        ImpressionCountry = "254";
        ImpressionRegion = "128";
        ImpressionCity = "210";
        ImpressionZipcode = "66203";
        ImpressionIndustryID = 1;
        ImpressionProfession = 1;
        ImpressionLCID = 1;
    }
    //For future if we neeed to 
    public static void NonAuthenticatedUser(string sectionid)
    {
        switch (sectionid)
        {
            case "1":
                NonAuthenticatedUserImpressionROS();
                break;
                
        }

    }
    public static void AuthenticatedUserImpression(int userid, string userrole, string firstname, string lastname, string address, string phone, string age,
                                                   int gender, string emailaddress, string photopath, string country, string region, string city, string zipcode, int industryid , int profession, int lcid)
    {
        ImpressionUserID = userid;
        ImpressionUserRole = userrole;
        ImpressionFirstName = firstname;
        ImpressionLastName = lastname;
        ImpressionAddress = address;
        ImpressionPhone = phone;
        ImpressionAge = 25;
        ImpressionGender = 1;
        ImpressionEmailAddress = emailaddress;
        ImpressionPhotoPath = photopath;
        ImpressionCountry = country;
        ImpressionRegion = region;
        ImpressionCity = city;
        ImpressionZipcode = zipcode;
        ImpressionIndustryID = industryid;
        ImpressionProfession = profession;
        ImpressionLCID = lcid;
    }


        
}