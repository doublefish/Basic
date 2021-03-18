using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class CommissionRule
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
