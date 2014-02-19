using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs.CareerBuilder_Module
	{
	public class CareerBuilderJobPost
		{

		/// <summary>
		/// The company that posted the job. Example: "ProTrades Connection"
		/// </summary>
		public String Company
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "chq66b6pbmkqfjvqq4r"
		/// </summary>
		public String CompanyDID
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "http://www.careerbuilder.com/Jobs/Company/CHQ66B6PBMKQFJVQQ4R/Infinity-Consulting-Solutions/?sc_cmp1=13_JobRes_ComDet"
		/// </summary>
		public String CompanyDetailsURL
			{
			get;
			set;
			}
		/// <summary>
		/// CareerBuilder's job Id. Example: J8F7RZ75JKT1J9KBS28
		/// </summary>
		public String DID
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "11-1021.00"
		/// </summary>
		public String OnetCode
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "Operations Managers"
		/// </summary>
		public String ONetFriendlyTitle
			{
			get;
			set;
			}
		/// <summary>
		/// One or two line teaser. Example: "Event Marketing Coordinator / PR - College Graduates ***Management Training Provided*** Finding the right career in this market is tough...."
		/// </summary>
		public String DescriptionTeaser
			{
			get;
			set;
			}
		public String Distance
			{
			get;
			set;
			}
		

		/// <summary>
		/// Employment type. Example: "Full-Time/Part-Time"
		/// </summary>
		public String EmployMentType
			{
			get;
			set;
			}
		/// <summary>
		/// The URL to view the details of the job post. Example: "http://www.careerbuilder.com/JobSeeker/..."
		/// </summary>
		public String JobDetailsURL
			{
			get;
			set;
			}
		/// <summary>
		/// The API URL specific to this job post. Example: "http://api.careerbuilder.com/v1/job?DID=..."
		/// </summary>
		public String JobServiceURL
			{
			get;
			set;
			}
		/// <summary>
		/// State and City. Example: "CA - Fresno"
		/// </summary>
		public String Location
			{
			get;
			set;
			}
		/// <summary>
		/// Latitude coordinate of the location of the job posting. Example: "36.763642"
		/// </summary>
		public String LocationLatitude
			{
			get;
			set;
			}
		/// <summary>
		/// Longitude coordinate of the location of the job posting. Example: "-119.774665"
		/// </summary>
		public String LocationLongitude
			{
			get;
			set;
			}
	
		/// <summary>
		/// The date this job was posted. Example: "9/26/2007"
		/// </summary>
		public String PostedDate
			{
			get;
			set;
			}
		/// <summary>
		/// Salary information. Example: "N/A"
		/// </summary>
		public String Pay
			{
			get;
			set;
			}
		/// <summary>
		/// URL to view other job posts like this job post on CareerBuilder. Example: "http://www.careerbuilder.com/jobseeker/..."
		/// </summary>
		public String SimilarJobsURL
			{
			get;
			set;
			}
		/// <summary>
		/// The Job title of the job. Example: "Account Executive"
		/// </summary>
		public String JobTitle
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "http://emj.icbdr.com/mediamanagement/r3/mrb1ym702srfd916xr3.jpg"
		/// </summary>
		public String CompanyImageURL
			{
			get;
			set;
			}
		/// <summary>
		/// Example: "PIcon"
		/// </summary>
		public String JobBrandingIcons
			{
			get;
			set;
			}
		}
	}