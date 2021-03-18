using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class ProductDiscount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int Total { get; set; }
        public int Used { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
