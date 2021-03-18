using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class UserPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Password { get; set; }
    }
}
