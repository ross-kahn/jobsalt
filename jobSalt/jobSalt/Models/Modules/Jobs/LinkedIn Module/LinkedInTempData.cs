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
            string json = File.ReadAllText("C:\\Users\\Ross\\Documents\\GitHub\\jobsalt\\jobSalt\\jobSalt\\Models\\Modules\\Jobs\\LinkedIn Module\\example_json_linkedin.txt");
            var serializer = new JavaScriptSerializer();

            LinkedInResult lResult = serializer.Deserialize<LinkedInResult>(json);

            return lResult;
        }
    }
}