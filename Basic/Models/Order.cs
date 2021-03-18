using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Number { get; set; }
        public string Mobile { get; set; }
        public DateTime Date { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int AccountId { get; set; }
        public decimal OriginalPrice { get; set; }
        public int? DiscountId { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string DiscountInfo { get; set; }
        public decimal? AdultPrice { get; set; }
        public decimal? ChildPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? UserId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
