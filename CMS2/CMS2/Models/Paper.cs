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
    
    public partial class Paper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paper()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int paperId { get; set; }
        public string title { get; set; }
        public string paperAbstract { get; set; }
        public string content { get; set; }
        public string keywordList { get; set; }
        public Nullable<System.DateTime> publishDate { get; set; }
        public string reviewstatus { get; set; }
        public string finalstate { get; set; }
        public string paperType { get; set; }
        public Nullable<int> conferenceId { get; set; }
        public string authorId { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Conference Conference { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}