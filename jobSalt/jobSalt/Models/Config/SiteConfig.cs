using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class SiteConfig
    {
        public string UniversityThemeCSSFile { get; set; }

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

        // An empty constructor is needed by the config loader. 
        // Set defaults here incase they aren't specified in the xml.
        public SiteConfig()
        {
            UniversityThemeCSSFile = "";
            AlumniEnabled = true;
            SalaryEnabled = true;
            HousingEnabled = true;

            TwitterFeedEnabled = false;
            TwitterUser = "";
            TwitterWidgetID = "";

            JobsDBConnection = new DBConnectionConfig();
            AlumniDBConnection = new DBConnectionConfig();
            SalaryDBConnection = new DBConnectionConfig();
        }
    }
}