using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.Dice_Module
	{
	public class DiceQueryBuilder
		{
		//Please refer to http://www.dice.com/common//content/util/apidoc/jobsearch.html for more info.
		// THEIR API IS EXTREMELY BASIC

		/// <summary>
		/// Builds a URI pertaining to the filters and parameters specified to query the Dice.Com REST JSON API. 
		/// </summary>
		/// <param name="filters">The filters specific to a particular job search.</param>
		/// <param name="Page">Page of results.</param>
		/// <param name="ResultsPerPage">The number of results you want to be returned each time this method is called.</param>
		/// <returns>A string representing the URI for a REST query.</returns> 
		public String BuildQuery ( FilterBag filters , int Page , int ResultsPerPage )
			{
			StringBuilder builder = new StringBuilder( );

			//add job search api .
			builder.Append( "http://service.dice.com/api/rest/jobsearch/v1/simple.xml?country=US&sd=a&sort=1&pgcnt="+ResultsPerPage +"&page="+Page );

			String text="&text="; //since this is perhaps the only field we have available aside from location and skill, everything is going to have to be added here.
			if ( filters.CompanyName != "" )
				{
				text+= filters.CompanyName+" " ;
				}
			if ( filters.FieldOfStudy != "" )
				{
				text+=filters.FieldOfStudy+" ";
				builder.Append( "&skill="+filters.FieldOfStudy );
				}
			if ( filters.JobTitle != "" )
				{
				text+= filters.JobTitle+" ";
				builder.Append( "&skill="+filters.Keyword );
				}
			if ( filters.Keyword != "" )
				{
				text+= filters.Keyword +" ";
				}
			if (filters.Location!=null && (filters.Location.City != "" || filters.Location.State != "" ||filters.Location.ZipCode !="" ))
				{
				builder.Append( "&city="+ filters.Location.City + ", "+ filters.Location.State +" " +filters.Location.ZipCode );
				}
			if ( !text.Equals( "&text=" ) )
				builder.Append( text );
			return builder.ToString( );
			}
		}
	}