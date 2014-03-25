using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class Constants
    {
        public const string INDEED_REQUEST_BASE = "http://api.indeed.com/ads/apisearch?publisher=4232565611687723";
        public const string LINKEDIN_REQUEST_BASE = "https://api.linkedin.com/v1/job-search:(jobs:(posting-date,company:(name),position:(title,location,job-type,experience-level),skills-and-experience,description-snippet,site-job-url,location-description))?";

        // https://api.linkedin.com/v1/job-search:(jobs:(posting-date,company:(name),position:(title,location,job-type,experience-level),skills-and-experience,description-snippet,site-job-url,location-description))?sort=DD&keyword=engineer&country-code=us&format=json&f_E=0,1,2,3

        // https://api.linkedin.com/v1/job-search:(jobs:(id,customer-job-code,active,posting-date,expiration-date,posting-timestamp,expiration-timestamp,company:(id,name,logo-url,employee-count-range,founded-year,specialties),position:(title,location,job-functions,industries,job-type,experience-level),skills-and-experience,description-snippet,description,salary,job-poster:(id,first-name,last-name,headline),referral-bonus,site-job-url,location-description))?keywords=ios&facets=company,location,industry,salary,job-function,date-posted&format=json

        public const string RESULT_LIMIT = "10";

        public const string INDEED_DISPLAY_NAME = "Indeed.com";

		/// <summary>
		/// The threashold for when to remove a duplicate job posting from results. If two job posts have a similarity above this number, one of them will be removed. Range: 0.0 - 1.0
		/// </summary>
		public const Double DUPLICATE_POST_SIMILARITY_THREASHOLD =0.6;

    }
}