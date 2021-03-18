using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountCommission
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int OrderId { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
