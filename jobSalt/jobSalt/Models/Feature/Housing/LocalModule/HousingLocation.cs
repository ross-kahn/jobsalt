//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jobSalt.Models.Feature.Housing.LocalModule
{
    using System;
    using System.Collections.Generic;
    
    public partial class HousingLocation
    {
        public HousingLocation()
        {
            this.HousingReviews = new HashSet<HousingReview>();
        }
    
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
    
        public virtual ICollection<HousingReview> HousingReviews { get; set; }
    }
}
