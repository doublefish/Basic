using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public int Sequence { get; set; }
    }
}
