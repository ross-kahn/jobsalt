using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs.CareerBuilder_Module
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
		public String BuildQuery ( FilterBag filters , int Page , int ResultsPerPage )
		{
			StringBuilder builder = new StringBuilder( );

			//add job search api and required dev key.
			builder.Append( "http://api.careerbuilder.com/v1/jobsearch?DeveloperKey=WDHV0RV6Q60BJ3WD2H15&PerPage="+ResultsPerPage +"&PageNumber="+ (Page+1).ToString() );

            builder.Append(keywordConverter(filters.Keyword));
            builder.Append(locationConverter(filters.Location));
            builder.Append(jobTitleConverter(filters.JobTitle));
            builder.Append(companyNameConverter(filters.CompanyName));
			builder.Append(JobTypeConverter(filters.JobType.ToString())); 
					/* case Field.EducationCode:
						switch(FilterDict[key])
							{
							case "Not Specified":
								builder.Append( "&EducationCode=DRNS");
 								break;
							case "None":
								builder.Append( "&EducationCode=DR3210");
 								break;
							case "High School":
								builder.Append( "&EducationCode=DR3211");
 								break;
							case "2 Year Degree":
								builder.Append( "&EducationCode=DR321");
 								break;
							case "4 Year Degree":
								builder.Append( "&EducationCode=DR32");
 								break;
							case "Graduate Degree":
								builder.Append( "&EducationCode=DR3");
 								break;
							case "Doctorate":
								builder.Append( "&EducationCode=DR");
 								break;
							default:
								break; */
			return builder.ToString( );
        }
		private static string JobTypeConverter ( String JobType )
			{
			String converted="";
			if(!String.IsNullOrWhiteSpace(JobType))
				switch ( JobType )
					{
					case "FullTime":
						converted="&EmpType=JTFT";
						break;
					case "PartTime":
						converted="&EmpType=JTPT";
						break;
					case "FullTimeOrPartTime":
						converted="&EmpType=JTFP";
						break;
					case "Contractor":
						converted="&EmpType=JTCT";
						break;
					case "Internship":
						converted="&EmpType=JTIN";
						break;
					case "SeasonalOrTemp":
						converted="&EmpType=JTSE";
						break;
					case "PerDiem":
						converted="&EmpType=JTPD";
						break;
					case "Franchises":
						converted="&EmpType=JTFR";
						break;
					case "All":
						converted="&EmpType=JTFT,JTPT,JTFP,JTCT,JTIN,JTSE,JTPD,JTFR";
						break;
					default:
						break;
					}
			return converted;
			}
        private static string companyNameConverter(string companyname)
        {
            if (String.IsNullOrWhiteSpace(companyname))
            {
                return "";
            }
            else
            {
                if (companyname.Contains(' '))
                {
                    companyname = '"' + companyname + '"'; //surround with quotes if spaces.
                }
                return "&CompanyName=" + companyname;
            }
        }

		private static string jobTitleConverter ( string title )
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            else
            {
                return "&JobTitle=" + title;
            }

        }

		private static string keywordConverter ( string keywords )
        {
            if (String.IsNullOrWhiteSpace(keywords))
            {
                return "";
            }
            else
            {
                return "&Keywords=" + keywords;
            }
        }

		private static string locationConverter ( Location loc )
        {
            if (null == loc)
            {
                return "";
            }

            string q = "&Location=";

            if (!String.IsNullOrWhiteSpace(loc.ZipCode))
            {
                q += loc.ZipCode;
            }
            else if (!String.IsNullOrWhiteSpace(loc.City) && !String.IsNullOrWhiteSpace(loc.State))
            {
                q += loc.City + ", " + loc.State;
            }
            else
            {
                return "";
            }

            return q;
        }

		}
	}