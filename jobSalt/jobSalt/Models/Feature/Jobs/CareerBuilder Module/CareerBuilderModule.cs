using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs.CareerBuilder_Module
	{
	public class CareerBuilderModule : Feature.Jobs.IJobModule
		{
		Source source = new Source( )
		{
			Name = "CareerBuilder" ,
			Icon = "/Content/images/careerbuilder_icon.png"
		};
		public Source Source
			{
			get
				{
				return source;
				}
			}
		private readonly CareerBuilderQueryBuilder builder;

		public CareerBuilderModule ( )
			{
			builder = new CareerBuilderQueryBuilder( );
			}

		public List<JobPost> GetJobs ( Dictionary<Field , string> filters , int page , int resultsPerPage )
			{
			List<JobPost> jobsToReturn = new List<JobPost>( );
			;
			// Return empty list if no filters are specified
			if ( filters.Count == 0 )
				{
				return new List<JobPost>( );
				}

			string request = builder.BuildQuery( filters , page , resultsPerPage );


			XDocument doc = XDocument.Load( request );
			IEnumerable<XElement> results = doc.Descendants( "ResponseJobSearch" ).Single( ).Descendants( "Results" ).Single( ).Descendants( "JobSearchResult" );


			foreach ( var jobPost in results )
				{
				JobPost post = new JobPost( );

				post.URL = jobPost.Element( "JobDetailsURL" ).Value;
				post.SourceModule =source;
				post.DatePosted = DateTime.Parse( jobPost.Element( "PostedDate" ).Value );
				post.JobTitle = jobPost.Element( "JobTitle" ).Value;

				//this field is returned as "MN - Plymouth", so split by values
				string[] location = jobPost.Element( "Location" ).Value.Split( new char[] { '-' } );
				post.Location = new Location
				{
					State= location[0].Trim( ) ,
					City= location[1].Trim( ) ,
					ZipCode=null
				};
				//if the company name is empty, fill with city and state (there are some posts with an empty company name)
				post.Company = jobPost.Element( "Company" ).Value==""?location[1]+" , "+location[0]:jobPost.Element( "Company" ).Value;
				post.Description =  jobPost.Element( "DescriptionTeaser" ).Value;
				post.FieldOfStudy = null;
				post.Salary =  jobPost.Element( "Pay" ).Value;
				jobsToReturn.Add( post );

				}
			return jobsToReturn;
			}
		}
	}