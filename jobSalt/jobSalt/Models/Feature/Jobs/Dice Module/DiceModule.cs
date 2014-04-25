using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs.Dice_Module
{
	public class DiceModule : Feature.Jobs.IJobModule
	{
		Source source = new Source( )
		{
			Name = "Dice_Module" ,
			Icon = "/Content/images/DiceLogo-512x512.png"
		};
		public Source Source
			{
			get
				{
				return source;
				}
			}
		

		private readonly DiceQueryBuilder builder;

		public DiceModule()
			{
			builder = new DiceQueryBuilder( );
			}

		public List<JobPost> GetJobs ( FilterBag filters , int page , int resultsPerPage )
			{
			List<JobPost> jobsToReturn = new List<JobPost>( );
			;

			string request = builder.BuildQuery( filters , page , resultsPerPage );


			XDocument doc = XDocument.Load( request );
			IEnumerable<XElement> results = doc.Descendants( "result" ).Single( ).Descendants( "resultItemList" ).Single( ).Descendants( "resultItem" );

			foreach ( var jobPost in results )
				{
				JobPost post = new JobPost( );


				post.Company = jobPost.Element( "company" ).Value;
				post.URL = jobPost.Element( "detailUrl" ).Value;
				post.SourceModule =source;
				post.DatePosted = DateTime.Parse( jobPost.Element( "date" ).Value );
				post.JobTitle = jobPost.Element( "jobTitle" ).Value;
				//this field is returned as "San Ramon, CA", so split by values
				string[] location = jobPost.Element( "location" ).Value.Split( new char[] { ',' } );
				post.Location = new Location
				{
					State= location[1].Trim( ) ,
					City= location[0].Trim( ) ,
					ZipCode=null
				};
				//Dice_Module doesn't even give a teaser...so I'm faking one.
				post.Description =  jobPost.Element( "company" ).Value +" is looking for a " +jobPost.Element( "jobTitle" ).Value;
				post.FieldOfStudy = null;
				post.Salary =  null;
				jobsToReturn.Add( post );

				}
			return jobsToReturn;
			}
	}
}