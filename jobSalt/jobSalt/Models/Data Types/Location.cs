using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Data_Types
{

    public class Location
    {

        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public Location(string state, string city, string zip)
        {
            this.ZipCode = zip;
            this.State = state;
            this.City = city;
        }

        // This parameterless constructor is needed for deserialization
        public Location() { }
    }
}