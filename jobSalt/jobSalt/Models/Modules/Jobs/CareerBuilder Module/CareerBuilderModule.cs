using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace jobSalt.Models.Modules.Jobs.CareerBuilder_Module
	{
	public class CareerBuilderModule : Modules.Jobs.IJobModule
		{
		Source source = new Source( )
		{
			Name = "CareerBuilder" ,
			Icon = "http://img.icbdr.com/images/js/cb_logo_new_homepage.png"
		};
		public Source Source
			{
			get
				{
				return source;
				}
			}
		private CareerBuilderQueryBuilder builder;
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

			XDocument doc= XDocument.Load( request );
			var results = doc.Element("ResponseJobSearch").Element("Results");

			foreach ( var post in results.Elements( "JobSearchResult" ))
				{
				CareerBuilderJobPost jobPost = new CareerBuilderJobPost
				{
					Company = post.Element( "Company" ).ToString( ) ,
					CompanyDetailsURL = post.Element( "CompanyDetailsURL" ).ToString( ) ,
					CompanyDID =post.Element( "CompanyDID" ).ToString( ) ,
					CompanyImageURL =post.Element( "CompanyImageURL" ).ToString( ) ,
					DescriptionTeaser =post.Element( "DescriptionTeaser" ).ToString( ) ,
					DID =post.Element( "DID" ).ToString( ) ,
					Distance =post.Element( "Distance" ).ToString( ) ,
					EmployMentType =post.Element( "EmployMentType" ).ToString( ) ,
					JobBrandingIcons =post.Element( "JobBrandingIcons" ).ToString( ) ,
					JobDetailsURL =post.Element( "JobDetailsURL" ).ToString( ) ,
					JobServiceURL=post.Element( "JobServiceURL" ).ToString( ) ,
					JobTitle =post.Element( "JobTitle" ).ToString( ) ,
					Location =post.Element( "Location" ).ToString( ) ,
					LocationLatitude=post.Element( "LocationLatitude" ).ToString( ) ,
					LocationLongitude =post.Element( "LocationLongitude" ).ToString( ) ,
					OnetCode =post.Element( "OnetCode" ).ToString( ) ,
					ONetFriendlyTitle =post.Element( "ONetFriendlyTitle" ).ToString( ) ,
					Pay =post.Element( " Pay" ).ToString( ) ,
					PostedDate =post.Element( "PostedDate" ).ToString( ) ,
					SimilarJobsURL =post.Element( "SimilarJobsURL" ).ToString( )
				};
				jobsToReturn.Add( new JobPost
				{
					Company = jobPost.Company ,
					URL = jobPost.JobDetailsURL ,
					SourceModule =source ,
					DatePosted = DateTime.Parse( jobPost.PostedDate ) ,
					JobTitle = jobPost.JobTitle ,
					Location = new Location( ) ,
					Description = jobPost.DescriptionTeaser ,
					FieldOfStudy = null ,
					Salary =null
				} );
				}
			return jobsToReturn;
			}
		}
	}