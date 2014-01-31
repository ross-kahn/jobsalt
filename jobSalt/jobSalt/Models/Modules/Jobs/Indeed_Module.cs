using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace jobSalt.Models
{
    public class Indeed_Module
    {

    
    }

    public class IndeedResult
    {
        public int Version { get; set; }
        public string Query { get; set; }
        public string Location { get; set; }
        public bool DupeFilter { get; set; }
        public bool Highlight { get; set; }
        public int Radius { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int TotalResults { get; set; }
        public int PageNumber { get; set; }
        public IndeedJobPost[] Results { get; set; }
    }

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