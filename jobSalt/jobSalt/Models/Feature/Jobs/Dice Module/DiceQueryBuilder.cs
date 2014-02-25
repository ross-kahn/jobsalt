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
		public String BuildQuery ( Dictionary<Field , String> FilterDict , int Page , int ResultsPerPage )
			{
			StringBuilder builder = new StringBuilder( );

			//add job search api .
			builder.Append( "http://service.dice.com/api/rest/jobsearch/v1/simple.xml?country=US&sort=2&pgcnt="+ResultsPerPage +"&page="+Page );

			foreach ( Field key in FilterDict.Keys )
				{
				switch ( key )
					{
					case Field.CompanyName: 
						builder.Append( "&text="+FilterDict[key] );
						break;

					case Field.Date:
						break;

					case Field.FieldOfStudy:
						break;

					case Field.JobTitle:
						break;

					case Field.Keyword:
						builder.Append( "&text="+FilterDict[key] );
						break;

					case Field.Location:
						builder.Append( "&city="+FilterDict[key] );
						break;

					case Field.Salary:
						break;

					case Field.Source:
						break;
					case Field.EducationCode:
						break;

					default:
						break;
					}
				}
			return builder.ToString( );
			}
		}
	}