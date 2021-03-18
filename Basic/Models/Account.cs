using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Sex { get; set; }
        public int? IdType { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public decimal Balance { get; set; }
        public decimal Freeze { get; set; }
        public string SecretKey { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
