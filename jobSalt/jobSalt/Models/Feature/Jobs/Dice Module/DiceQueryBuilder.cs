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
		/// 
		/// </summary>
		/// <param name="FilterDict"></param>
		/// <param name="Page"></param>
		/// <param name="ResultsPerPage"></param>
		/// <returns></returns>
		public String BuildQuery ( FilterBag filters , int Page , int ResultsPerPage )
			{
			StringBuilder builder = new StringBuilder( );

			//add job search api .
			builder.Append( "http://service.dice.com/api/rest/jobsearch/v1/simple.xml?country=US&sort=2&pgcnt="+ResultsPerPage +"&page="+Page );

			if ( filters.CompanyName != "" )
				{
				builder.Append( "&text="+ filters.CompanyName );
				}
			if ( filters.FieldOfStudy != "" )
				{
				builder.Append( "&text="+filters.FieldOfStudy );
				}
			if ( filters.JobTitle != "" )
				{
				builder.Append( "&text="+ filters.JobTitle );
				}
			if ( filters.Keyword != "" )
				{
				builder.Append( "&text="+filters.Keyword );
				}
			if ( filters.Location.City != "" || filters.Location.State != "" ||filters.Location.ZipCode !="" )
				{
				builder.Append( "&city="+ filters.Location.City + ", "+ filters.Location.State +" " +filters.Location.ZipCode );
				}
			
			return builder.ToString( );
			}
		}
	}