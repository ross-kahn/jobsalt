using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using jobSalt.Models.Data_Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace jobSalt.Models.Feature.Jobs.UAJobLink_Module
	{
	public class UAJobLinkModule : IJobModule
		{

		//Still mostly an empty module for now.
		// Simplicity data from wildcat joblink needs to be cached first in order to be queried.

		private Source source = new Source( )
			{
				Name = "UAJobLink" ,
				Icon = @"\Content\images\UA_Block_A.png"
			};
		public Source Source
			{
			get
				{
				return Source;
				}
			}
		/// <summary>
		/// Asyncronously grap a set of job postings based off of the given filters and page number.
		/// </summary>
		/// <param name="filters">The filters that the module should use to query</param>
		/// <param name="page">What page number of results to grab</param>
		/// <returns>The job postings</returns>
		public List<JobPost> GetJobs ( Dictionary<Field , string> filters , int page , int resultsPerPage )
			{

				List<JobPost> jobsToReturn = new List<JobPost>( ); 
				// Return empty list if no filters are specified
				if ( filters.Count == 0 )
					{
					return new List<JobPost>( );
					}

				//build the URI query
				string request = BuildQuery( filters , page , resultsPerPage );

				//get the raw JSON data
				WebClient wc  = new WebClient( ); 
				JArray JobPosts = JArray.Parse( wc.DownloadString( request ) );

				//Process It into JobPost objects
				foreach ( var jobPost in JobPosts )
					{
					JobPost post = new JobPost( );
					post.URL = "https://arizona-csm.symplicity.com/students/index.php?mode=form&id="+jobPost["SymplicityJobID"]+"&s=jobs&ss=jobs";
					post.SourceModule =source;
					post.DatePosted = DateTime.Parse( jobPost["PostingDate"].ToString( ) );
					post.JobTitle = jobPost["JobTitle"].ToString( );
					string[] location = jobPost["Location"].ToString( ).Split( new char[] { ',' } );
					post.Location = new Location
					{
						State= location[1].Trim( ) ,
						City= location[0].Trim( ) ,
						ZipCode=null
					};
					post.Company = jobPost["Employer"].ToString( );
					post.Description =  jobPost["Description"].ToString( );
					post.FieldOfStudy = null;
					post.Salary =  jobPost["SalaryLevel"].ToString( );
					jobsToReturn.Add( post );
					}
				return jobsToReturn;
			}

		public static String BuildQuery ( Dictionary<Field , String> FilterDict , int Page , int ResultsPerPage )
			{
			StringBuilder builder = new StringBuilder( );
			 
			builder.Append( "http://localhost:57215/api/SymplicityJobs/GetPage/?PageNum="+Page+"&ResultsPerPage="+ResultsPerPage );
			String Location ="NONE";
			String Keyword="NONE";
			String JobTitle ="NONE";
			String Employer="NONE";
			foreach ( Field key in FilterDict.Keys )
				{
				switch ( key )
					{
					case Field.CompanyName:
						Employer = FilterDict[key];
						break;

					case Field.Date:
						break;

					case Field.FieldOfStudy:
						break;

					case Field.JobTitle:
						JobTitle = FilterDict[key];
						break;

					case Field.Keyword:
						Keyword=FilterDict[key];
						break;

					case Field.Location:
						Location=FilterDict[key];
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

				builder.Append( "&Location="+Location+"&Keyword="+Keyword+"&JobTitle="+JobTitle+"&Employer="+Employer );
				}
			return builder.ToString( );
			}
		}
	}