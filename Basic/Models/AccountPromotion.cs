using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountPromotion
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? PromoterId { get; set; }
        public int Orders { get; set; }
        public decimal OrderAmount { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
