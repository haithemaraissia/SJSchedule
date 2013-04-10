using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

namespace Roboblob.Utility
{
    public class OnlineVisitorsUtility
    {
        public static Dictionary<string, WebsiteVisitor> Visitors = new Dictionary<string, WebsiteVisitor>();
    }

    /// <summary>
    /// Summary description for WebsiteVisitor
    /// </summary>
    public class WebsiteVisitor
    {
        private string sessionId;

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        /// <summary>
        /// UserIp Property
        /// </summary>
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private string ipAddress;

        /// <summary>
        /// UrlReferrer Property
        /// </summary>
        public string UrlReferrer
        {
            get { return urlReferrer; }
            set { urlReferrer = value; }
        }

        private string urlReferrer;

        /// <summary>
        /// EnterUrl Property
        /// </summary>
        public string EnterUrl
        {
            get { return enterUrl; }
            set { enterUrl = value; }
        }

        private string enterUrl;


        /// <summary>
        /// HostName Property
        /// </summary>
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        private string hostName;

        /// <summary>
        /// UserAgent Property
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        private string userAgent;

        /// <summary>
        /// SessionStarted Property
        /// </summary>
        public DateTime SessionStarted
        {
            get { return sessionStarted; }
            set { sessionStarted = value; }
        }

        private DateTime sessionStarted;


        /// <summary>
        /// DeviceType Property
        /// </summary>
        public string DeviceType
        {
            get { return devicetype; }
            set { devicetype = value; }
        }

        private string devicetype;



        public WebsiteVisitor(HttpContext context)
        {
            if ((context != null) && (context.Request != null) && (context.Session != null))
            {
                this.sessionId = context.Session.SessionID;
                sessionStarted = DateTime.Now;
                userAgent = string.IsNullOrEmpty(context.Request.UserAgent) ? "" : context.Request.UserAgent;
                ipAddress = context.Request.UserHostAddress;
                if (context.Request.UrlReferrer != null)
                {
                    urlReferrer = string.IsNullOrEmpty(context.Request.UrlReferrer.OriginalString)
                                      ? ""
                                      : context.Request.UrlReferrer.OriginalString;
                }
                enterUrl = string.IsNullOrEmpty(context.Request.Url.OriginalString)
                               ? ""
                               : context.Request.Url.OriginalString;
                browsertype = context.Request.Browser.Type;
                browsername = context.Request.Browser.Browser;
                browserversion = context.Request.Browser.Version;
                browsermajorversion = context.Request.Browser.MajorVersion.ToString();
                browserminorversion = context.Request.Browser.MinorVersion.ToString();
                browserplatform = context.Request.Browser.Platform;
                iswin16 = context.Request.Browser.Win16.ToString();
                isWin32 = context.Request.Browser.Win32.ToString();
                supportcookie = context.Request.Browser.Cookies.ToString();
                ismobiledevice = context.Request.Browser.IsMobileDevice.ToString();
                if (ismobiledevice.ToLower().ToString() == "true")
                {
                    devicetype = DeviceDetection();
                }
                else
                {
                    devicetype = "Station";
                }
                mobiledevicemanufacturer = context.Request.Browser.MobileDeviceModel;
                mobiledevicemodel = context.Request.Browser.MobileDeviceManufacturer;


                
                userhostaddress = context.Request.UserHostAddress;
                userhostname = context.Request.UserHostName;
                userlanguages = context.Request.UserLanguages[0].ToString();

            }
        }


        public string DeviceDetection()
        {
            if (HttpContext.Current.Request.UserAgent != null)
            {
                string userAgent = HttpContext.Current.Request.UserAgent.ToLower();
                if (userAgent.Contains("iphone"))
                {
                    return "IPhone";
                }
                else if (userAgent.Contains("ipod"))
                {
                    return "Ipod";

                }

                else if (userAgent.Contains("android"))
                {
                    return "Android";

                }
                else if (userAgent.Contains("blackberry"))
                {
                    return "Blackberry";

                }
                else if (userAgent.Contains("windows ce"))
                {
                    return "Windows CE";

                }
                else if (userAgent.Contains("opera mini"))
                {
                    return "Opera Mini";

                }
                else if (userAgent.Contains("palm"))
                {
                    return "Palm";

                }
                else if (userAgent.Contains("chrome"))
                {
                    return "Chrome";

                }
                else if (userAgent.Contains("ipad"))
                {
                    return "IPad";

                }
                else
                {
                    return "Mobile";

                }
            }
            else
            {
                return "Mobile";
            }
        }




    /// <summary>
        /// BrowserType Property
        /// </summary>
        public string BrowserType
        {
            get { return browsertype; }
            set { browsertype = value; }
        } 
        private string browsertype;

        /// <summary>
        /// BrowserName Property
        /// </summary>
        public string BrowserName
        {
            get { return browsername; }
            set { browsername = value; }
        }
        private string browsername;

        /// <summary>
        /// BrowserVersion Property
        /// </summary>
        public string BrowserVersion
        {
            get { return browserversion; }
            set { browserversion = value; }
        }
        private string browserversion;


        /// <summary>
        /// BrowserVersion Property
        /// </summary>
        public string BrowserMajorVersion
        {
            get { return browsermajorversion; }
            set { browsermajorversion = value; }
        }
        private string browsermajorversion;


        /// <summary>
        /// BrowserVersion Property
        /// </summary>
        public string BrowserMinorVersion
        {
            get { return browserminorversion; }
            set { browserminorversion = value; }
        }
        private string browserminorversion;


        /// <summary>
        /// BrowserPlatform Property
        /// </summary>
        public string BrowserPlatform
        {
            get { return browserplatform; }
            set { browserplatform = value; }
        }
        private string browserplatform;


        /// <summary>
        /// IsWin16 Property
        /// </summary>
        public string IsWin16
        {
            get { return iswin16; }
            set { iswin16 = value; }
        }
        private string iswin16;

        /// <summary>
        /// IsWin32 Property
        /// </summary>
        public string IsWin32
        {
            get { return isWin32; }
            set { isWin32 = value; }
        }
        private string isWin32;


        /// <summary>
        /// SupportCookie Property
        /// </summary>
        public string SupportCookie
        {
            get { return supportcookie; }
            set { supportcookie = value; }
        }
        private string supportcookie;




        /// <summary>
        /// IsWin64 Property
        /// </summary>
        public string IsMobileDevice
        {
            get { return ismobiledevice; }
            set { ismobiledevice = value; }
        }
        private string ismobiledevice;


        /// <summary>
        /// IsWin64 Property
        /// </summary>
        public string MobileDeviceManufacturer
        {
            get { return mobiledevicemanufacturer; }
            set { mobiledevicemanufacturer = value; }
        }
        private string mobiledevicemanufacturer;


        /// <summary>
        /// IsWin64 Property
        /// </summary>
        public string MobileDeviceModel
        {
            get { return mobiledevicemodel; }
            set { mobiledevicemodel = value; }
        }
        private string mobiledevicemodel;



        /// <summary>
        /// UserLanguages Property
        /// </summary>
        public string UserLanguages
        {
            get { return userlanguages; }
            set { userlanguages = value; }
        }
        private string userlanguages;



        /// <summary>
        /// UserHostAddress Property
        /// </summary>
        public string UserHostAddress
        {
            get { return userhostaddress; }
            set { userhostaddress = value; }
        }
        private string userhostaddress;



        /// <summary>
        /// UserHostName Property
        /// </summary>
        public string UserHostName
        {
            get { return userhostname; }
            set { userhostname = value; }
        }
        private string userhostname;





    }
}