using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class Transaction
    {
        public int TransactionNumber { get; set; } // Primary Key
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } // Deposit or Withdrawal
        public string Note { get; set; }

        public string accountNumber { get; set; } // Foreign Key, one-to-many relationship with Account
        public Account account { get; set; } // Navigation Property
    }
}
