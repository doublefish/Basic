using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Region
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Parents { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string EnName { get; set; }
        public string Pinyin { get; set; }
        public string AreaCode { get; set; }
        public string ZipCode { get; set; }
        public int Sequence { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
