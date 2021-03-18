using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
