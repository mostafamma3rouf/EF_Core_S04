using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class Account
    {
        public string AccountNumber { get; set; } // Primary Key
        public decimal CurrentBalance { get; set; }
        public string AccountType { get; set; } // Savings, Current, or Fixed Deposit
        public DateTime OpeningDate { get; set; }

        public string branchCode { get; set; } // Foreign Key, one-to-many relationship with Branch
        public Branch branch { get; set; } // Navigation Property

        public ICollection<Transaction> transactions { get; set; } = new List<Transaction>(); // Navigation Property, one-to-many relationship with Transaction
        public ICollection<AccountCustomers> accountCustomers { get; set; } = new List<AccountCustomers>(); // Navigation Property, many-to-many relationship with Customer
    }
}
