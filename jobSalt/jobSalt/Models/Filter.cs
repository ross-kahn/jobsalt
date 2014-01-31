﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public enum Field { Keyword, Source, Date, CompanyName, JobTitle, Location, Salary, Description, FieldOfStudy}

    public class Filter
    {
        public Field TargetField { get; private set; }
        public string Value { get; private set; }

        public Filter(Field targetField, string value)
        {
            this.TargetField = targetField;
            this.Value = value;
        }
    }
}