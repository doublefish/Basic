﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountMail
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string CheckCode { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
