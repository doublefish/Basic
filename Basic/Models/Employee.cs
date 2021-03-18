using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public int? IdType { get; set; }
        public string IdNumber { get; set; }
        public int? Nation { get; set; }
        public int? NativePlace { get; set; }
        public int? PoliticalStatus { get; set; }
        public int? MaritalStatus { get; set; }
        public string HealthStatus { get; set; }
        public int? Faith { get; set; }
        public int? Education { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string JobTitle { get; set; }
        public string LanguageSkills { get; set; }
        public string ComputerSkills { get; set; }
        public string Certificates { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int? Post { get; set; }
        public string Photo { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
