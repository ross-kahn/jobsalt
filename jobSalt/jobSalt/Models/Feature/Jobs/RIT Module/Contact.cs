//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jobSalt.Models.Feature.Jobs.RIT_Module
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public Contact()
        {
            this.Employers = new HashSet<Employer>();
            this.Jobs = new HashSet<Job>();
        }
    
        public string id { get; set; }
        public string salutation { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleInitial { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string archiveStatus { get; set; }
        public string employerId { get; set; }
    
        public virtual Employer Employer { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
