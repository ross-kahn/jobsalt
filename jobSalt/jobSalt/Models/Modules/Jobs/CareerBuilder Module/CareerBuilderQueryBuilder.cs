﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace jobSalt.Models.Modules.Jobs.CareerBuilder_Module
	{
	public class CareerBuilderQueryBuilder
		{
		//Please refer to http://api.careerbuilder.com/Search/jobsearch/jobsearchinfo.aspx for more info.

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

			//add job search api and required dev key.
			builder.Append( "http://api.careerbuilder.com/v1/jobsearch?DeveloperKey=WDHV0RV6Q60BJ3WD2H15&PerPage="+ResultsPerPage +"&PageNumber="+Page );

			foreach ( Field key in FilterDict.Keys )
				{
				switch (key)
                {
                    case Field.CompanyName:
					String val = FilterDict[key];
					if ( val.Contains( ' ' ) )
						val = '"' +val +'"'; //surround with quotes if spaces.
					builder.Append( "&CompanyName="+val);
                        break;

                    case Field.Date:
                        break;

                    case Field.FieldOfStudy: 
                        break;

                    case Field.JobTitle:
						builder.Append( "&JobTitle="+FilterDict[key] );
                        break;

                    case Field.Keyword:
                        builder.Append("&Keywords="+FilterDict[key]);
                        break;

                    case Field.Location:
						builder.Append( "&Location=" + FilterDict[key] );
                        break;

                    case Field.Salary:
                        break;

                    case Field.Source:
                        break;

                    default:
                        break;
                }
				}
			return builder.ToString( );
			}
		}
	}