using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.Indeed_Module
{
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
}