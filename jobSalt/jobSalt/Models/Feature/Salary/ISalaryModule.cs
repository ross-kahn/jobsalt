using jobSalt.Models.Data_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Salary
{
    interface ISalaryModule
    {
        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        Source Source { get; }

        /// <summary>
        /// Asyncronously grab a set of Salary postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <returns>The Salary postings</returns>
        SalaryPost GetSalaries(FilterBag filters);

    }
}