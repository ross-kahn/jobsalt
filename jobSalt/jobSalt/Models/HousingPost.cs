using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class HousingPost
    {
        public DateTime DatePosted { get; set; }
        public String Company { get; set; }
        public Location Location { get; set; }
        public String PriceRange { get; set; }
        public String Description { get; set; }
        public int Rating { get; set; }
    }
}