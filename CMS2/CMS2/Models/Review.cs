//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public int reviewId { get; set; }
        public Nullable<System.DateTime> reviewDate { get; set; }
        public string rating { get; set; }
        public string reviewDesc { get; set; }
        public string reviewerId { get; set; }
        public Nullable<int> paperId { get; set; }
    
        public virtual Paper Paper { get; set; }
        public virtual Reviewer Reviewer { get; set; }
    }
}
