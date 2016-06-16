using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class Page
    {
        public int page_id { get; set; }
        public string ControlName { get; set; }
        public string ControlPath { get; set; }
        public Nullable<int> Pagetype { get; set; }
        public Nullable<bool> is_home { get; set; }
        public string title { get; set; }
        public string nav_title { get; set; }
        public string slug { get; set; }
        public string uri { get; set; }
        public Nullable<int> parent_id { get; set; }
        public string meta_title { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
        public string banner { get; set; }
        public string css { get; set; }
        public string js { get; set; }
        public string status { get; set; }
        public Nullable<decimal> orderrank { get; set; }
        public string template { get; set; }
        public string sub_template { get; set; }
        public string additionals { get; set; }
        public string widgets { get; set; }
        public Nullable<bool> in_main_nav { get; set; }
        public Nullable<bool> in_footer_nav { get; set; }
        public Nullable<bool> in_bottom_nav { get; set; }
        public string body { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    }
}
