using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;

namespace jobSalt.Models.Modules.Jobs.LinkedIn_Module
{
    public class LinkedInTempData
    {
        private const string format = "&format=json";


        public LinkedInResult getDummyData()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\example_json_linkedin.txt" ;
            //bool fileexists = File.Exists(path);
            string json = File.ReadAllText(path);
            var serializer = new JavaScriptSerializer();

            LinkedInResult lResult = serializer.Deserialize<LinkedInResult>(json);

            return lResult;
        }
    }
}