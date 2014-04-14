using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs
{
    public class DummyJobModule : IJobModule
    {
        public List<JobPost> GetJobs(FilterBag filters, int page, int resultsPerPage)
        {
            List<JobPost> jobs = new List<JobPost>()
            {
                new JobPost(){
                    Company="Microsoft", 
                    DatePosted = DateTime.Now, 
                    Description="Job Post", 
                    JobTitle="Software Development Engineer in Test", 
                    Location=new Location("WA", "Redmond", "98006"),
                    SourceModule = new Source(){ Icon=@"\Content\images\indeed_icon.png", Name="Indeed"}
                },
                new JobPost(){
                    Company="Amazon", 
                    DatePosted = DateTime.Now, 
                    Description="Job Post", 
                    JobTitle="Distributed Systems Engineer", 
                    Location=new Location("WA", "Redmond", "98006"),
                    SourceModule = new Source(){ Icon=@"\Content\images\rit_icon.png", Name="Indeed"}
                },
                new JobPost(){
                    Company="Google", 
                    DatePosted = DateTime.Now.AddDays(1), 
                    Description="Are you a talented Android developer who wants to build best-in-class apps? Do you want to work with exceptional developers and interact with your clients instead of dealing with project managers? As an Android developer at WillowTree Apps you will work on a wide range of projects and be responsible for your work and client deliverables. ", 
                    JobTitle="Mobile Application Developer", 
                    Location=new Location("CA", "San Francisco", "76594"),
                    SourceModule = new Source(){ Icon=@"\Content\images\careerbuilder_icon.png", Name="Indeed"}
                },
                new JobPost(){
                    Company="Intel", 
                    DatePosted = DateTime.Now, 
                    Description="Job Post", 
                    JobTitle="Software Egineer", 
                    Location=new Location("WA", "Redmond", "98006"),
                    SourceModule = new Source(){ Icon=@"\Content\images\indeed_icon.png", Name="Indeed"}
                },
                new JobPost(){
                    Company="Rochester Institute of Technology", 
                    DatePosted = DateTime.Now.AddDays(1.5), 
                    Description="Job Post", 
                    JobTitle="Software Engineer Chair", 
                    Location=new Location("NY", "Rochester", "14623"),
                    SourceModule = new Source(){ Icon=@"\Content\images\rit_icon.png", Name="Indeed"}
                },
                new JobPost(){
                    Company="Intuit", 
                    DatePosted = DateTime.Now.AddDays(2), 
                    Description="Job Post", 
                    JobTitle="C++ Aplication Developer", 
                    Location=new Location("WA", "Redmond", "98006"),
                    SourceModule = new Source(){ Icon=@"\Content\images\careerbuilder_icon.png", Name="Indeed"}
                }
            };

            return jobs;
        }

        public Source Source
        {
            get { throw new NotImplementedException(); }
        }

    }
}