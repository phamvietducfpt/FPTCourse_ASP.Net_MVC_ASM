//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPTCourse_ASP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPer_Permission
    {
        public string User_Permission { get; set; }
        public string Permission_ID { get; set; }
        public string Ghichu { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual User_Permission User_Permission1 { get; set; }
    }
}
