using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.Indeed_Module
{
    public class IndeedJobPost
    {
        /// <summary>
        /// Title of the Job from Indeed
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// Company offering the job
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// City the Job is located in
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State the Job is located in
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Country the job is located in
        /// </summary>
        public string Country { get; set; }
        public string FormattedLocation { get; set; }
        public string Source { get; set; }
        /// <summary>
        /// Date Job post submitted
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Snippet of the job text, short description
        /// </summary>
        public string Snippet { get; set; }
        /// <summary>
        /// Source URL
        /// </summary>
        public string URL { get; set; }
        public string OnMouseDown { get; set; }
        /// <summary>
        /// Latitute Location
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Longitude Location
        /// </summary>
        public float Longitude { get; set; }
        public string JobKey { get; set; }
        /// <summary>
        /// Whether or not this is a sponsered job offer
        /// </summary>
        public bool Sponsored { get; set; }
        /// <summary>
        /// Whether or not the job is expired
        /// </summary>
        public bool Expired { get; set; }
        public string FormattedLocationFull { get; set; }
        public string FormattedRelativeTime { get; set; }
    }
}