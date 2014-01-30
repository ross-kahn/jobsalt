using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class Filter
    {

        public enum FilterType
        {
            Company_Name,
            Job_Title,
            Keyword,
            Location,
            Salary
        }

        public FilterType type { get; set; }

        public string value { get; set; }

        public Filter(FilterType Type, string Value)
        {
            type = Type;
            value = Value;
        }

    }
}