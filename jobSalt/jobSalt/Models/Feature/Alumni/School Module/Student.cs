//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.GradPlacements = new HashSet<GradPlacement>();
        }
    
        public string UID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public bool IsNTID { get; set; }
        public bool IsAALANA { get; set; }
        public string Citizenship1 { get; set; }
        public string Citizenship2 { get; set; }
        public string Ethnicity { get; set; }
        public int CurrentTerm { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentProgramId { get; set; }
        public Nullable<int> CurrentSecondaryProgramId { get; set; }
        public string CurrentDCE { get; set; }
        public string CurrentEmail { get; set; }
        public Nullable<decimal> CurrentGPA { get; set; }
        public Nullable<decimal> CurrentCumGPA { get; set; }
        public Nullable<int> CurrentCompletionTerm { get; set; }
        public Nullable<int> CurrentExpectedGradTerm { get; set; }
        public Nullable<int> CurrentNumCoops { get; set; }
        public Nullable<int> CurrentNumWaivers { get; set; }
        public Nullable<System.DateTime> CurrentLeavingDate { get; set; }
        public string CurrentLeavingReason { get; set; }
        public string CurrentAdvisorLastName { get; set; }
        public string CurrentAdvisorFirstName { get; set; }
        public string CurrentAdvisorUID { get; set; }
        public string CurrentAdvisorEmail { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<GradPlacement> GradPlacements { get; set; }
        public virtual Program Program { get; set; }
    }
}
