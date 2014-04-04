using jobSalt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    /// <summary>
    /// This class is used across other controllers and views to modify
    /// the filters. An API is exposed through the FilterUtlityController to add/remove/edit
    /// filters stored in the filterString.
    /// </summary>
    public class FilterUtility
    {
        public static List<Models.Filter> GetFilters(string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);

            return fb.GetFilters();
        }

        public static string GetFilterValue(Field targetField, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            return fb.GetFilterValue(targetField);
        }

        public static string AssignFilter(Field targetField, string value, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            fb.AssignFilter(new Models.Filter(targetField, value));
            string newFilterString = fb.JsonEncode().Replace("\"", "'");

            return newFilterString;
        }

        public static bool FilterIsSet(List<Filter> filters, Field targetField)
        {
            return filters.Find(filter => filter.TargetField == targetField) != null;
        }

        public static List<Field> FiltersForFeature(Features feature)
        {
            List<Field> fields = new List<Field>();

            switch (feature)
            {
                case Features.Jobs:
                    fields.Add(Field.Keyword);
                    fields.Add(Field.FieldOfStudy);
                    fields.Add(Field.CompanyName);
                    fields.Add(Field.JobTitle);
                    fields.Add(Field.Location);
                    fields.Add(Field.Source);
                    break;
                case Features.Alumni:
                    fields.Add(Field.Keyword);
                    fields.Add(Field.FieldOfStudy);
                    fields.Add(Field.CompanyName);
                    fields.Add(Field.Location);
                    break;
                case Features.Salary:
                    fields.Add(Field.FieldOfStudy);
                    fields.Add(Field.Location);
                    break;
                case Features.Housing:
                    fields.Add(Field.Keyword);
                    fields.Add(Field.FieldOfStudy);
                    fields.Add(Field.CompanyName);
                    fields.Add(Field.JobTitle);
                    fields.Add(Field.Location);
                    fields.Add(Field.Source);
                    break;
            }

            return fields;
        }

        public static string FilterDisplayName(Field filter)
        {
            switch (filter)
            {
                case Field.CompanyName:
                    return "Company";
                case Field.Date:
                    return "Date";
                case Field.EducationCode:
                    return "Education Code";
                case Field.FieldOfStudy:
                    return "Field of Study";
                case Field.JobTitle:
                    return "Job Title";
                case Field.Keyword:
                    return "Keyword";
                case Field.Location:
                    return "Location";
                case Field.Salary:
                    return "Salary";
                case Field.Source:
                    return "Source";
                default:
                    return filter.ToString();
            }
        }
    }
}
