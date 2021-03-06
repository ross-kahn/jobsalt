﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni
{
    public class AlumniPost
    {
        public String Company { get; set; }
        public Location Location { get; set; }
        public String FieldOfStudy { get; set; }
        public String Name { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public int GraduatingYear { get; set; }
    }
}