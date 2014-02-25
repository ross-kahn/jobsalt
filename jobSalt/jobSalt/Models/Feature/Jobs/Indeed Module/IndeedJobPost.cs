using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.Indeed_Module
{
    public class IndeedJobPost
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FormattedLocation { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public string Snippet { get; set; }
        public string URL { get; set; }
        public string OnMouseDown { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string JobKey { get; set; }
        public bool Sponsored { get; set; }
        public bool Expired { get; set; }
        public string FormattedLocationFull { get; set; }
        public string FormattedRelativeTime { get; set; }
    }
}