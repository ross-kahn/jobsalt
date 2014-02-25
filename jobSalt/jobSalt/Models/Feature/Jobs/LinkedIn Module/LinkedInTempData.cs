using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;

namespace jobSalt.Models.Feature.Jobs.LinkedIn_Module
{
    public class LinkedInTempData
    {
        private const string format = "&format=json";


        public LinkedInJobPost.LinkedInResult getDummyData()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\example_json_linkedin.txt" ;
            //bool fileexists = File.Exists(path);
            string json = File.ReadAllText(path);
            var serializer = new JavaScriptSerializer();

            LinkedInJobPost.LinkedInResult lResult = serializer.Deserialize<LinkedInJobPost.LinkedInResult>(json);

            return lResult;
        }
    }
}