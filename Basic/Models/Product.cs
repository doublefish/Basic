using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Tags { get; set; }
        public string Themes { get; set; }
        public decimal Price { get; set; }
        public string Overview { get; set; }
        public string Feature { get; set; }
        public string Notice { get; set; }
        public string Book { get; set; }
        public string Cost { get; set; }
        public string Cover { get; set; }
        public string Cover1 { get; set; }
        public int Recommends { get; set; }
        public int Orders { get; set; }
        public int Clicks { get; set; }
        public int Sequence { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
