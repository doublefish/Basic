using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Dict
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Parents { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Sequence { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
