using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WeChat { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
