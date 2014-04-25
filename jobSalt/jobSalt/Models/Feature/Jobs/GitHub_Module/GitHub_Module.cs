using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using jobSalt.Models.Data_Types;
using System.Net;

namespace jobSalt.Models.Feature.Jobs.GitHub_Module
	{
	public class GitHub_Module : IJobModule
		{
		private Source source = new Source( )
		{
			Name = "GitHub" ,
			Icon = @"\Content\images\GitHub-Mark-230.png"
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
		/// https://jobs.github.com/api
		/// </summary>
		/// <param name="filters">The filters that the module should use to query</param>
		/// <param name="page">What page number of results to grab</param>
		/// <returns>The job postings</returns>
		public List<JobPost> GetJobs ( FilterBag filters , int page , int resultsPerPage )
			{
			resultsPerPage = ( ( resultsPerPage<=50?resultsPerPage:50 )>=1?( resultsPerPage<=50?resultsPerPage:50 ):1 );
			int startIndex = ( resultsPerPage*page )<50? ( resultsPerPage*page ):49;
			//int stopIndex = startIndex + resultsPerPage-1 <=50? startIndex + resultsPerPage-1 :50;
			List<JobPost> jobsToReturn = new List<JobPost>( );

			//build the URI query
			string request = BuildQuery( filters , page , resultsPerPage );

			//get the raw JSON data
			WebClient wc  = new WebClient( );
			JArray JobPosts = JArray.Parse( wc.DownloadString( request ) );

			//Process It into JobPost objects
			foreach ( var jobPost in JobPosts )
				{
				try
					{

					JobPost post = new JobPost( );
					post.URL = jobPost["url"].ToString( );
					post.SourceModule =source;
					var date = jobPost["created_at"].ToString( ).Replace( "UTC " , "" );
					post.DatePosted =DateTime.ParseExact( date , "ddd MMM dd HH:mm:ss yyyy" , null );
					post.JobTitle = jobPost["title"].ToString( );
					string[] location = jobPost["location"].ToString( ).Split( new char[] { ',' } );
					if ( location.Length>=2 )
						post.Location = new Location
						{
							State= location[1].Trim( ) ,
							City= location[0].Trim( ) ,
							ZipCode=null
						};
					else
						post.Location = new Location
						{
							City=null ,
							State=null ,
							ZipCode=null
						};
					post.Company = jobPost["company"].ToString( );
					post.Description =  jobPost["description"].ToString( );
					post.FieldOfStudy = null;
					post.Salary =  null;
					jobsToReturn.Add( post );
					}
				catch ( Exception ex )
					{
					System.Diagnostics.Debug.WriteLine( "GitHub Module:\t" +ex.Message );
					}
				}
			return jobsToReturn.Skip( startIndex ).Take( resultsPerPage ).ToList<JobPost>( );
			}

		public static String BuildQuery ( FilterBag FilterDict , int Page , int ResultsPerPage )
			{
			StringBuilder builder = new StringBuilder( );
			ResultsPerPage = ResultsPerPage<=50?ResultsPerPage:50;
			Page = (Int32)( Page*( (Double)ResultsPerPage/50.0 ) ); //returns 50 pages
			builder.Append( "http://jobs.github.com/positions.json?page="+Page );
			String description ="";

			if ( FilterDict.CompanyName != "" )
				{
				description+= FilterDict.CompanyName+", ";
				}
			if ( FilterDict.FieldOfStudy != "" )
				{
				description+=  FilterDict.FieldOfStudy+", ";
				}
			if ( FilterDict.JobTitle != "" )
				{
				description+=  FilterDict.JobTitle+", ";
				}
			if ( FilterDict.Keyword != "" )
				{
				if ( FilterDict.Keyword == "full time" )
					builder.Append( "&full_time=true" );
				else
					description+= FilterDict.Keyword;
				}
			if ( FilterDict.Location!=null && ( FilterDict.Location.City != "" || FilterDict.Location.State != "" ||FilterDict.Location.ZipCode !="" ) )
				{
				builder.Append( "&location="+ ( FilterDict.Location.City??"" ) + ", "+ ( FilterDict.Location.State??"" ) +" " +( FilterDict.Location.ZipCode??"" ) );
				}
			if ( description!="" )
				{
				builder.Append( "&description="+description );
				}


			return builder.ToString( );
			}
		}

	}