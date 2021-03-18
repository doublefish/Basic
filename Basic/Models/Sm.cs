using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Sm
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string CheckCode { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
