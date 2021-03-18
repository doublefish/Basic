using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Menus { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
