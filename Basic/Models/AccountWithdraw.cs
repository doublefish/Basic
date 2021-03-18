using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountWithdraw
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public string CardNumber { get; set; }
        public string Cardholder { get; set; }
        public string Branch { get; set; }
        public int? UserId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
