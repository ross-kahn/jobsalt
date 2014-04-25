using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class SiteConfig
    {
        // Jobs intentionally left out as it is the home controller and must
        // be enabled at all times.
        public bool AlumniEnabled { get; set; }
        public bool SalaryEnabled { get; set; }
        public bool HousingEnabled { get; set; }

        public bool TwitterFeedEnabled { get; set; }
        public string TwitterUser { get; set; }
        public string TwitterWidgetID { get; set; }

        public DBConnectionConfig JobsDBConnection { get; set; }
        public DBConnectionConfig AlumniDBConnection { get; set; }
        public DBConnectionConfig SalaryDBConnection { get; set; }
        public DBConnectionConfig HousingDBConnection { get; set; }

        public List<string> AdminUsers { get; set; }
        public List<string> Moderators { get; set; }
        public List<string> RestrictAccessToUsers { get; set; }

        // ******** Site Theme ********
        #region Site Theme
        public string UniversityThemeCSSFile { get; set; }
        public string UniversityLogo { get; set; }
        public string BannerTopColor { get; set; }
        public string BannerBottomColor { get; set; }
        public string FilterEnabledTopColor { get; set; }
        public string FilterEnabledBottomColor { get; set; }
        public string FilterDisabledTopColor { get; set; }
        public string FilterDisabledBottomColor { get; set; }
        #endregion // Site Theme


        // An empty constructor is needed by the config loader. 
        // Set defaults here incase they aren't specified in the xml.
        public SiteConfig()
        {            
            AlumniEnabled = true;
            SalaryEnabled = true;
            HousingEnabled = true;

            TwitterFeedEnabled = false;
            TwitterUser = "";
            TwitterWidgetID = "";

            JobsDBConnection = new DBConnectionConfig();
            AlumniDBConnection = new DBConnectionConfig();
            SalaryDBConnection = new DBConnectionConfig();
            HousingDBConnection = new DBConnectionConfig();

            AdminUsers = new List<string>();
            Moderators = new List<string>();
            RestrictAccessToUsers = new List<string>();

            UniversityLogo = "";
            UniversityThemeCSSFile = "";
            BannerTopColor = "";
            BannerBottomColor = "";
            FilterEnabledTopColor = "";
            FilterEnabledBottomColor = "";
            FilterDisabledTopColor = "";
            FilterDisabledBottomColor = "";
        }
    }
}