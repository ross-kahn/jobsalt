using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Data_Types
{

    public class Location
    {

        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }

        public Location(string state, string city, string zip)
        {
            this.ZipCode = zip;
            this.State = state;
            this.City = city;
        }
    }
}