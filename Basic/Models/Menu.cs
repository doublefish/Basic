using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Parents { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string PageUrl { get; set; }
        public string ApiUrl { get; set; }
        public string Icon { get; set; }
        public int Sequence { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
