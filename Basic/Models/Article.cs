using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Sections { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public string Cover { get; set; }
        public bool IsStick { get; set; }
        public long Clicks { get; set; }
        public long Favorites { get; set; }
        public long Shares { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
