//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagerDBA
{
    using System;
    using System.Collections.Generic;
    
    public partial class List_Tasks
    {
        public int id { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public int list_id { get; set; }
        public string status { get; set; }
    }
}
