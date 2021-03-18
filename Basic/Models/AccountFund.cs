using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountFund
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Number { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal Freeze { get; set; }
        public string Note { get; set; }
    }
}
