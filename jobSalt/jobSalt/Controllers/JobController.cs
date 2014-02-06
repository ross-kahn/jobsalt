using jobSalt.Models.Modules.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class JobController : Controller
    {
        private JobShepard shepard = new JobShepard();

        public ActionResult Index(List<Models.Filter> filters = null, int page = 1, int resultsPerPage = 10)
        { 
			// If filters is null replace it
            filters = filters ?? new List<Models.Filter>();

			//filterStrings is expected to be in the form of "{targetField,value},{targetField1,value1}" without the quotes.
			String filterStrings= Request.QueryString["filterStrings"];

			if ( filterStrings!=null )
				{

				//fill filters with filters from filterStrings
				foreach ( string filterString in filterStrings.Split( new Char[] { '{' , '}' } ) ) //split into individual filter strings first
					{
					//split current filter string into a string array {targetFiled,value}
					String[] currentFilterString = filterString.Split( new char[] { ',' } );

					if ( currentFilterString.Length==2 && Enum.IsDefined( typeof( Models.Field ) , currentFilterString[0] ) )
						{

						//get target field from Enum
						Models.Field targetField = (Models.Field)Enum.Parse( typeof( Models.Field ) , currentFilterString[0] );

						//build the current filter
						Models.Filter filter = new Models.Filter( targetField , currentFilterString[1] );

						//if the filter is not already in the filter list, add it.
						if ( !filters.Any( a => a.TargetField.Equals( filter.TargetField ) && a.Value.Equals( filter.Value ) ) )
							{
							filters.Add( filter );
							}
						}

					}
				}
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

    }
}
