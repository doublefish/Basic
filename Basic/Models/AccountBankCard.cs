using System;
using System.Collections.Generic;

#nullable disable

namespace Basic.Models
{
    public partial class AccountBankCard
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BankId { get; set; }
        public string CardNumber { get; set; }
        public string Cardholder { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
